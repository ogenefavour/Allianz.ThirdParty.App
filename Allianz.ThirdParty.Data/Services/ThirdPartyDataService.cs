using Allianz.ThirdParty.Data.AppSettingsManager;
using Allianz.ThirdParty.Data.Interfaces;
using Allianz.ThirdParty.Data.Model;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allianz.ThirdParty.Data.Services
{
    public class ThirdPartyDataService : IThirdPartyDataService
    {
        private readonly ILogger<ThirdPartyDataService> _logger;
        private readonly ConnectionStrings _configValue;
        private readonly SqlConnection _allianzDb;

        public ThirdPartyDataService(IOptions<ConnectionStrings> allianzDb, ILogger<ThirdPartyDataService> logger)
        {
            _configValue = allianzDb.Value;
            _allianzDb = new SqlConnection(_configValue.AllianzConnectionString) ?? throw new ArgumentNullException(nameof(allianzDb));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> LogThirdPartyData(UserVehicle thirdPartyData)
        {
            string methodName = "LogThirdPartyData";

            try
            {
                var insertModel = await _allianzDb.InsertAsync(thirdPartyData);

                return insertModel > 0;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"ClassName: {GetThisClassName()}| MethodName: {methodName}| Message: An exception occurred : {ex}");
                return false;
            }

        }

        public async Task<long> LogUserData(Users userData)
        {
            string methodName = "LogUserData";

            try
            {
                var insertModel = await _allianzDb.InsertAsync(userData);

                return insertModel;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"ClassName: {GetThisClassName()}| MethodName: {methodName}| Message: An exception occurred : {ex}");
                return 0;
            }

        }

        public async Task<bool> RemoveUserData(Users userData)
        {
            string methodName = "RemoveUserData";
            try
            {
                return await _allianzDb.DeleteAsync(userData);
            }
            catch (Exception ex)
            {

                _logger.LogCritical($"ClassName: {GetThisClassName()}| MethodName: {methodName}| Message: An exception occurred : {ex}");
                return false;
            }
        }

        protected string GetThisClassName() => GetType().Name;
    }
}
