using System;
using System.Collections.Generic;
using System.Text;

namespace RmbClone.Library.Models.Responses
{
    public class AuthResponseModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
