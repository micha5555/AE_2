using AE_2_Ziober;
using System.Security.Cryptography;
/*
Zadanie 2

Napisać program rozwiązujący problem komiwojażera (minimalizacja drogi pomiędzy n miastami bez powtórzeń) przy pomocy algorytmu genetycznego. Zastosować reprodukcję przy użyciu nieproporcjonalnej ruletki, operator krzyżowania CX, oraz mutację równomierną.

Program powinien umożliwiać użycie różnych wielkości populacji, liczby iteracji, prawdopodobieństwa mutacji. 

Program powinien zapewnić wizualizację wyników w postaci wykresów średniego, maksymalnego i minimalnego przystosowania (długości trasy) dla kolejnych populacji oraz 2 map (o wymiarach 10x10 punktów), na których będą wyświetlane miasta oraz drogi najdłuższa i najkrótsza.

Pokazać działanie programu na danych testowych składających się z 10 miast, opisanych za pomocą współrzędnych na mapie o wymiarach 10x10 punktów.

Dane testowe: miasta:
A(2, 1), B(9, 7), C(6, 5), D(1, 7), E(3, 6), F(5, 6), G(4, 2), H(10, 4), I(7, 3), J(8, 10)
*/

// See https://aka.ms/new-console-template for more information

class Program
{
    static void Main(string[] args)
    {
        List<City> cities = new List<City>
        {
            new City("A", 2, 1),
            new City("B", 9, 7),
            new City("C", 6, 5),
            new City("D", 1, 7),
            new City("E", 3, 6),
            new City("F", 5, 6),
            new City("G", 4, 2),
            new City("H", 10, 4),
            new City("I", 7, 3),
            new City("J", 8, 10)
        };

        int populationSize = 200;
        int iterations = 100;
        double mutationProbability = 0.005;
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
        Console.WriteLine("Population history size " + populationHistory.Count);
        /*foreach(Entity entity in populationHistory[populationHistory.Count - 1].entities)
        {
            Console.WriteLine(populationHistory[0].entities[0].GetPopulationCode() + " " + entity.totalLength);
        }*/
        Console.WriteLine(populationHistory[0].entities[0].GetPopulationCode() + " " + populationHistory[0].entities[0].totalLength);
        Console.WriteLine(populationHistory[iterations / 4].entities[0].GetPopulationCode() + " " + populationHistory[iterations / 4].entities[0].totalLength);
        Console.WriteLine(populationHistory[iterations / 3].entities[0].GetPopulationCode() + " " + populationHistory[iterations / 3].entities[0].totalLength);
        Console.WriteLine(populationHistory[iterations / 2].entities[0].GetPopulationCode() + " " + populationHistory[iterations / 2].entities[0].totalLength);
        Console.WriteLine(populationHistory[iterations - 1].entities[0].GetPopulationCode() + " " + populationHistory[iterations - 1].entities[0].totalLength);

        // DEFJBHICGA
    }
}