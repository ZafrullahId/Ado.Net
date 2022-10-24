using System;

namespace FoodOderingApp.Enums
{
    public class Staff:Person
    {
        public int Id {get;set;}
        public string StaffNo {get;set;}
        public Role Role{get;set;}
        public Gender Gender {get;set;}
        public Staff(int id, string fName, string lName,string email,string password,string phone,Gender gender,Role role):base(fName,lName,email,password,phone)
        {
            Id = id;
           Role = role;
           StaffNo =  $"ST{Guid.NewGuid().ToString().Replace("_", "").Substring(0,5).ToUpper()}";
        }
        //  public Staff(int id, string fName, string lName,string email,string password,string phone,Role role):base(fName,lName,email,password,phone)
        // {
           
        // }
        //  internal static Staff FormatLine(string line)
        // {
        //     var props = line.Split('\t');
        //     return new Staff(int.Parse(props[0]), props[1], props[2],props[3],props[4],props[5],(Gender)Enum.Parse(typeof(Gender),props[6]),(Role)Enum.Parse(typeof(Role),props[7]));
        // }

        // public override string ToString()
        // {
        //     return $"{Id}\t{FirstName}\t{LastName}\t{Email}\t{Password}\t{PhoneNumber}\t{Gender}\t{Role}\t{StaffNo}";
        // }
    }
}