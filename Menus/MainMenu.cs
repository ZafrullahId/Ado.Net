using System;

namespace  FoodOderingApp.Enums
{
    public class MainMenu
    {
       static CustomerMenu customerMenu = new CustomerMenu();
        static StaffMenu staffMenu = new StaffMenu();
        public MainMenu()
        {
           
        }
        public static void WelcomePage()
        {
          Console.WriteLine("==================================================");
          Console.WriteLine("## WELCOME #######################################");
          Console.WriteLine("====== TO ========================================");
          Console.WriteLine("########## CLH ###################################");
          Console.WriteLine("<<<<<<<<<<<< FOOD >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
          Console.WriteLine("################## ODERING #######################");
          Console.WriteLine("======================== CONSOLE =================");
          Console.WriteLine("############################## APP ###############");
          Console.WriteLine("==================================================");
          Main:
          Console.WriteLine("Enter 1 to Customer Menu \nEnter 2 to Staff Menu\nEnter 0 To exit");
          int option;
          while(!int.TryParse(Console.ReadLine(),out option))
          {
              Console.WriteLine("Try again");
          }
          switch(option)
          {
              case 1:
              customerMenu.CustomerOptionMenu();
              break;
              case 2:
              staffMenu.StaffOptionMenu();
              break;
              case 0:
              break;
              default:
              Console.WriteLine("Invalid input");
              goto Main;
              
          }
        }
    }
}