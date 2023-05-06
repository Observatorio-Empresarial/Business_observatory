﻿namespace Business_observatory.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; } = null;
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
    }
}
