using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allianz.ThirdParty.Data.Model
{
    [Table("UserVehicle")]
    public class UserVehicle
    {
        [Key]
        public int UserVehicleId { get; set; }

        public int VehicleMake { get; set; }

        public int VehicleModel { get; set; }

        public string RegistrationNumber { get; set; }

        public string BodyType { get; set; }

        public decimal InsuranceFee { get; set; }

        public long UserId { get; set; }
    }
}
