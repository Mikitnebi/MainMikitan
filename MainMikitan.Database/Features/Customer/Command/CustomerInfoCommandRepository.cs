using System.Data.SqlClient;
using Dapper;
using MainMikitan.Database.Features.Common.Multifunctional.Interface.Repository;
using MainMikitan.Database.Features.Customer.Interface;
using MainMikitan.Domain.Models.Setting;
using MainMikitan.Domain.Requests.Customer;
using Microsoft.Extensions.Options;

namespace MainMikitan.Database.Features.Customer.Command;

public class CustomerInfoCommandRepository
{
    private readonly ConnectionStringsOptions _connectionString;
    private readonly IMultifunctionalRepository _multifunctionalRepository;
    private readonly ICustomerInfoQueryRepository _customerInfoQueryRepository;
    public CustomerInfoCommandRepository
    (IOptions<ConnectionStringsOptions> connectionString, 
        IMultifunctionalRepository multifunctionalRepository, 
        ICustomerInfoQueryRepository customerInfoQueryRepository)
    {
        _multifunctionalRepository = multifunctionalRepository;
        _customerInfoQueryRepository = customerInfoQueryRepository;
        _connectionString = connectionString.Value;
    }

    // public async Task<int> AddOrUpdateCustomerInfo(FillCustomerInfoRequest request)
    // {
    //     using var connection = new SqlConnection(_connectionString.MainMik);
    //     var customerInfo = await _customerInfoQueryRepository.GetVerifiedFromCustomerInfoById(request.CustomerId);
    //     if (customerInfo is null)
    //     {
    //         return await _multifunctionalRepository.AddOrUpdateTableData(request, "MainMikitan", "dbo", "CustomerInfo");
    //     }
    //
    //
    //     var updateQuery = "UPDATE [MainMikitan].[dbo].[CustomerInfo] SET FirstName = @FirstName, " +
    //                       "LastName = @LastName," +
    //                       "BirthDate = @BirthDate," +
    //                       "GenderId = @GenderId," +
    //                       "NationalityId = @NationalityId," +
    //                       "UpdateAt = @UpdateAt" +
    //                       "WHERE CustomerId = @CustomerId";
    //
    //     return await connection.ExecuteAsync(updateQuery, request);
    // }
}