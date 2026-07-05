using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_LUIS_GRAFOS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Grafo campus = new Grafo();
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=== SISTEMA DE EVACUACION - MAPA DEL CAMPUS ===\n");
                Console.WriteLine("Lugares disponibles:");
                campus.MostrarLugares();

                Console.WriteLine("\n1. Calcular ruta de evacuacion desde un lugar");
                Console.WriteLine("0. Salir");
                Console.Write("\nSeleccione una opcion: ");
                string opcion = Console.ReadLine();

                if (opcion == "0")
                {
                    salir = true;
                }
                else if (opcion == "1")
                {
                    Console.Write("\nIngrese el nombre exacto del lugar de origen (ej: Aula 3): ");
                    string origen = Console.ReadLine();

                    Console.WriteLine();
                    campus.Dijkstra(origen);

                    Console.WriteLine("\nPresione una tecla para volver al menu...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("\nOpcion invalida.");
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                }
            }
            Console.WriteLine("\nPrograma finalizado. Hasta luego.");
        }
    }
    
}
