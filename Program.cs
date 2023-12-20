using AE_2_Ziober;
using System;
using System.Drawing;
using System.IO;
using ScottPlot;

/*
Michał Ziober 311612
Zadanie 2

Napisać program rozwiązujący problem komiwojażera (minimalizacja drogi pomiędzy n miastami bez powtórzeń) przy pomocy algorytmu genetycznego. Zastosować reprodukcję przy użyciu nieproporcjonalnej ruletki, operator krzyżowania CX, oraz mutację równomierną.

Program powinien umożliwiać użycie różnych wielkości populacji, liczby iteracji, prawdopodobieństwa mutacji. 

Program powinien zapewnić wizualizację wyników w postaci wykresów średniego, maksymalnego i minimalnego przystosowania (długości trasy) dla kolejnych populacji oraz 2 map (o wymiarach 10x10 punktów), na których będą wyświetlane miasta oraz drogi najdłuższa i najkrótsza.

Pokazać działanie programu na danych testowych składających się z 10 miast, opisanych za pomocą współrzędnych na mapie o wymiarach 10x10 punktów.

Dane testowe: miasta:
A(2, 1), B(9, 7), C(6, 5), D(1, 7), E(3, 6), F(5, 6), G(4, 2), H(10, 4), I(7, 3), J(8, 10)
*/

class Program
{
    static void Main(string[] args)
    {
        Config config = Config.ReadConfig();
        List<City> cities = config.cities;
        int populationSize = config.populationSize;
        int iterations = config.iterations;
        double mutationProbability = config.mutationProbability;

        List<Population> populationHistory = new List<Population>();

        List<Entity> entitiesList = new List<Entity>();
        for(int i = 0; i < populationSize; i++)
        {
            entitiesList.Add(Entity.InitializeRandomEntity(cities));
        }
        
        Population population = new Population(entitiesList);
        

        for(int i = 0; i < iterations; i++)
        {
            List<Entity> parents = population.ChooseParentsForNextPopulation();

            List<Entity> children = new List<Entity>();
            for (int j = 0; j < populationSize / 2; j++)
            {
                List<Entity> childs = GeneticOperations.CycleCrossover(Entity.CopyEntity(parents[j * 2]), Entity.CopyEntity(parents[j * 2 + 1]));
                children.Add(childs[0]);
                children.Add(childs[1]);
            }
            Random random = new Random();
            foreach (Entity child in children)
            {
                double randomValue = random.NextDouble();
                if (randomValue < mutationProbability)
                {
                    GeneticOperations.Mutate(child);
                }
            }
            populationHistory.Add(population);
            population = new Population(children);
        }
        
        Entity entityWithLongestPath = FindEntityWithLongestPath(populationHistory);
        Entity entityWithShortestPath = FindEntityWithShortestPath(populationHistory);
        Console.WriteLine("Entity with longest path: " + entityWithLongestPath.GetEntityCode() + ", path length: " + entityWithLongestPath.totalLength); // DEFJBHICGA
        Console.WriteLine("Entity with shortest path: " + entityWithShortestPath.GetEntityCode() + ", path length: " + entityWithShortestPath.totalLength);

        
        string pathToProject = System.IO.Path.GetFullPath(@"..\..\..\");
        Console.WriteLine("Charts are being saved in path: " + pathToProject);

        Plot pathLengthsPlot = ChartsMaker.CreateFitnessChart(populationHistory, config);
        Plot worstPathCityPlot = ChartsMaker.CreateCityChart(entityWithLongestPath, config);
        Plot bestPathCityPlot = ChartsMaker.CreateCityChart(entityWithShortestPath, config);
        pathLengthsPlot.SaveFig(pathToProject + "/charts/path_lengths_by_iterations.png");
        worstPathCityPlot.SaveFig(pathToProject + "/charts/worst_path.png");
        bestPathCityPlot.SaveFig(pathToProject + "/charts/best_path.png");
    }
    private static Entity FindEntityWithLongestPath(List<Population> populationHistory) { 
        Entity entityWithLongestPath = null;
        double longestPath = 0;
        foreach(Population population in populationHistory)
        {
            foreach(Entity entity in population.entities)
            {
                if(entity.totalLength > longestPath)
                {
                    longestPath = entity.totalLength;
                    entityWithLongestPath = entity;
                }
            }
        }
        return entityWithLongestPath;
    }

    private static Entity FindEntityWithShortestPath(List<Population> populationHistory)
    {
        Entity entityWithShortestPath = null;
        double shortestPath = 0;
        foreach (Population population in populationHistory)
        {
            foreach (Entity entity in population.entities)
            {
                if (entity.totalLength < shortestPath || shortestPath == 0)
                {
                    shortestPath = entity.totalLength;
                    entityWithShortestPath = entity;
                }
            }
        }
        return entityWithShortestPath;
    }
}