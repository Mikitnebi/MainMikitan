﻿using Dapper;
using MainMikitan.Database.Features.Common.Otp.Interfaces;
using MainMikitan.Domain.Interfaces.Common;
using MainMikitan.Domain.Models.Common;
using MainMikitan.Domain.Models.Setting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMikitan.Database.Features.Common.Otp.Command
{
    public class OtpLogCommandRepository : IOtpLogCommandRepository
    {
        private readonly ConnectionStringsOptions _connectionStrings;
        public OtpLogCommandRepository(IOptions<ConnectionStringsOptions> connectionStrings)
        {
            _connectionStrings = connectionStrings.Value;
        }
        public async Task<int?> Create(OtpLogIntroEntity model)
        {
            using var connection = new SqlConnection(_connectionStrings.MainMik);
            model.CreateAt = DateTime.Now; 
            var sqlCommand = "INSERT INTO [dbo].[OtpLogIntro] " +
              "[CreatedAt], " +
              "[Otp]," +
              "[MobileNumber], " +
              "[EmailAddress], " +
              "[NumberOfTrials], " +
              "[NumberOfTrialsIsRequired]) " +
              " OUTPUT INSERTED.Id" +
              " VALUES (@CreatedAt, @Otp, @MobileNumber, @NumberOfTrials, @NumberOfTrialsIsRequired)";
            var result = await connection.QuerySingleOrDefaultAsync<int?>(sqlCommand, model);
            return result;
        }
    }
}
