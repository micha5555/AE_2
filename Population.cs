using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AE_2_Ziober
{
    internal class Population
    {
        public List<Entity> entities { get; set; }
        public double totalFitness { get; set; }
        public double shortestPath { get; set; }
        public double longestPath { get; set; }
        public double averagePath { get; set; }

        public Population(List<Entity> entities)
        {
            this.entities = entities;
            for(int i = 0; i < entities.Count; i++)
            {
                totalFitness += entities[i].inverseLength;
            }
            CountFitnessPercentsForEntities();
            CountPaths();
        }

        public List<Entity> ChooseParentsForNextPopulation()
        {
            List<Entity> parentsForNextPopulation = new List<Entity>();
            Random random = new Random();
            while (parentsForNextPopulation.Count < entities.Count)
            {
                double randomValue = random.NextDouble();
                double cumulativeProbability = 0;
                Entity chosen = null;
                foreach (Entity entity in entities)
                {
                    double epsilon = 1e-10;
                    cumulativeProbability += entity.fitnessPercent;
                    if (randomValue <= cumulativeProbability)
                    {
                        chosen = entity;
                        break;
                    }
                }
                if(chosen == null)
                {
                    chosen = entities[entities.Count - 1];
                }
                parentsForNextPopulation.Add(Entity.CopyEntity(chosen));
            }
            return parentsForNextPopulation;
        }

        public void CountFitnessPercentsForEntities()
        {
            for(int i = 0; i < entities.Count; i++)
            {
                entities[i].fitnessPercent = entities[i].inverseLength / totalFitness;
            }
        }

        private void CountPaths()
        {
            foreach(Entity entity in entities)
            {
                if(entity.totalLength < shortestPath || shortestPath == 0)
                {
                    shortestPath = entity.totalLength;
                }
                if(entity.totalLength > longestPath)
                {
                    longestPath = entity.totalLength;
                }
                averagePath += entity.totalLength;
            }
            averagePath /= entities.Count;
        }

        private static Population CopyPopulation(Population input)
        {
            List<Entity> entitiesCopy = new List<Entity>();
            foreach(Entity en in input.entities)
            {
                List<City> citiesCopy = new List<City>();
                foreach(City city in en.GetCitiesInOrder())
                {
                    citiesCopy.Add(new City(city.name, city.x, city.y));
                }
                entitiesCopy.Add(new Entity(citiesCopy));
            }
            Population populationCopy = new Population(entitiesCopy);
            return populationCopy;
        }

    }
}
