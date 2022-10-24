using System;
using MySql.Data.MySqlClient;

namespace FoodOderingApp.Enums
{
     public class OrderRepo
    {
         string connect = "server = localhost;user = root;database = ConsoleApp;port = 3306;password = zafuolobeyo1@";
         MySqlConnection connection;
         FoodRepo foodRepo = new FoodRepo();
         List<Food> carts = new List<Food>();
        decimal TotalAmount = 0.00m;
        public  void OrderFood(Customer customer1)
        {
            
            foodRepo.PrintAllReadyFood();
            Console.WriteLine("Please choose your food choice by their corresponding numbers");
            int choosedId;
            while (!int.TryParse(Console.ReadLine(), out choosedId))
            {
                Console.WriteLine("Enter a valid option");
            }
            var food = foodRepo.GetFoodType(choosedId);
            if (food == null)
            {
                Console.WriteLine("Food not Available");
                OrderFood(customer1);
            }
             Console.WriteLine("How many plates would you like to order");
            int numberOfSpoon;
            while (!int.TryParse(Console.ReadLine(), out numberOfSpoon))
            {
                Console.WriteLine("Invalid input. Try again");
            }
            if (numberOfSpoon > 7)
            {
                Console.WriteLine("The maximum number of plates is 7. Please Try again");
                OrderFood(customer1);
            }
            Start:
            Console.WriteLine("Do you like to Add to cart. \nEnter 1 for yes and 2 for no");
            
             TotalAmount += food.Price * numberOfSpoon;
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Try again");
            }
            switch (choice)
            {
                case 1:
                carts.Add(food);
                OrderFood(customer1);
                break;
                case 2:
                carts.Add(food);
                Console.WriteLine("Total amount is " + "#" + TotalAmount);
                Again:
                Console.WriteLine("1. To proceed\n2. To cancel order\n3. To view cart");
                int opt = int.Parse(Console.ReadLine());
                switch(opt)
                {
                    case 1:
                    break;
                    case 2:
                    TotalAmount = 0;
                    MainMenu.WelcomePage();
                    break;
                    case 3:
                    CheckCart(numberOfSpoon);
                    break;
                    default:
                    goto Again;
                }
                if (customer1.Wallet < TotalAmount)
                {
                    Restart:
                    Console.WriteLine("Insuficient funds. Enter 1 to fund wallet. 0 to Cancel order");
                    int option;
                    while(!int.TryParse(Console.ReadLine(), out option))
                    {
                        Console.WriteLine("Invalid input. Try again");
                    }
                    switch(option)
                    {
                        case 1:
                        FundWallet(customer1);
                        customer1.Wallet = customer1.Wallet - TotalAmount;
                        Console.WriteLine("Thank you for your patronage. Your wallet balance is " +"#" + customer1.Wallet);
                        UpdateWallet(customer1.Wallet,customer1.Email);
                        TotalAmount = 0;
                        Insert(customer1,food);
                        Console.WriteLine("Enter any key to log-out");
                        Console.ReadKey();
                        MainMenu.WelcomePage();
                        break;
                        case 0:
                        TotalAmount = 0;
                        MainMenu.WelcomePage();
                        break;
                        default:
                        goto Restart;
                    }
                }
                else
                {
                     customer1.Wallet = customer1.Wallet - TotalAmount;
                     TotalAmount = 0;
                     Insert(customer1,food);
                     Console.WriteLine("Thank you for your patronage. Your wallet balance is " +"#" + customer1.Wallet);
                     UpdateWallet(customer1.Wallet,customer1.Email);
                      Console.WriteLine("Enter any key to log out");
                      Console.ReadKey();
                      MainMenu.WelcomePage();
                }
                 break;
                 default:
                 goto Start;
            }
        }
        public void CheckCart(int numberOfSpoon)
        {
            foreach (var food in carts)
            {
                Console.WriteLine(food.FoodId + "." + " " + food.FoodTypeName + "  " + food.Price);
            }
            viewCart();
        }
        public void viewCart()
        {
            Console.WriteLine("1. To proceed\n2. To remove food from carts");
            int opt;
            while(!int.TryParse(Console.ReadLine(),out opt))
            {
                System.Console.WriteLine("Invalid option");
            }
            switch(opt)
            {
                case 1:
                break;
                case 2:
                RemoveFood();
                break;
            }
        }
        public void RemoveFood()
        {
            int count = 1;
             foreach (var food in carts)
            {
                Console.Write(count++ + "." + " ");
                Console.WriteLine(food.FoodTypeName + "  " + food.Price);
            }
            
            Console.WriteLine("Enter food Id");
            int opt = int.Parse(Console.ReadLine());
            carts.Remove(carts[opt -1]);
             foreach (var food in carts)
            {
                
                Console.WriteLine(food.FoodId + "." + " " + food.FoodTypeName + "  " + food.Price);
                // TotalAmount += food.Price * numberOfSpoon;
            }
        }
        public void FundWallet(Customer customer1)
       {
           Console.WriteLine("Enter amount to credit your wallet");
           decimal amount = decimal.Parse(Console.ReadLine());
           customer1.Wallet += amount;
           Console.WriteLine($"Wallet successfully funded. Your total Wallet amount is #{customer1.Wallet}.");    
           UpdateWallet(customer1.Wallet,customer1.Email);    
       }
        public void FundWalletWithoutOrder(Customer customer1)
       {
           Console.WriteLine("Enter amount to credit your wallet");
           decimal amount = decimal.Parse(Console.ReadLine());
           customer1.Wallet += amount;
          Console.WriteLine($"Wallet successfully funded. Your total Wallet amount is #{customer1.Wallet}.");
          UpdateWallet(customer1.Wallet,customer1.Email);
           AfterFundWallet(customer1); 
       }
         public void UpdateWallet(decimal wallet, string email)
        { 
             try
           {
               connection = new MySqlConnection(connect);
               MySqlCommand command = new MySqlCommand($"Update Customer set Wallet = '{wallet}' where Email = '{email}'",connection);
               connection.Open();
               command.ExecuteNonQuery();
               Console.WriteLine("Table Updated successfully");
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
        public void AfterFundWallet(Customer customer1)
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
               OrderFood(customer1);
               break;
               case 0:
               MainMenu.WelcomePage();
               break;
               default:
               AfterFundWallet(customer1);
               return;
           }
        }
         public void Insert(Customer customer, Food food)
        {           
           try
           {
               connection = new MySqlConnection(connect);
               MySqlCommand command = new MySqlCommand($"Insert into CustomerFood (CustomerId,FoodId) values ('{customer.Id}','{food.FoodId}')",connection);
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
    }
}