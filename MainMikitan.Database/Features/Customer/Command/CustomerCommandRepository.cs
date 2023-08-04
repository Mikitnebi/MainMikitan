using Dapper;
using MainMikitan.Domain.Interfaces.Common;
using MainMikitan.Domain.Interfaces.Customer;
using MainMikitan.Domain.Models.Customer;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;
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
        public async Task<int> Create(CustomerEntity entity)
        {
            using var connection = new SqlConnection(_connectionStrings.MainMik);
            entity.CreatedAt = DateTime.Now;
            entity.StatusId = (int)CustomerStatusId.Verified;
            entity.HashPassWord = _passwordHasher.HashPassword(entity.HashPassWord);

            var sqlCommand = "INSERT INTO [dbo].[Customers] " +
                "([FullName]," +
                "[Email]," +
                "[EmailConfirmation]," +
                "[MobileNumber]," +
                "[MobileNumberConfirmation]," +
                "[HashPassWord]," +
                "[GenderId]," +
                "[StatusId]," +
                "[CreatedAt]," +
                " OUTPUT INSERTED.Id" +
                " VALUES (@FullName,@Email,@EmailConfirmation," +
                "@MobileNumber, @MobileNumberConfirmation, @HashPassWord," +
                "@GenderId, @StatusId, @CreatedAt)";
            var result = await connection.QuerySingleOrDefaultAsync<int>(sqlCommand, entity);
                return result;
        }
    }
}
