using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Security.Abstract
{
    public interface IAknUser
    {
        public int UserId { get; set; }

        public string FirsName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneAreaCode { get; set; }

        public string PhoneNumber { get; set; }
    }
}
