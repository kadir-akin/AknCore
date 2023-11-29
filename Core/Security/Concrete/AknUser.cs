﻿using Core.Security.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Security.Concrete
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

        public string AuthenticationType => AuthotanticationType.BASIC.ToString();

        public bool IsAuthenticated => true;

        public string Name => FirsName + " "+ LastName;
    }
}
