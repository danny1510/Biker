namespace Biker.Web.Data.Entities.Biker
{
    public class BikerMotorEntity
    {
        public int Id { get; set; }

        public BikerEntity Biker { get; set; }

        public MotorBikeSpareEntity MotorBikeSpare { get; set; }



    }
}
