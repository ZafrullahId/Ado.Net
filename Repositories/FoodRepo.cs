using System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace FoodOderingApp.Enums
{
    class FoodRepo
    {
        
         MySqlConnection connection;

        string connect = "server = localhost;user = root;database = ConsoleApp;port = 3306;password = zafuolobeyo1@";
         public  FoodRepo()
        {
            
        } 
        public void PrintAllReadyFood()
        {
            var foods = GetAllFood();
            foreach (var food in foods)
            {
                    if (foods[0] != null)
                {
                     Console.Write(food.FoodId + "." + " ");
                     Console.Write("Food Name: " + food.FoodTypeName + " ");
                     Console.Write("Price: " + food.Price + " per plate");
                     Console.WriteLine();
                    
                }
                 else if(foods[0] == null)
                {
                    Console.WriteLine("No food available.Enter any key back to Main Menu");
                    Console.ReadKey();
                    MainMenu.WelcomePage();
                }
            }
        }
         public void AddFood()
        {
             START:
            Console.WriteLine("Enter food name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter food price per plate");
            decimal price;
            while(!decimal.TryParse(Console.ReadLine(),out price))
            {
                Console.WriteLine("invalid price input. Try again.");
            }
            var food = new Food(name, price);
            InsertFood(food);
            Console.WriteLine("Food type successfuly addad to list...");
            Console.WriteLine("Would you like to add another food.\n1. yes\n2. no\n0. To Main Menu");
            int option;
            while(!int.TryParse(Console.ReadLine(),out option))
            {
                Console.WriteLine("Try again");
            }
            switch(option)
            {
                case 1:
                goto START;
                case 2:
                AfterPrintAllReadyFood();
                break;
                case 0:
                MainMenu.WelcomePage();
                break;
                default:
                AfterPrintAllReadyFood();
                return;
            }
        }
        public void AfterPrintAllReadyFood()
        {
           var foods =  GetAllFood();
             Console.WriteLine("This are the foods added");
           foreach (var food in foods)
            {
                    Console.Write(food.FoodId + "." + "    ");
                     Console.Write("Food Name: " + food.FoodTypeName + "    ");
                     Console.Write("Price: " + food.Price + " per plate");
                     Console.WriteLine();
            }
            Pfood:
            Console.WriteLine("Enter 0 To log out");
            int option;
            while(!int.TryParse(Console.ReadLine(),out option))
            {
                Console.WriteLine("Invalid input. Try again");
            }
            switch(option)
            {
                case 0:
                MainMenu.WelcomePage();
                break;
                default:
               goto Pfood;
            }
        }
         public void PrintOnlyReadyFood()
         {
            var foods = GetAllFood();
            foreach(var food in foods)
            {
                if (foods[0] != null)
                {
                     Console.Write(food.FoodId + "." + " ");
                     Console.Write("Food Name: " + food.FoodTypeName + " ");
                     Console.Write("Food Name: " + food.Price + " per plate");
                     Console.WriteLine();
                    
                }
                 else if(foods[0] == null)
                {
                    Console.WriteLine("No food available.Enter any key back to Main Menu");
                    Console.ReadKey();
                    MainMenu.WelcomePage();
                }
            }
             PFood:
            Console.WriteLine("Enter 0 to main menu \nEnter 1 To go Back");
            int option;
            while(!int.TryParse(Console.ReadLine(),out option))
            {
                Console.WriteLine("Invalid input");
            }    
            switch(option)
            {
                case 0:
                MainMenu.WelcomePage();
                break;
                case 1:
                CustomerMenu.AfterCustomerLogin(new Customer());
                break;
                default:
                goto PFood;
            }
         }
          public void UpdateFood(int id)
        {
             Console.WriteLine("Enter food name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter food price per plate");
            decimal price;
            while(!decimal.TryParse(Console.ReadLine(),out price))
            {
                Console.WriteLine("invalid price input. Try again.");
            }
             try
           {
               connection = new MySqlConnection(connect);
               MySqlCommand command = new MySqlCommand($"Update Food set `FoodName` = '{name}', `FoodPrice` = '{price}' where FoodId = '{id}'",connection);
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
        public void ChangeFood(int id)
        {
             var foods = GetAllFood();
              foreach (var food in foods)
            {
                    if (foods[0] != null)
                {
                     Console.Write(food.FoodId + "." + " ");
                     Console.Write("Food Name: " + food.FoodTypeName + " ");
                     Console.Write("Price: " + food.Price + " per plate");
                     Console.WriteLine();
                    
                }
                 else if(foods[0] == null)
                {
                    Console.WriteLine("No food available.Enter any key back to Main Menu");
                    Console.ReadKey();
                    MainMenu.WelcomePage();
                }
            }
             Console.WriteLine("Enter food id to be updated");
              id = int.Parse(Console.ReadLine());
             Console.WriteLine("1. To change food\n2. To Remove food from list");
             int opt;
             while(!int.TryParse(Console.ReadLine(),out opt))
             {
                 Console.WriteLine("Invalid Input. Try agin");
             }
             switch(opt)
             {
                 case 1:
                 UpdateFood(id);
                 break;
                 case 2:
                DeleteFood(id);
                break;
             }
                 Update:
                  Console.WriteLine("Would you like to update another food.\n1. yes\n2. no");
                 int op;
             while(!int.TryParse(Console.ReadLine(),out op))
             {
                 Console.WriteLine("Invalid Input. Try agin");
             }
             switch(op)
             {
                 case 1:
                 ChangeFood(id);
                 break;
                 case 2:
                 AfterPrintAllReadyFood();
                 break;
                 default:
                 goto Update;
             }             
        }
         public void DeleteFood(int id)
        {
            try
           {
               connection = new MySqlConnection(connect);
               MySqlCommand command = new MySqlCommand($"Delete from Food where FoodId = '{id}'",connection);
               connection.Open();
               command.ExecuteNonQuery();
               Console.WriteLine("Table Delet successfully");
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
        public Food GetFoodType(int id)
        {
            var foods = GetAllFood();
            for (int i = 0; i < foods.Count; i++)
            {
                if(foods[i] != null && foods[i].FoodId == id)
                return foods[i];
            }
            return null;
        }
         public void InsertFood(Food food)
        {           
           try
           {
               connection = new MySqlConnection(connect);
               MySqlCommand command = new MySqlCommand($"Insert into Food (FoodName,FoodPrice) values ('{food.FoodTypeName}','{food.Price}')",connection);
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
         public List<Food> GetAllFood()
        {
             List<Food> foods = new List<Food>();

              try
           {
               connection = new MySqlConnection(connect);
               MySqlCommand command = new MySqlCommand("Select * from Food",connection);
               connection.Open();
               MySqlDataReader reader = command.ExecuteReader();
               while(reader.Read())
               {
                 var id = reader.GetInt32(0);
                 var name = reader.GetString(1);
                 var price = reader.GetDecimal(2);
                 var food = new Food(id,name,price);  
                 foods.Add(food);      
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
         return foods;
        }
        
    }
}