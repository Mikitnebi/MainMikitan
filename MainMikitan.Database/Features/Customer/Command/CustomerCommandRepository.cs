using Dapper;
using MainMikitan.InternalServiceAdapter.Hasher;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain.Models.Customer;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;
using NPOI.XSSF.UserModel.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.Database.Features.Customer.Command
{
    public class CustomerCommandRepository : ICustomerCommandRepository
    {
        private readonly ConnectionStringsOptions _connectionStrings;
        private readonly ICustomerQueryRepository _customerQueryRepository;
        private readonly IPasswordHasher _passwordHasher;
        public CustomerCommandRepository
            (
            IPasswordHasher passwordHasher,
            IOptions<ConnectionStringsOptions> connectionStrings,
            ICustomerQueryRepository customerQueryRepository
            )
        {
            _passwordHasher = passwordHasher;
            _connectionStrings = connectionStrings.Value;
            _customerQueryRepository = customerQueryRepository;
        }
        public async Task<int?> UpdateStatus(string email, bool emailConfirmation, CustomerStatusId status)
        {
            int statusId = (int)status;
            int confirmation = emailConfirmation == true ? 1 : 0;
            using var connection = new SqlConnection(_connectionStrings.MainMik);
            var customer = await _customerQueryRepository.GetNonVerifiedByEmail(email);
            if (customer != null)
            {
                var sqlCommand = "UPDATE [dbo].[Customers] " +
                    "SET [StatusId] = @statusId, " +
                    "[EmailConfirmation] = @confirmation " +
                    "WHERE [Id] = @id";
                var result = await connection.ExecuteAsync(sqlCommand, new { statusId, confirmation, id = customer.Id});
                return result;
            }
            return customer== null ? 0 : customer.Id;
        }
        public async Task<int?> CreateOrUpdate(CustomerEntity entity)
        {
            using var connection = new SqlConnection(_connectionStrings.MainMik);
            if (await _customerQueryRepository.GetNonVerifiedByEmail(entity.EmailAddress) != null)
            {
                entity.CreatedAt = DateTime.Now;
                entity.StatusId = (int)CustomerStatusId.NoneVerified;
                entity.HashPassWord = _passwordHasher.HashPassword(entity.HashPassWord);

                var sqlCommand = "UPDATE [dbo].[Customers] " +
                    "SET [FullName] = @FullName, " +
                    "[EmailConfirmation] = @EmailConfirmation, " +
                    "[MobileNumber] = @MobileNumber, " +
                    "[MobileNumberConfirmation] = @MobileNumberConfirmation," +
                    "[HashPassWord] = @HashPassWord, " +
                    "[StatusId] = @StatusId," +
                    "[CreatedAt] = @CreatedAt " +
                    " WHERE [EmailAddress] = @EmailAddress";
                var result = await connection.ExecuteAsync( sqlCommand, entity );
                return result;
            }
            else
            {
                entity.CreatedAt = DateTime.Now;
                entity.StatusId = (int)CustomerStatusId.NoneVerified;
                entity.HashPassWord = _passwordHasher.HashPassword(entity.HashPassWord);

                var sqlCommand = "INSERT INTO [dbo].[Customers] " +
                    "([FullName]," +
                    "[EmailAddress]," +
                    "[EmailConfirmation]," +
                    "[MobileNumber]," +
                    "[MobileNumberConfirmation]," +
                    "[HashPassWord]," +
                    "[StatusId]," +
                    "[CreatedAt])" +
                    " OUTPUT INSERTED.Id" +
                    " VALUES (@FullName,@EmailAddress,@EmailConfirmation," +
                    "@MobileNumber, @MobileNumberConfirmation, @HashPassWord," +
                    "@StatusId, @CreatedAt)";
                var result = await connection.QueryFirstOrDefaultAsync<int?>(sqlCommand, entity);
                return result;
            }
        }
    }
}
