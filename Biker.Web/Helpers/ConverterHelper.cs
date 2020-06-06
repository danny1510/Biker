using Biker.Web.Data;
using Biker.Web.Data.Entities;
using Biker.Web.Data.Entities.MotorBike;
using Biker.Web.Models.MotorBike;
using System.Threading.Tasks;

namespace Biker.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _dataContext;

        public ConverterHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<MotorBikeEntity> ToMotorbikeasync(AddMotorBikeViewModel model)
        {
            return new MotorBikeEntity
            {
                Name = model.Name,
                Cylinder = model.Cylinder,
                BikeMaker = await _dataContext.BikeMakers.FindAsync(model.MakerId),
                BikeType = await _dataContext.BikeTypes.FindAsync(model.TypeId),
                WidthTireF = model.WidthTireF,
                HeightTireF = model.HeightTireF,
                FrontTire = model.FrontTire,
                WidthTireR = model.WidthTireR,
                HeightTireR = model.HeightTireR,
                RearTire = model.RearTire,
                Millimeters = model.Millimeters,
            };

        }

        public async Task<MotorBikeSpareEntity> ToMotorbikeSpareasync(AddMotorBikeSpareViewModel model, string path)
        {
            return new MotorBikeSpareEntity
            {
               YearSince = model.YearSince,
               YearUntil = model.YearUntil,
               ImageUrl = path,
               MotorBike = await _dataContext.MotorBikes.FindAsync(model.MotorbikeId)
            };

        }








    }
}
