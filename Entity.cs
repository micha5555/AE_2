using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_2_Ziober
{
    internal class Entity
    {
        // city at index 0 is connected with city at index 1, city at index 1 is connected with city at index 2, etc.
        private List<City> citiesInOrder = new List<City>();

        public double totalLength { get; set; }
        public double fitness { get; set; }
        public double fitnessPercent { get; set; }

        public Entity()
        {

        }

        public Entity(List<City> cities)
        {
            citiesInOrder = cities;
            for(int i = 0; i < cities.Count - 1; i++)
            {
                totalLength += cities[i].DistanceTo(cities[i + 1]);
            }
            fitness = 1 / totalLength;
        }

        public static Entity InitializeRandomEntity(List<City> cities)
        {
            List<City> citiesCopy = copyList(cities);
            Random random = new Random();
            int citiesSize = citiesCopy.Count;
            Entity entity = new Entity();
            for(int i = 0; i < citiesSize; i++)
            {
                //Console.WriteLine(cities.Count);
                int randomCityIndex = random.Next(0, citiesSize - i);
                entity.Add(citiesCopy[randomCityIndex]);
                citiesCopy.RemoveAt(randomCityIndex);
            }

            double length = 0;
            for (int i = 0; i < cities.Count - 1; i++)
            {
                length += entity.GetCitiesInOrder()[i].DistanceTo(entity.GetCitiesInOrder()[i + 1]);
                //Console.WriteLine(cities[i].DistanceTo(cities[i + 1]));
            }
            entity.totalLength = length;
            entity.fitness = 1 / length;
            return entity;
        }

        public void Add(City city)
        {
            citiesInOrder.Add(city);
        }

        /*public void SetTotalLength(double length)
        {
            totalLength = length;
        }*/

        /*public double GetTotalLength()
        {
            return totalLength;
        }*/

        public List<City> GetCitiesInOrder()
        {
            return citiesInOrder;
        }

        public String GetPopulationCode()
        {
            String code = "";
            foreach(City city in citiesInOrder)
            {
                code += city.Name;
            }
            return code;
        }

        public static Entity CopyEntity(Entity entity)
        {
            List<City> citiesCopy = new List<City>();
            foreach (City city in entity.GetCitiesInOrder())
            {
                citiesCopy.Add(new City(city.Name, city.X, city.Y));
            }
            return new Entity(citiesCopy);
        }   

        private static List<City> copyList(List<City> list)
        {
            List<City> copy = new List<City>();
            foreach(City city in list)
            {
                copy.Add(new City(city.Name, city.X, city.Y));
            }
            return copy;
        }
    }
}
