using Biker.Web.Data;
using Biker.Web.Data.Entities;
using Biker.Web.Data.Entities.MotorBike;
using Biker.Web.Models.MotorBike;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;

namespace Biker.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _dataContext;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(DataContext dataContext, ICombosHelper combosHelper)
        {
            _dataContext = dataContext;
            _combosHelper = combosHelper;
        }

        public async Task<MotorBikeEntity> ToMotorbikeEntityasync(AddMotorBikeViewModel model)
        {
            return new MotorBikeEntity
            {
                Id = (model.Id != 0) ? model.Id : 0,
                Name = model.Name,
                Cylinder = model.Cylinder,
                BikeMaker = await _dataContext.BikeMakers.FindAsync(model.MakerId),
                BikeType = await  _dataContext.BikeTypes.FindAsync(model.TypeId),
                WidthTireF = model.WidthTireF,
                HeightTireF = model.HeightTireF,
                FrontTire = model.FrontTire,
                WidthTireR = model.WidthTireR,
                HeightTireR = model.HeightTireR,
                RearTire = model.RearTire,
                Millimeters = model.Millimeters,
            };

        }

        public AddMotorBikeViewModel ToMotorBikeViewModel(MotorBikeEntity model)
        {
            return new AddMotorBikeViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Cylinder = model.Cylinder,
                BikeMaker = model.BikeMaker,
                BikeType = model.BikeType,
                WidthTireF = model.WidthTireF,
                HeightTireF = model.HeightTireF,
                FrontTire = model.FrontTire,
                WidthTireR = model.WidthTireR,
                HeightTireR = model.HeightTireR,
                RearTire = model.RearTire,
                Millimeters = model.Millimeters,
                MakerId = model.BikeMaker.Id,
                Makers = _combosHelper.GetComboMakers(),
                TypeId = model.BikeType.Id,
                Types = _combosHelper.GetComboTypes(),
    
            };

        }

        public async Task<MotorBikeSpareEntity> ToMotorbikeSpareEntityasync(
            AddMotorBikeSpareViewModel model,
            string path,
            bool Isnew)
        {
            
            return new MotorBikeSpareEntity
            {
                YearSince = model.YearSince,
                YearUntil = model.YearUntil,
                ImageUrl = path,
                MotorBike = await _dataContext.MotorBikes.FindAsync(model.MotorbikeId),
                Id = Isnew ? 0 : model.Id
            };
        }

        public AddMotorBikeSpareViewModel ToMotorBikeSpareViewModel(MotorBikeSpareEntity model)
        {
            return new AddMotorBikeSpareViewModel
            {
                Id = model.Id,
                BikerMotors = model.BikerMotors,
                ImageUrl = model.ImageUrl,
                MotorBike = model.MotorBike,
                MotorbikeId = model.MotorBike.Id,
                YearSince = model.YearSince,
                YearUntil = model.YearUntil
            };
        }

        public BikeMakerEntity ToBikeMakerEntity(AddBikeMakerViewModel model, string path)
        {

            return new BikeMakerEntity
            {
                Id = model.Id,
                Name = model.Name,
                ImageUrl = path                
            };
        }

        public AddBikeMakerViewModel ToBikeMakerViewModel(BikeMakerEntity model)
        {
            return new AddBikeMakerViewModel
            {
                Id = model.Id,
                ImageUrl = model.ImageUrl,
                Name = model.Name
                
            };
        }


    }
}
