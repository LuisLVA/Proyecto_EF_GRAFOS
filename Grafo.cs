using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Grafo
    {
        ListaSimple l_vertices = new ListaSimple();
        Random r = new Random();

        public Grafo()
        {
            CrearCampus();
        }
       public void MostrarLugares()
        {
            l_vertices.Mostrar();
        }
       private void CrearCampus()
        {
            Vertice aula1 = AgregarLugar("Aula 1", "Aula");
            Vertice aula2 = AgregarLugar("Aula 2", "Aula");
            Vertice aula3 = AgregarLugar("Aula 3", "Aula");
            Vertice aula4 = AgregarLugar("Aula 4", "Aula");
            Vertice aula5 = AgregarLugar("Aula 5", "Aula");
            Vertice aula6 = AgregarLugar("Aula 6", "Aula");

            Vertice lab1 = AgregarLugar("Laboratorio 1", "Laboratorio");
            Vertice lab2 = AgregarLugar("Laboratorio 2", "Laboratorio");

            Vertice comedor = AgregarLugar("Comedor", "Comedor");

            Vertice patio = AgregarLugar("Patio", "ZonaSegura", true);
            Vertice estacionamiento = AgregarLugar("Estacionamiento", "ZonaSegura", true);
            Vertice patio2 = AgregarLugar("Patio 2", "ZonaSegura", true);


            AgregarArista(aula1, aula2, r.Next(15, 31));
            AgregarArista(aula2, aula3, r.Next(15, 31));
            AgregarArista(aula4, aula5, r.Next(15, 31));
            AgregarArista(aula5, aula6, r.Next(15, 31));

            AgregarArista(aula3, lab1, r.Next(15, 31));
            AgregarArista(aula6, lab2, r.Next(15, 31));

            AgregarArista(lab1, comedor, r.Next(15, 31));
            AgregarArista(lab2, comedor, r.Next(15, 31));

            AgregarArista(comedor, patio, r.Next(15, 31));
            AgregarArista(comedor, estacionamiento, r.Next(15, 31));

            AgregarArista(lab1, patio, r.Next(15, 31));
            AgregarArista(lab2, estacionamiento, r.Next(15, 31));

            AgregarArista(aula3, patio, r.Next(15, 31));
            AgregarArista(aula6, estacionamiento, r.Next(15, 31));

            AgregarArista(aula1, patio2, r.Next(15, 31));
            AgregarArista(aula4, patio2, r.Next(15, 31));
        }

        private Vertice AgregarLugar(string nombre, string tipo, bool esZonaSegura = false)
        {
            Lugar l = new Lugar
            {
                nombre = nombre,
                tipo = tipo,
                esZonaSegura = esZonaSegura
            };
            return l_vertices.Insertar(l);
        }
        private void AgregarArista(Vertice origen, Vertice destino, float peso, bool bidireccional = true)
        {
            origen.ls.Insertar(destino, peso);
            if (bidireccional)
                destino.ls.Insertar(origen, peso);
        }
        public void Dijkstra(string nombreOrigen)
        {
            Vertice temp = l_vertices.primero;
            while (temp != null)
            {
                temp.distancia = float.MaxValue;
                temp.visitado = false;
                temp.anterior = null;
                temp = temp.sig;
            }

            Vertice origen = l_vertices.Buscar(nombreOrigen);
            if (origen == null)
            {
                Console.WriteLine("No se encontro el lugar de origen: " + nombreOrigen);
                return;
            }
            origen.distancia = 0;
            while (true)
            {
                Vertice actual = null;
                float menor = float.MaxValue;

                temp = l_vertices.primero;
                while (temp != null)
                {
                    if (!temp.visitado && temp.distancia < menor)
                    {
                        menor = temp.distancia;
                        actual = temp;
                    }
                    temp = temp.sig;
                }

                if (actual == null) break; 
                actual.visitado = true;

                Arista a = actual.ls.primero;
                while (a != null)
                {
                    float nuevaDistancia = actual.distancia + a.peso;
                    if (nuevaDistancia < a.destino.distancia)
                    {
                        a.destino.distancia = nuevaDistancia;
                        a.destino.anterior = actual;
                    }
                    a = a.sig;
                }
            }

            MostrarRutaMasSegura(origen);
        }
        private void MostrarRutaMasSegura(Vertice origen)
        {
            Vertice zonaMasCercana = null;
            float mejorDistancia = float.MaxValue;

            Vertice temp = l_vertices.primero;
            while (temp != null)
            {
                if (temp.dato.esZonaSegura && temp.distancia < mejorDistancia)
                {
                    mejorDistancia = temp.distancia;
                    zonaMasCercana = temp;
                }
                temp = temp.sig;
            }

            Console.WriteLine("--------------------------------------------");
            if (zonaMasCercana == null || mejorDistancia == float.MaxValue)
            {
                Console.WriteLine("No existe una ruta hacia ninguna zona segura desde " + origen.dato.nombre);
                Console.WriteLine("--------------------------------------------");
                return;
            }

            Console.WriteLine("Origen: " + origen.dato.nombre);
            Console.WriteLine("Zona segura mas cercana: " + zonaMasCercana.dato.nombre);
            Console.WriteLine("Distancia total: " + mejorDistancia + " metros");
            Console.WriteLine("--------------------------------------------");

            Vertice[] ruta = new Vertice[100];
            int n = 0;
            Vertice c = zonaMasCercana;
            while (c != null)
            {
                ruta[n] = c;
                n++;
                c = c.anterior;
            }

            Console.Write("Ruta de evacuacion: ");
            for (int i = n - 1; i >= 0; i--)
            {
                Console.Write(ruta[i].dato.nombre);
                if (i > 0) Console.Write(" -> ");
            }
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------");
        }
    }
}