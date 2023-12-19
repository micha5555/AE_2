using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_2_Ziober
{
    public class City
    {
        public string Name { get; }
        public int X { get; }
        public int Y { get; }

        public City(string name, int x, int y)
        {
            Name = name;
            X = x;
            Y = y;
        }

        public double DistanceTo(City city)
        {
            var xDistance = Math.Abs(X - city.X);
            var yDistance = Math.Abs(Y - city.Y);
            var distance = Math.Sqrt(Math.Pow(xDistance, 2) + Math.Pow(yDistance, 2));

            return distance;
        }

        public override string ToString()
        {
            return Name + "(" + X + ", " + Y + ")";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is City)) return false;
            
            City casted = obj as City;
            return Name.Equals(casted.Name) && X == casted.X && Y == casted.Y;

        }
    }
}
