using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Magazzino_TechWare
{
    internal class Program
    {
        static Random random = new Random();
        static async Task Main(string[] args)
        {
            /*
            Il programma deve simulare il lavoro di tre operatori che preparano gli ordini contemporaneamente.
            
            Ogni operatore gestisce un determinato numero di ordini in parallelo, e il programma deve monitorare e riportare il tempo impiegato
            
             - Ogni operatore prepara da 10 a 20 ordini, ciascuno con un tempo di preparazione casuale tra 200 e 1000 ms.

            Il programma deve:

             - Utilizzare i Task per simulare il lavoro degli operatori.
             - Monitorare il progresso in tempo reale (stato degli ordini completati).
             - Al termine, visualizzare quanti ordini sono stati completati da ciascun operatore e il tempo totale impiegato per completare tutti gli ordini.
            
            Luca richiede un report finale che mostri quale operatore è stato più veloce e quale ha completato il maggior numero di ordini.
            */

            Stopwatch stopwatchTotal = Stopwatch.StartNew();

            Task<int[]> Operaio1 = Operaio("Operaio 1");
            Task<int[]> Operaio2 = Operaio("Operaio 2");
            Task<int[]> Operaio3 = Operaio("Operaio 3");

            int[][] Risultati = await Task.WhenAll(Operaio1, Operaio2, Operaio3);
            stopwatchTotal.Stop();

            int OrdiniTotali = Risultati[0][0] + Risultati[1][0] + Risultati[2][0];
            Console.WriteLine($"\n\tOrdini totali: {OrdiniTotali}\n\tTempo totale: {stopwatchTotal.ElapsedMilliseconds}\n");

            int FastestTime = Risultati[0][1];
            int FastestId = 1;
            int MaxValue = Risultati[0][0];
            int MaxId = 1;
            for (int i = 0; i < 3; i++)
            {
                if(Risultati[i][1] < FastestTime)
                {
                    FastestTime = Risultati[i][1];
                    FastestId = i + 1;
                }

                if(Risultati[i][0] > MaxValue)
                {
                    MaxValue = Risultati[i][0];
                    MaxId = i + 1;
                }
            }

            Console.WriteLine($"\n\tPiù veloce: Operaio {FastestId} - {FastestTime} \n\tPiù ordini fatti: Operaio {MaxId} - {MaxValue}");
            
            Console.ReadKey();
        }


        static async Task<int[]> Operaio(string nomeOperaio)
        {            
            int NumeroOrdini = random.Next(10, 21);
            int TempoDiPreparazione = random.Next(200, 1001);
            string NomeOperaio = nomeOperaio;

            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < NumeroOrdini; i++)
            {
                Console.WriteLine($"{NomeOperaio} inizia la preparazione di un ordine - {i + 1}/{NumeroOrdini}");

                await Task.Delay(TempoDiPreparazione);

                Console.WriteLine($"{NomeOperaio} ha preparato l'ordine - {i + 1}/{NumeroOrdini}");
            }

            stopwatch.Stop();

            int[] Returns = { NumeroOrdini, (int)stopwatch.ElapsedMilliseconds };            

            return Returns;
        }
    }
}
