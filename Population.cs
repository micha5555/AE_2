using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_2_Ziober
{
    internal class Population
    {
        private List<Entity> entities;
        private double totalFitness;

        public Population(List<Entity> entities)
        {
            this.entities = entities;
            for(int i = 0; i < entities.Count; i++)
            {
                totalFitness += entities[i].fitness;
            }
        }
    }
}
