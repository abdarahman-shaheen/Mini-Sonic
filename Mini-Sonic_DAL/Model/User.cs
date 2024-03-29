﻿using System.ComponentModel.DataAnnotations;

namespace Mini_Sonic.Model
{
    public class User
    {
        public int Id { get; set; }


        public string ?UserName { get; set; }

        public string Email { get; set; }


        public string Password { get; set; }

        public string ?Role { get; set; }
        public string ?Token { get; set; }
    }
}
