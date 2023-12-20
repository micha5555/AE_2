using ScottPlot;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_2_Ziober
{
    internal class ChartsMaker
    {
        public static Plot CreateCityChart(Entity entity, Config config)
        {
            Plot citiesPlot = new Plot(1000, 1000);

            var bubblePlt = citiesPlot.AddBubblePlot();
            citiesPlot.Title(string.Format("Path {0}\nPath length: {1}\nIterations: {2}\nPopulation size: {3}\nMutation probability: {4}", entity.GetEntityCode(), entity.totalLength, config.iterations, config.populationSize, config.mutationProbability));

            for (int i = 0; i < entity.GetCitiesInOrder().Count; i++)
            {
                City actualCity = entity.GetCitiesInOrder()[i];
                bubblePlt.Add(actualCity.x, actualCity.y, 7, System.Drawing.Color.FromArgb(0, 0, 255), 1, System.Drawing.Color.Transparent);
                citiesPlot.AddText(string.Format("{0} ({1}, {2})", actualCity.name, actualCity.x, actualCity.y), actualCity.x, actualCity.y, 25, Color.Black);
                if (i > 0)
                {
                    City previousCity = entity.GetCitiesInOrder()[i - 1];
                    citiesPlot.AddLine(previousCity.x, previousCity.y, actualCity.x, actualCity.y);
                }
            }
            return citiesPlot;
        }

        public static Plot CreateFitnessChart(List<Population> populationHistory, Config config)
        {
            double[] generationNumers = new double[config.iterations];
            double[] minimalValues = new double[config.iterations];
            double[] maxValues = new double[config.iterations];
            double[] averageValues = new double[config.iterations];
            for (int i = 0; i < config.iterations; i++)
            {
                minimalValues[i] = populationHistory[i].shortestPath;
                maxValues[i] = populationHistory[i].longestPath;
                averageValues[i] = populationHistory[i].averagePath;
                generationNumers[i] = i + 1;
            }

            Plot pathLengthsPlot = new Plot(1000, 1000);
            pathLengthsPlot.AddScatter(generationNumers, minimalValues, label: "Shortest path length");
            pathLengthsPlot.AddScatter(generationNumers, averageValues, label: "Average path length");
            pathLengthsPlot.AddScatter(generationNumers, maxValues, label: "Longest path length");
            pathLengthsPlot.Legend(location: Alignment.UpperRight);

            pathLengthsPlot.Title(string.Format("Path lengths by generation\nIterations: {0}\nPopulation size: {1}\nMutation probability: {2}", config.iterations, config.populationSize, config.mutationProbability));
            return pathLengthsPlot;
        }
    }
}
