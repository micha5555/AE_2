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
        var cities = new List<City>
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

        var populationSize = 100;
        var iterations = 1000;
        var mutationProbability = 0.01;

        List<Entity> population = new List<Entity>();
        for(int i = 0; i < populationSize; i++)
        {
            population.Add(Entity.InitializeRandomEntity(cities));
        }
       

        Entity e1 = new Entity(new List<City>
        {
            // shuffle these citites
            new City("C", 6, 5),
            new City("B", 9, 7),
            new City("J", 8, 10),
            new City("G", 4, 2),
            new City("A", 2, 1),
            new City("I", 7, 3),
            new City("F", 5, 6),
            new City("D", 1, 7),
            new City("E", 3, 6),
            new City("H", 10, 4)
        });
        Entity e2 = new Entity(new List<City>
        {
            new City("J", 8, 10),
            new City("I", 7, 3),
            new City("E", 3, 6),
            new City("A", 2, 1),
            new City("C", 6, 5),
            new City("B", 9, 7),
            new City("D", 1, 7),
            new City("H", 10, 4),
            new City("G", 4, 2),
            new City("F", 5, 6)
        });
        Console.WriteLine(e1.fitness);
        Console.WriteLine(e1.totalLength);
        Console.WriteLine(e2.fitness);
        Console.WriteLine(e2.totalLength);

        for (int i = 0; i < e1.GetCitiesInOrder().Count; i++)
        {
            Console.Write(e1.GetCitiesInOrder()[i].Name + " ");
        }
        Console.WriteLine();
        for (int i = 0; i < e2.GetCitiesInOrder().Count; i++)
        {
            Console.Write(e2.GetCitiesInOrder()[i].Name + " ");
        }
        Console.WriteLine("\n");
        List<Entity> childs = GeneticOperations.CycleCrossover(e1, e2);
        Entity child1 = childs[0];
        Entity child2 = childs[1];
        for (int i = 0; i < child1.GetCitiesInOrder().Count; i++)
        {
            Console.Write(child1.GetCitiesInOrder()[i].Name + " ");
        }
        Console.WriteLine();
        for (int i = 0; i < child2.GetCitiesInOrder().Count; i++)
        {
            Console.Write(child2.GetCitiesInOrder()[i].Name + " ");
        }

        /*for (int i = 0; i < cities.Count; i++)
        {
            //Console.WriteLine(population.GetCitiesInOrder().Count);
            Console.WriteLine(population.GetCitiesInOrder()[i]);
        }
        Console.WriteLine(population.GetTotalLength());
        Console.WriteLine(population.GetPopulationCode());
        */



        /*
        var population = new Population(cities, populationSize, mutationProbability);
        var best = population.Best;

        for (var i = 0; i < iterations; i++)
        {
            population.NextGeneration();
            best = population.Best;
        }

        Console.WriteLine($"Best: {best}");
        Console.WriteLine($"Distance: {best.Distance}");
        Console.WriteLine($"Fitness: {best.Fitness}");
        Console.WriteLine($"Iterations: {iterations}");
        Console.WriteLine($"Population size: {populationSize}");
        Console.WriteLine($"Mutation probability: {mutationProbability}");
        */
    }
}