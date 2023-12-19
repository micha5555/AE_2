using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_2_Ziober
{
    internal class GeneticOperations
    {
        public static void Mutate(Entity entity)
        {
            int entitySize = entity.GetCitiesInOrder().Count;
            Random random = new Random();
            int firstCityIndex = random.Next(0, entitySize);
            int secondCityIndex = random.Next(0, entitySize);
            while (firstCityIndex == secondCityIndex)
            {
                secondCityIndex = random.Next(0, entitySize);
            }
            City firstCity = entity.GetCitiesInOrder()[firstCityIndex];
            City secondCity = entity.GetCitiesInOrder()[secondCityIndex];
            entity.GetCitiesInOrder()[firstCityIndex] = secondCity;
            entity.GetCitiesInOrder()[secondCityIndex] = firstCity;
        }

        // it returns list of two childs
        public static List<Entity> CycleCrossover(Entity first, Entity second)
        {
            List<City> citiesOfFirst = first.GetCitiesInOrder();
            List<City> citiesOfSecond = second.GetCitiesInOrder();
            Entity firstChild = null;
            Entity secondChild = null;
            if(citiesOfFirst == null || citiesOfSecond == null)
            {
                return null;
            }
            //int cycles = first.GetCitiesInOrder().Count/2;
            List<int> cyclesIndexes = new List<int>();
            //int addedCycles = 0;
            int iterator = 0;
            City cityAtIndexFromSecond = null;
            bool stop = false;
            while (!stop)
            {
                if (citiesOfFirst[iterator].Equals(citiesOfSecond[iterator]))
                {
                    iterator++;
                    if(iterator == citiesOfFirst.Count)
                    {
                        return new List<Entity> { first, second };
                    }
                    continue;
                }
                if(cityAtIndexFromSecond != null)
                {
                    cyclesIndexes.Add(iterator);
                }
                cityAtIndexFromSecond = citiesOfSecond[iterator];
                iterator = citiesOfFirst.IndexOf(cityAtIndexFromSecond);
                if(cyclesIndexes.Count > 0 && iterator == cyclesIndexes[0])
                {
                    stop = true;
                }
                //addedCycles++;
            }

            List<City> firstChildCities = new List<City>();
            List<City> secondChildCities = new List<City>();
            for(int i = 0; i < citiesOfFirst.Count; i++)
            {
                if(cyclesIndexes.Contains(i))
                {
                    firstChildCities.Add(citiesOfFirst[i]);
                    secondChildCities.Add(citiesOfSecond[i]);
                }
                else
                {
                    firstChildCities.Add(citiesOfSecond[i]);
                    secondChildCities.Add(citiesOfFirst[i]);
                }
            }
            firstChild = new Entity(firstChildCities);
            secondChild = new Entity(secondChildCities);

            return new List<Entity> { firstChild, secondChild };
        }

        // ???
        /*public static void CXCrossover(List<Entity> entites)
        {
               Random random = new Random();
            int firstEntityIndex = random.Next(0, entites.Count);
            int secondEntityIndex = random.Next(0, entites.Count);
            while (firstEntityIndex == secondEntityIndex)
            {
                secondEntityIndex = random.Next(0, entites.Count);
            }
            Entity firstEntity = entites[firstEntityIndex];
            Entity secondEntity = entites[secondEntityIndex];
            int firstEntitySize = firstEntity.GetCitiesInOrder().Count;
            int secondEntitySize = secondEntity.GetCitiesInOrder().Count;
            int firstEntityRandomIndex = random.Next(0, firstEntitySize);
            int secondEntityRandomIndex = random.Next(0, secondEntitySize);
            while (firstEntityRandomIndex == secondEntityRandomIndex)
            {
                secondEntityRandomIndex = random.Next(0, secondEntitySize);
            }
            City firstEntityRandomCity = firstEntity.GetCitiesInOrder()[firstEntityRandomIndex];
            City secondEntityRandomCity = secondEntity.GetCitiesInOrder()[secondEntityRandomIndex];
            firstEntity.GetCitiesInOrder()[firstEntityRandomIndex] = secondEntityRandomCity;
            secondEntity.GetCitiesInOrder()[secondEntityRandomIndex] = firstEntityRandomCity;
        }*/
    }
}
