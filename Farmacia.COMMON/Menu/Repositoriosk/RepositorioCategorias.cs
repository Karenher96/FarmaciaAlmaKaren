using Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Repositoriosk
{
    public class RepositorioCategorias
    {
        EmpleadoCruud accionesArchivo;
        List<Clases.Categoria> categorias;
        public RepositorioCategorias()
        {
            accionesArchivo = new EmpleadoCruud("Categorias.poo");
            categorias = new List<Clases.Categoria>();
        }

        public bool Agregar(Clases.Categoria inv)
        {
            categorias.Add(inv);
            bool accion = ActualizarArchivo();
            categorias = Leer();
            return accion;
        }

        private bool ActualizarArchivo()
        {
            string elementos = "";
            foreach (Clases.Categoria item in categorias)
            {
                elementos += string.Format("{0}\n", item.Medicamentos);
            }
            return accionesArchivo.Guardar(elementos);
        }

        public List<Clases.Categoria> Leer()
        {
            string elementos = accionesArchivo.Leer();
            if (elementos != null)
            {
                List<Clases.Categoria> inv = new List<Clases.Categoria>();
                string[] fila = elementos.Split('\n');
                for (int i = 0; i < fila.Length - 1; i++)
                {
                    string[] espacio = fila[i].Split('|');
                    Clases.Categoria a = new Clases.Categoria();
                    a.Medicamentos = (espacio[0]);
                    

                    inv.Add(a);
                }
                categorias = inv;
                return inv;
            }
            else
            {
                return null;
            }
        }

        public bool Eliminar(Clases.Categoria cat)
        {
            Clases.Categoria categori = new Clases.Categoria();
            foreach (var Buscador in categorias)
            {
                if (Buscador.Medicamentos == cat.Medicamentos)
                {
                    categori = Buscador;
                }
            }
            categorias.Remove(categori);
            bool accion = ActualizarArchivo();
            categorias = Leer();
            return accion;
        }

        public bool Modificar(Clases.Categoria original, Clases.Categoria modificado)
        {
            Clases.Categoria t = new Clases.Categoria();
            foreach (var buscador in categorias)
            {
                if (original.Medicamentos == buscador.Medicamentos)
                {
                    t = buscador;
                }
            }
            t.Medicamentos = modificado.Medicamentos;
            
            bool resultado = ActualizarArchivo();
            categorias = Leer();
            return resultado;
        }
    }
}
