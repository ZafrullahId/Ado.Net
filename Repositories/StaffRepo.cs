using System;
using MySql.Data.MySqlClient;

namespace FoodOderingApp.Enums
{
    public class StaffRepo
    {
         string connect = "server = localhost;user = root;database = ConsoleApp;port = 3306;password = zafuolobeyo1@";
         MySqlConnection connection;
       private static int count = 2;
       public StaffRepo()
       {
        //    var staff = new Staff (1, "Zafrullah", "Idris","zaf@gmail.com","password","08012345678",Gender.Male,Role.Manager);
        //    staffs[0] = staff;
       }
       public void AddNewStaff(string fName, string lName, string email, string phone, string password,Gender gender,Role role)
       {
       
           var staff = new Staff(count,fName,lName,email,password,phone,gender,role);
            InsertStaff(staff);
           Console.WriteLine($"Staff successfully added and staff number is {staff.StaffNo}");
       }
       public Staff Login(string email,string password)
       {
           var staff = GetStaffByEmail(email);
           if(staff != null && staff.Password == password && staff.Email == email)
           {
               return staff;
           }
           return null;
       }
        public void InsertStaff(Staff staff)
        {           
           try
           {
               connection = new MySqlConnection(connect);
               MySqlCommand command = new MySqlCommand($"Insert into Staff (FirstName,LastName,Email,PhoneNumber,Passwords,StaffNo,Roles,Gender) values ('{staff.FirstName}','{staff.LastName}','{staff.Email}','{staff.PhoneNumber}','{staff.Password}','{staff.StaffNo}','{staff.Role}','{staff.Gender}')",connection);
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
         public Staff GetStaffByEmail(string mail)
        {
            Staff staff = null;
              try
           {
               connection = new MySqlConnection(connect);
               MySqlCommand command = new MySqlCommand($"Select * from Staff where Email = '{mail}';",connection);
               connection.Open();
               MySqlDataReader reader = command.ExecuteReader();
               while(reader.Read())
               {
                 var id = reader.GetInt32(0);
                 var firstname = reader.GetString(1);
                 var lastName = reader.GetString(2);
                 var email = reader.GetString(3);
                 var phoneNumber = reader.GetString(4);
                 var password = reader.GetString(5);
                 var staffNo = reader.GetString(6);
                 var role = (Role)Enum.Parse(typeof(Role),reader.GetString(7));
                 var gender = (Gender)Enum.Parse(typeof(Gender),reader.GetString(8));
                 staff = new Staff(id,firstname,lastName,email,password,phoneNumber,gender,role);  
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
         return staff;
        }
       public List<Staff> GetAllStaff()
        {
             List<Staff> staffs = new List<Staff>();

              try
           {
               connection = new MySqlConnection(connect);
               MySqlCommand command = new MySqlCommand("Select * from Staff",connection);
               connection.Open();
               MySqlDataReader reader = command.ExecuteReader();
               while(reader.Read())
               {
                 var id = reader.GetInt32(0);
                 var firstname = reader.GetString(1);
                 var lastName = reader.GetString(2);
                 var email = reader.GetString(3);
                 var phoneNumber = reader.GetString(4);
                 var password = reader.GetString(5);
                 var staffNo = reader.GetString(6);
                 var role = (Role)Enum.Parse(typeof(Role),reader.GetString(7));
                 var gender = (Gender)Enum.Parse(typeof(Gender),reader.GetString(8));
                 var staff = new Staff(id,firstname,lastName,email,password,phoneNumber,gender,role);  
                 staffs.Add(staff);      
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
         return staffs;
        }
        public void PrintAllStaff()
        {
            var staff = GetAllStaff();
            foreach (var item in staff)
            {
                Console.WriteLine();
               Console.WriteLine("ID:" + " " + item.Id + "\t" + "FIRSTNAME:" + "  " + item.FirstName + "\t" + " LASTNAME:" + "  " + item.LastName + "\t" + "PHONENUMBER:" + "  " + item.PhoneNumber + "\t" + "ROLE:  " + item.Role + "\t" +"GENDER:  "+ item.Gender);
            }
        }
         public void DeleteStaff(int id)
        {
            
            try
           {
               connection = new MySqlConnection(connect);
               MySqlCommand command = new MySqlCommand($"Delete from Staff where Id = '{id}'",connection);
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
        public void DeletingStaff(int id)
        {
            PrintAllStaff();
            Console.WriteLine("Enter Staff Id");
            id = int.Parse(Console.ReadLine());
            DeleteStaff(id);
        }
       
    }
}