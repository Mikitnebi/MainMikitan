using Dapper;
using MainMikitan.InternalServiceAdapter.Hasher;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain.Models.Customer;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using MainMikitan.Database.DbContext;
using Microsoft.EntityFrameworkCore;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.Database.Features.Customer.Command
{
    public class CustomerCommandRepository(
        IPasswordHasher passwordHasher,
        IOptions<ConnectionStringsOptions> connectionStrings,
        ICustomerQueryRepository customerQueryRepository,
        MikDbContext mikDbContext)
        : ICustomerCommandRepository
    {
        private readonly ConnectionStringsOptions _connectionStrings = connectionStrings.Value;

        public async Task<bool> UpdateStatus(string email, bool emailConfirmation, CustomerStatusId status, CancellationToken cancellationToken = default)
        {
            var statusId = (int)status;
            var confirmation = emailConfirmation == true ? 1 : 0;
            await using var connection = new SqlConnection(_connectionStrings.MainMik);
            var customer = await customerQueryRepository.GetNonVerifiedByEmail(email, cancellationToken);
            if (customer == null) return (customer?.Id) != 0;
            var sqlCommand = $"""
                              UPDATE [dbo].[Customers]
                              SET [StatusId] = @statusId,
                              [EmailConfirmation] = @confirmation
                              WHERE [Id] = @id
                              """;
            var result = await connection.ExecuteAsync(sqlCommand, new { statusId, confirmation, id = customer.Id});
            return result > 0;
        }
        public async Task<bool> CreateOrUpdate(CustomerEntity entity, CancellationToken cancellationToken = default)
        {
            await using var connection = new SqlConnection(_connectionStrings.MainMik);
            if (await customerQueryRepository.GetNonVerifiedByEmail(entity.EmailAddress, cancellationToken) != null)
            {
                entity.CreatedAt = DateTime.Now;
                entity.StatusId = (int)CustomerStatusId.NoneVerified;
                entity.HashPassWord = passwordHasher.HashPassword(entity.HashPassWord);

                var sqlCommand = $"""
                                  UPDATE [dbo].[Customers] 
                                  SET [FullName] = @FullName, 
                                  [EmailConfirmation] = @EmailConfirmation, 
                                  [MobileNumber] = @MobileNumber, 
                                  [MobileNumberConfirmation] = @MobileNumberConfirmation,
                                  [HashPassWord] = @HashPassWord, 
                                  [StatusId] = @StatusId,
                                  [CreatedAt] = @CreatedAt 
                                  WHERE [EmailAddress] = @EmailAddress
                                  """;
                var result = await connection.ExecuteAsync( sqlCommand, entity );
                return result > 0;
            }
            else
            {
                entity.CreatedAt = DateTime.Now;
                entity.StatusId = (int)CustomerStatusId.NoneVerified;
                entity.HashPassWord = passwordHasher.HashPassword(entity.HashPassWord);

                var sqlCommand = $"""
                                  INSERT INTO [dbo].[Customers] 
                                  ([FullName],
                                  [EmailAddress],
                                  [EmailConfirmation],
                                  [MobileNumber],
                                  [MobileNumberConfirmation],
                                  [HashPassWord],
                                  [StatusId],
                                  [CreatedAt])
                                   OUTPUT INSERTED.Id
                                   VALUES (@FullName,@EmailAddress,@EmailConfirmation,
                                  @MobileNumber, @MobileNumberConfirmation, @HashPassWord,
                                  @StatusId, @CreatedAt)
                                  """;
                var result = await connection.QueryFirstOrDefaultAsync<int?>(sqlCommand, entity);
                return result > 0;
            }
        }

        public bool UpdateCustomer(CustomerEntity customer)
        {
            customer.HashPassWord = passwordHasher.HashPassword(customer.HashPassWord);
            var updateResponse = mikDbContext.Customers.Update(customer);
            return updateResponse.State == EntityState.Modified ? true : false;
        }

        public async Task<bool> SaveChanges(CancellationToken cancellationToken = default)
        {
            return (await mikDbContext.SaveChangesAsync(cancellationToken)) > 0;
        }

        public async Task<bool> Delete(int userId, CancellationToken cancellationToken = default)
        {
            await using var connection = new SqlConnection(_connectionStrings.MainMik);
            var sqlCommand = "DELETE FROM [dbo].[Customers] WHERE [Id] = @userId";
            var result = await connection.QueryFirstOrDefaultAsync<int?>(sqlCommand, new { userId});
            return result > 0;
        } 

        
    }
}
