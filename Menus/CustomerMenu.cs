using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace FoodOderingApp.Enums
{
     public class CustomerMenu
    {
       static FoodRepo foodRepo = new FoodRepo();
      static  OrderRepo orderRepo = new OrderRepo();
      static  CustmerRepo custmerRepo = new CustmerRepo();
       public void CustomerOptionMenu()
      {
        System.Console.WriteLine("Enter 1 to Login\nEnter 2 to Register\nEnter 0 to back to Main Menu");
        int option;
        while(!int.TryParse(Console.ReadLine(),out option))
        {
          System.Console.WriteLine("Invalid input. Try again");
        }
        switch(option)
        {
          case 1:
          Login();
          break;
          case 2:
          Register();
          break;
          case 0:
          MainMenu.WelcomePage();
          break;
          default:
          Console.WriteLine("Invalid input");
          CustomerOptionMenu();
          return;
        }
      }
      public void Login()
      {
         Console.WriteLine("Enter Email: ");
         string email = Console.ReadLine();
         Console.WriteLine("Enter Password: ");
         string password = Console.ReadLine();
         var customer = custmerRepo.Login(email,password);
         
         if (customer == null)
         {
           Console.WriteLine("This account was not found. please register an account. Enter any key to register");
           Console.ReadKey();
           Register();
         }
         else
         {
            Console.WriteLine("Log-in successfuly...");
            Console.Beep();
            AfterCustomerLogin(customer);
         }
      }
         public static void AfterCustomerLogin(Customer customer)
         {
           CMenu:
         Console.WriteLine("1. To check available foods\n2. To order for food\n3. To check Wallet\n4. To Update profile\n5. To delete account\n0 To Main Menu");
         int option;
         while(!int.TryParse(Console.ReadLine(),out option))
         {
           Console.WriteLine("Invalid input. Try again");
         }
         switch(option)
         {
           case 1:
          foodRepo.PrintOnlyReadyFood();
          break;
          case 2:
          orderRepo.OrderFood(customer);
          break;
          case 3:
          custmerRepo.CheckWallet(customer);
          break;
          case 4:
          UpdateProfile();
          break;
          case 5:
          Confirmation(customer);
          break;
          case 0:
          MainMenu.WelcomePage();
          break;
          default:
           Console.WriteLine("Invalid Input. Try again");
           goto CMenu;
         }
      }
        public void Register()
       {
          System.Console.WriteLine("Enter first name");
          string fName = Console.ReadLine();
          System.Console.WriteLine("Enter Last name");
          string lName= Console.ReadLine();
          System.Console.WriteLine("Enter your Email");
          string email = Console.ReadLine();
          System.Console.WriteLine("Enter your phone number");
          string phone = Console.ReadLine();
          System.Console.WriteLine("Enter password");
          string password = Console.ReadLine();
          Console.WriteLine("Enter your address");
          string address = Console.ReadLine();
          custmerRepo.Register(fName,lName,email,password,phone,address);
          ConfirmationReg();
      }
      public void ConfirmationReg()
      {
        Console.WriteLine("Enter your email to confirm your registration");
        string email = Console.ReadLine();
         Console.WriteLine("Enter your password to confirm your registration");
         string password = Console.ReadLine();
         var customer = custmerRepo.Login(email,password);
          if (customer == null)
         {
           Console.WriteLine("Email or password not match. Enter any key to register again");
           Console.ReadKey();
           Register();
         }
         else
         {
            Console.WriteLine("Log-in successfuly...");
            AfterCustomerLogin(customer);
         }
      }
      public static void UpdateProfile()
      {
         Console.WriteLine("Enter your email to confirm it is you");
         string mail = Console.ReadLine();
         var customer = custmerRepo.GetCustomerByEmail(mail);
         if(customer == null)
         {
            Console.WriteLine("Invalid Email");
            UpdateProfile();
         }
         else
         {
          Console.WriteLine("Enter FirstName");
          string firstName = Console.ReadLine();
          Console.WriteLine("Enter LastName");
          string lastName = Console.ReadLine();
          Console.WriteLine("Enter Email");
          string email = Console.ReadLine();
          Console.WriteLine("Phone number");
          string phone = Console.ReadLine();
          Console.WriteLine("Enter Address");
          string address = Console.ReadLine();
          custmerRepo.Update(mail,firstName,lastName,email,phone,address);
          AfterCustomerLogin(customer);
         }
      }
      public static void Confirmation(Customer customer)
      {
        Console.WriteLine("Are you sure you want to Delete your account??.Enter yes or no");
        string option  = Console.ReadLine().ToLower();
        switch(option)
        {
          case "yes":
          custmerRepo.DeleteCustomer(customer);
          break;
          case "no":
          AfterCustomerLogin(customer);
          break;
        }
      }
    }
}