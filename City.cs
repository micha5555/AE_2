using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_2_Ziober
{
    public class City
    {
        public string name { get; }
        public double x { get; }
        public double y { get; }

        public City(string name, double x, double y)
        {
            this.name = name;
            this.x = x;
            this.y = y;
        }

        public double DistanceTo(City city)
        {
            var xDistance = Math.Abs(x - city.x);
            var yDistance = Math.Abs(y - city.y);
            var distance = Math.Sqrt(Math.Pow(xDistance, 2) + Math.Pow(yDistance, 2));

            return distance;
        }

        public override string ToString()
        {
            return name + "(" + x + ", " + y + ")";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is City)) return false;
            
            City casted = obj as City;
            return name.Equals(casted.name) && x == casted.x && y == casted.y;

        }
    }
}
