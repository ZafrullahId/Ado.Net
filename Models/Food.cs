using System;

namespace FoodOderingApp.Enums
{
    public class Food
    {
        public int FoodId {get;set;}
        public string FoodTypeName {get;set;}
        public decimal Price {get;set;}

        public Food(int id,string foodTypeName, decimal price)
        {
            FoodId = id;
            FoodTypeName = foodTypeName;
            Price = price;
        }
        public Food(string foodTypeName, decimal price)
        {
            FoodTypeName = foodTypeName;
            Price = price;
        }
        //  internal static Food LineFomart(string line)
        // {
        //     var props = line.Split('\t');
        //     return new Food(props[1],decimal.Parse(props[2]));
        // }

        // public override string ToString()
        // {
        //     return $"{FoodId}\t{FoodTypeName}\t{Price}";
        // }
    }
}