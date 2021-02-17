using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace puzzle
{
    class Program
    {
        const string FisierIntrare = "puzzle.txt";
        const string FisierSolutie = "solutie.txt";
        static void Main(string[] args)
        {

            var sw = Stopwatch.StartNew();

            int[,] elemente;

            using (StreamReader file = new StreamReader(FisierIntrare))
            {
                int n = Convert.ToInt32(file.ReadLine());
                elemente = new int[n, n];

                for (int i = 0; i < n; i++)
                {
                    string rand = file.ReadLine();
                    string[] coloane = rand.Split(' ');

                    for (int j = 0; j < n; j++)
                    {
                        elemente[i, j] = Convert.ToInt32(coloane[j]);
                    }
                }
            }

            var stare = new Stare(elemente, null, Stare.Actiuni.INIT);
            var solutie = new RezolvarePuzzle(stare);

            using (StreamWriter file = new StreamWriter(FisierSolutie, false))
            {
                if (solutie.Rezolva())
                {
                    var pasi = solutie.GetPasiRezolvare();

                    file.WriteLine(
                        String.Format("Numar de stari generate: {0}", solutie.NumarStariGenerate));
                    file.WriteLine(
                        String.Format("Numar de stari explorate: {0}", solutie.NumarStariExplorate));

                    file.WriteLine(
                        String.Format("Numar pasi pentru solutie: {0}", pasi.Count));

                    file.WriteLine();
                    file.WriteLine("Pasi rezolvare:");

                    foreach (var pas in pasi)
                    {
                        file.WriteLine(pas.Print());
                    }
                }
                else
                {
                    file.WriteLine("Nu are solutie");
                }

                sw.Stop();
                file.WriteLine();
                file.WriteLine(String.Format("Problema a fost rezolvata in {0} milisecunde.", sw.ElapsedMilliseconds));
            }

            Console.Write(File.ReadAllText(FisierSolutie));
        }
    }
}
