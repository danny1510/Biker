using Biker.Web.Data.Entities;
using Biker.Web.Data.Entities.MotorBike;
using Biker.Web.Models.MotorBike;
using System.Threading.Tasks;

namespace Biker.Web.Helpers
{
    public interface IConverterHelper
    {
        Task<MotorBikeEntity> ToMotorbikeasync(AddMotorBikeViewModel model);
        Task<MotorBikeSpareEntity> ToMotorbikeSpareasync(AddMotorBikeSpareViewModel model,string path);
    }
}