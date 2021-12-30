using Allianz.ThirdParty.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allianz.ThirdParty.Data.Interfaces
{
    public interface IThirdPartyDataService
    {
        Task<bool> LogThirdPartyData(UserVehicle thirdPartyData);
        Task<long> LogUserData(Users userData);
        Task<bool> RemoveUserData(Users userData);
    }
}
