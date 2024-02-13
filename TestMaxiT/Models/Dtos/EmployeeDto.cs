﻿namespace TestMaxiT.Models.Dtos
{
    public class EmployeeDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime Birthday { get; set; }
        public int EmployeeNumber { get; set; }
        public string? CURP { get; set; }
        public int SSN { get; set; }
        public int PhoneNumber { get; set; }
        public string? Nationality { get; set; }
    }
}
