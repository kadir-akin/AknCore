using Core.Security.Abstract;

namespace Template_Api
{
    public class AknUser : IAknUser
    {
        public int UserId { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneAreaCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Roles { get; set; }

        public string AuthenticationType => Core.Security.Concrete.AuthotanticationType.BASIC.ToString();

        public bool IsAuthenticated => true;

        public string Name => FirsName + " " + LastName;
    }

   
}
