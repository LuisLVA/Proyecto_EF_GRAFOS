using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class ListaSimple
    {
        public Vertice primero = null;

        public Vertice Insertar(Lugar d)
        {
            Vertice nuevo = new Vertice
            {
                dato = d
            };

            if (primero == null)
            {
                primero = nuevo;
            }
            else
            {
                Vertice temp = primero;

                while (temp.sig != null)
                {
                    temp = temp.sig;
                }
                temp.sig = nuevo;
            }

            return nuevo; 
        }
        public Vertice Buscar(string nombre)
        {
            Vertice temp = primero;
            while (temp != null)
            {
                if (temp.dato.nombre.ToUpper() == nombre.ToUpper())
                    return temp;
                temp = temp.sig;
            }
            return null;
        }

        public void Mostrar()
        {
            Vertice temp = primero;
            int i = 1;
            while (temp != null)
            {
                Console.WriteLine(i + ". " + temp.dato.nombre);
                temp = temp.sig;
                i++;
            }
        }
    }
}