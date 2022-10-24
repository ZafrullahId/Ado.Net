using System;

namespace FoodOderingApp.Dto
{
    public class CustomerDto
    {
        public int Id {get; set;}
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public string Email {get; set;}
        public string PhoneNumber {get;set;}
        public string Address {get;set;}
        public CustomerDto(int id, string firstName,string lastName,string email,string phoneNumber,string address)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }
    }
}