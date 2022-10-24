using System;
using MySql.Data.MySqlClient;
namespace FoodOderingApp.Enums
{
    public class CustmerRepo
    {
         string connect = "server = localhost;user = root;database = ConsoleApp;port = 3306;password = zafuolobeyo1@";
         MySqlConnection connection;
          public OrderRepo orderRepo = new OrderRepo();
        public CustmerRepo()
        {
           
        } 
         public void Register(string fName, string lName, string email,string password, string phone,string address)
         {
           decimal wallet = 0.00m;
           var customer = new Customer(fName,lName,email,password,phone,address,wallet);
           InsertStudent(customer); 
           Console.Beep();  
           Console.WriteLine($"You have successfully created an account and your customer number is {customer.CustomerNo}");
           Console.WriteLine($"We have given you a bonus of {customer.Wallet}.");
         }
          public void Update(string formerEmail,string fName, string lName, string email,string phone,string address)
         {
            var customer1 = new Customer(fName,lName,email,phone,address);
            UpdateCustomer(formerEmail,customer1);
            Console.WriteLine($"You have successfully updated your account");
         }
         
         public Customer Login(string email, string password)
        {
           var  customer = GetCustomerByEmail(email);
              if(customer != null && customer.Email == email && customer.Password == password)
              {
                return customer;
              }          
           return null;
        }
        public void CheckWallet(Customer customer)
        {
           
            Console.WriteLine("Your wallet balance is " + "#" + customer.Wallet + ".00");
            CWallet:
            Console.WriteLine("Would you like to fund your wallet\n1. To fund wallet  2. To order food  0. To Main Main");
            int option;
            while(!int.TryParse(Console.ReadLine(),out option))
            {
                Console.WriteLine("Invalid option. Try again");
            }
            switch(option)
            {
                case 1:
                orderRepo.FundWalletWithoutOrder(customer);
                break;
                case 2:
                orderRepo.OrderFood(customer);
                break;
                case 0:
                MainMenu.WelcomePage();
                break;
                default:
                goto CWallet;              
            }
        }
         public void AfterFundWallet(Customer customer)
        {
           Console.WriteLine("1. To Order food\n0. To Main Menu");
           int option;
           while(!int.TryParse(Console.ReadLine(),out option))
           {
               Console.WriteLine("Invalid Input. Try again");
           }
           switch(option)
           {
               case 1:
               orderRepo.OrderFood(customer);
               break;
               case 0:
               MainMenu.WelcomePage();
               break;
               default:
               AfterFundWallet(customer);
               return;
           }
          
        }
         public void InsertStudent(Customer customer)
        {           
           try
           {
               connection = new MySqlConnection(connect);
               MySqlCommand command = new MySqlCommand($"Insert into Customer (FirstName,LastName,Email,Passwords,Phone,Address,Wallet,CustomerNo) values ('{customer.FirstName}','{customer.LastName}','{customer.Email}','{customer.Password}','{customer.PhoneNumber}','{customer.Address}',{customer.Wallet},'{customer.CustomerNo}')",connection);
               connection.Open();
               command.ExecuteNonQuery();
               Console.WriteLine("Table Inserted successfully");
           }
           catch (Exception e)
           {
                Console.WriteLine(e.Message);
           }
           finally
           {
               connection.Close();
           } 
        }
         public Customer GetCustomerByEmail(string mail)
        {
            Customer customer1 = null;
              try
           {
               connection = new MySqlConnection(connect);
               MySqlCommand command = new MySqlCommand($"Select * from Customer where Email = '{mail}';",connection);
               connection.Open();
               MySqlDataReader reader = command.ExecuteReader();
               while(reader.Read())
               {
                 var id = reader.GetInt32(0);
                 var firstname = reader.GetString(1);
                 var lastName = reader.GetString(2);
                 var email = reader.GetString(3);
                 var password = reader.GetString(4);
                 var phoneNumber = reader.GetString(5);
                 var address = reader.GetString(6);
                 var wallet = reader.GetDecimal(7);
                 var customerNo = reader.GetString(8);
                 customer1 = new Customer(id,firstname,lastName,email,password,phoneNumber,address,wallet);                       
               }          
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            connection.Close();
        }
         return customer1;
        }
         public void UpdateCustomer(string email, Customer customer)
         {
           
             try
           {
               connection = new MySqlConnection(connect);
               MySqlCommand command = new MySqlCommand($"Update Customer set `FirstName` = '{customer.FirstName}',`LastName` = '{customer.LastName}',`Email` = '{customer.Email}',`Phone` = '{customer.PhoneNumber}',`Address` = '{customer.Address}' where `Email` = '{email}'",connection);
               connection.Open();
               command.ExecuteNonQuery();
               Console.WriteLine("Updated successfully...");
           }
            catch(Exception e)
          {
            Console.WriteLine(e.Message);
          }
           finally
          {
            connection.Close();
          }
       }
        public void DeleteCustomer(Customer customer)
        {
            
            try
           {
               connection = new MySqlConnection(connect);
               MySqlCommand command = new MySqlCommand($"Delete from Customer where Email = '{customer.Email}'",connection);
               connection.Open();
               command.ExecuteNonQuery();
               Console.WriteLine("Deleted successfully");
               MainMenu.WelcomePage();
           }
           catch (Exception e)
           {
                Console.WriteLine(e.Message);
           }
           finally
           {
               connection.Close();
           } 
        }
    } 
}