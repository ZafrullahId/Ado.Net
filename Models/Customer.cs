using System;

namespace FoodOderingApp.Enums
{
   public class Customer:Person
    {
  
         public decimal Wallet {get;set;}
         public string CustomerNo {get;set;}
         public string Address {get;set;}
        public Customer(string fName,string lName, string email,string password,string phone,string adress,decimal wallet):base (fName,lName,email,password,phone)
       {
         
         Wallet = wallet;
         Address = adress;
         CustomerNo = $"CU{Guid.NewGuid().ToString().Replace("_", "").Substring(0,5).ToUpper()} ";;
       }
       public Customer()
       {
         
       }
       public Customer(string firstName,string lastName,string email,string phone,string adress):base()
       {
        
       }
       public Customer(int id,string fName,string lName, string email,string password,string phone,string adress,decimal wallet):base (id,fName,lName,email,password,phone)
       {
         
         Wallet = wallet;
         Address = adress;
         CustomerNo = $"CU{Guid.NewGuid().ToString().Replace("_", "").Substring(0,5).ToUpper()} ";;
       }
        
    }
}