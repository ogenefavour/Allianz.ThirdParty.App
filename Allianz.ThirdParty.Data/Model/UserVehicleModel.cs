using Allianz.ThirdParty.Data.Enum;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allianz.ThirdParty.Data.Model
{
    [Table("UserVehicle")]
    public class UserVehicleModel
    {
        public int UserVehicleId { get; set; }

        [Required]
        [Display(Name = "Vehicle Make")]
        [EnumDataType(typeof(VehicleMake))]
        public VehicleMake VehicleMake { get; set; }

        [Display(Name = "Select Honda Model")]
        [EnumDataType(typeof(HondaModel))]
        public HondaModel Honda { get; set; }

        [Display(Name = "Select Toyota Model")]
        [EnumDataType(typeof(ToyotaModel))]
        public ToyotaModel Toyota { get; set; }

        public int VehicleModel { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        [Required]
        [Display(Name = "Body Type")]
        public BodyType BodyType { get; set; }

        public decimal InsuranceFee { get; set; }

        public long UserId { get; set; }

        public Users Users { get; set; }

        
    }
}
