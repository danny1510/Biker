using Biker.Web.Data;
using Biker.Web.Data.Entities;
using Biker.Web.Data.Entities.MotorBike;
using Biker.Web.Data.Entities.Provider;
using Biker.Web.Models.MotorBike;
using Biker.Web.Models.Provider;
using Biker.Web.Models.Spare;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
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
                TypeMaker = await _dataContext.TypeMakers
                .Include(tm=> tm.BikeMaker)
                .Include(tm => tm.BikeType)
                .FirstOrDefaultAsync(tm => tm.BikeMaker.Id == model.MakerId && tm.BikeType.Id == model.TypeId), //TODO: REVISAR
                WidthTireF = model.WidthTireF,
                HeightTireF = model.HeightTireF,
                FrontTire = model.FrontTire,
                WidthTireR = model.WidthTireR,
                HeightTireR = model.HeightTireR,
                RearTire = model.RearTire,
                Millimeters = model.Millimeters

            };

        }

        public AddMotorBikeViewModel ToMotorBikeViewModel(MotorBikeEntity model)
        {
            return new AddMotorBikeViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Cylinder = model.Cylinder,
                WidthTireF = model.WidthTireF,
                HeightTireF = model.HeightTireF,
                FrontTire = model.FrontTire,
                WidthTireR = model.WidthTireR,
                HeightTireR = model.HeightTireR,
                RearTire = model.RearTire,
                Millimeters = model.Millimeters,
                MakerId = model.TypeMaker.BikeMaker.Id,
                Makers = _combosHelper.GetComboMakers(),
                TypeId = model.TypeMaker.BikeType.Id,
                Types = _combosHelper.GetComboTypes(),
                TypeMaker = model.TypeMaker
    
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

        public SpareBrandEntity ToSpareBrandEntity(AddSpareBrandViewModel model, string path)
        {
            return new SpareBrandEntity
            {
                Id = model.Id,
                Name = model.Name,
                ImageUrl = path
            };
        }

        public SpareCategoryEntity ToSpareCategoryEntity(AddSpareCategoriesViewModel model, string path)
        {
            return new SpareCategoryEntity
            {
                Id = model.Id,
                Name = model.Name,
                ImageUrl = path
            };
        }

        public ProviderEntity ToProviderEntity(AddProviderViewModel model, string path)
        {
            return new ProviderEntity
            {
                Id = model.Id,
                Name = model.Name,
                ImageUrl = path
            };
        }


    }
}
