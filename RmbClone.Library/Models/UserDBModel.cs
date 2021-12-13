using System;
using System.Collections.Generic;
using System.Text;

namespace RmbClone.Library.Models
{
    public class UserDBModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FcmToken { get; set; }

        public UserDBModel()
        {
            Id = Guid.NewGuid().ToString();
        }

    }
}
