using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class Vertice
    {
        public Lugar dato;

        public Vertice sig = null;

        public ListaAristas ls = new ListaAristas();

        public float distancia;
        public bool visitado;
        public Vertice anterior;
    }
}
