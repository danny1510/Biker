using Biker.Web.Data.Entities;
using Biker.Web.Data.Entities.MotorBike;
using Biker.Web.Models.MotorBike;
using System.Threading.Tasks;

namespace Biker.Web.Helpers
{
    public interface IConverterHelper
    {
        Task<MotorBikeEntity> ToMotorbikeEntityasync(AddMotorBikeViewModel model);
        Task<MotorBikeSpareEntity> ToMotorbikeSpareEntityasync(AddMotorBikeSpareViewModel model,string path,bool Isnew);
        AddMotorBikeViewModel ToMotorBikeViewModel(MotorBikeEntity model);
        AddMotorBikeSpareViewModel ToMotorBikeSpareViewModel(MotorBikeSpareEntity model);
        BikeMakerEntity ToBikeMakerEntity(AddBikeMakerViewModel model, string path);
        AddBikeMakerViewModel ToBikeMakerViewModel(BikeMakerEntity model);
    }
}