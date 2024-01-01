using Core.Database.EF.Abstract;

namespace Template_Api
{
    public class UserEntity :IEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
