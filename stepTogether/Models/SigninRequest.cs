﻿namespace stepTogether.Models
{
    public class SignupRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
    }

}
