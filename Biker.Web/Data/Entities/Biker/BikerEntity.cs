using System.Collections.Generic;

namespace Biker.Web.Data.Entities.Biker
{
    public class BikerEntity
    {
        public int Id { get; set; }

        public UserEntity UserEntity { get; set; }

        public ICollection<BikerMotorEntity> BikerMotors { get; set; }



    }
}
