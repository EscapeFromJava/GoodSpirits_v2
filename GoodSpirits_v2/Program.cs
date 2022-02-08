using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace GoodSpirits_v2
{
    internal class Program
    {
        static void Main()
        {
            string INPUT = @"input.txt";
            string text = File.ReadAllText(INPUT);

            int numberOfLevels = Convert.ToInt32(text[0].ToString()); // считываем кол-во уровней

            var arrLevels = text.Remove(0,1).Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries); // разбиваем систему на массив, состоящий из кол-ва = levels

            Universe universe = new Universe(); // объявляем и инициализируем вселенную
            for (int i = 0; i < numberOfLevels; i++) // перебираем каждый уровень
            {
                Level level = new Level(); // объявляем и инициализируем новый уровень
                var arrPlanets = arrLevels[i].Split(new char[] { '\r','\n'}, StringSplitOptions.RemoveEmptyEntries).ToList(); // разбиваем уровень на массив, по кол-ву планет
                int nPlanet = Convert.ToInt32(arrPlanets[0].ToString()); // считаем кол-во планет на уровне
               
                for (int j = 0; j < nPlanet; j++) // перебираем каждую планету
                {
                    Planet planet = new Planet(); // объявляем и инициализируем новую планету
                    var arrTracks = arrPlanets[j + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); // разбиваем на массив всю информацию о переходах на планету и стоимости
                    
                    for (int k = 0; k < (arrTracks.Count() - 1); k += 2) // перебираем каждый элемент в массиве данных о текущей планете
                    {
                        Track track = new Track(); // объявляем и инициализируем новый путь
                        track.NumberPlanetPrevLevel = Convert.ToInt32(arrTracks[k]); // инициализируем номер планеты, с которой совершен переход
                        track.Costs = Convert.ToInt32(arrTracks[k + 1]); // инициализируем стоимость перехода
                        planet.GetTrack(track); // добавляем путь к планете
                    }
                    level.GetPlanet(planet); // добавляем планету к уровню
                }
                universe.GetLevel(level); // добавляем уровень к вселенной
            }

            //TODO дописать алгоритм вычисления оптимального пути ?

            #region Algorithm

            /*int costs = 0;
            int minTotalCosts = 0;

            for (int i = 0; i < universe.ListLevels.Count; i++)
            {
                for (int j = 0; j < universe.ListLevels[i].ListPlanets.Count; j++)
                {
                    for (int k = 0; k < universe.ListLevels[i].ListPlanets[j].ListTracks.Count; k++)
                    {
                        Console.WriteLine(universe.ListLevels[i].ListPlanets[j].ListTracks[k].Costs);

                    }
                }
            }*/

            #endregion

            Console.ReadKey();
        }
    }
    
    class Universe
    {
        public List<Level> ListLevels  = new List<Level>();

        public void GetLevel(Level level)
        {
            ListLevels.Add(level);
        }

    }

    class Level
    {
        public List<Planet> ListPlanets = new List<Planet>();

        public void GetPlanet(Planet planet)
        {
            ListPlanets.Add(planet);
        }

    }

    class Planet
    {
        public List<Track> ListTracks = new List<Track>();

        public void GetTrack(Track track)
        {
            ListTracks.Add(track);
        }
    }

    class Track
    {
        public int NumberPlanetPrevLevel { get; set; }
        public int Costs { get; set; }

    }
}
