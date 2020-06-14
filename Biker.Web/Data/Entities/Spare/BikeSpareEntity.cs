namespace Biker.Web.Data.Entities.Spare
{
    public class BikeSpareEntity
    {
        public int Id { get; set; }

        public MotorBikeSpareEntity MotorBikeSpare { get; set; }

        public SpareEntity Spare { get; set; }

    }
}
