using System;

namespace FoodOderingApp
{
    public class Person
    {
         public int Id {get;set;} 
         public string FirstName  {get;set;} 
         public string LastName  {get;set;}  
         public string Email  {get;set;} 
         public string PhoneNumber  {get;set;} 
         public string Password  {get;set;}
          
          public Person(string fName, string lName, string email, string password,string phone)
          {
            
              FirstName = fName;
              LastName = lName;
              Email = email;
              PhoneNumber = phone;
              Password = password;
          }
         
            public Person(int id,string fName, string lName, string email, string password,string phone)
          {
              Id = id;
              FirstName = fName;
              LastName = lName;
              Email = email;
              PhoneNumber = phone;
              Password = password;
          }
          public Person()
          {
              
          }
    } 
    
}