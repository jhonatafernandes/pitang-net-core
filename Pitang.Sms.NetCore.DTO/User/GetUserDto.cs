using System;
using System.Collections.Generic;
using System.Text;

namespace Pitang.Sms.NetCore.DTO.User
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public string Role { get; set; }


    }
}
