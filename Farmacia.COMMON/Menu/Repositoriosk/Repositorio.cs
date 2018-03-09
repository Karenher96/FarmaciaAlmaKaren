using Menu;
using Menu.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
   public class Repositorio
    {
        EmpleadoCruud accionesArchivo;
        List<Clases.ClassEmpleado> empleado;
        public Repositorio()
        {
            accionesArchivo = new EmpleadoCruud("Empleados.poo");
            empleado = new List<Clases.ClassEmpleado>();
        }

        public bool Agregar(ClassEmpleado inv)
        {
            empleado.Add(inv);
            bool accion = ActualizarArchivo();
            empleado = Leer();
            return accion;
        }

        private bool ActualizarArchivo()
        {
            string elementos = "";
            foreach (ClassEmpleado item in empleado)
            {
                elementos += string.Format("{0}| {1} | {2}\n",item.Nombre, item.sexo, item.Edad);
            }
            return accionesArchivo.Guardar(elementos);
        }

        public List<ClassEmpleado> Leer()
        {
            string elementos = accionesArchivo.Leer();
            if (elementos != null)
            {
                List<ClassEmpleado> inv = new List<ClassEmpleado>();
                string[] fila = elementos.Split('\n');
                for (int i = 0; i < fila.Length - 1; i++)
                {
                    string[] espacio = fila[i].Split('|');
                    ClassEmpleado a = new ClassEmpleado();
                    a.Nombre = (espacio[0]);
                    a.sexo = (espacio[1]);
                    a.Edad = espacio[2];

                    inv.Add(a);
                }
                empleado = inv;
                return inv;
            }
            else
            {
                return null;
            }
        }

        public bool Eliminar(ClassEmpleado cat)
        {
            ClassEmpleado categori = new ClassEmpleado();
            foreach (var Buscador in empleado)
            {
                if (Buscador.Edad == cat.Edad)
                {
                    categori = Buscador;
                }
            }
            empleado.Remove(categori);
            bool accion = ActualizarArchivo();
            empleado = Leer();
            return accion;
        }

        public bool Modificar(ClassEmpleado original, ClassEmpleado modificado)
        {
            ClassEmpleado t = new ClassEmpleado();
            foreach (var buscador in empleado)
            {
                if (original.Nombre == buscador.Nombre)
                {
                    t = buscador;
                }
            }
            t.Nombre = modificado.Nombre;
            t.sexo = modificado.sexo;
            t.Edad = modificado.Edad;
            bool resultado = ActualizarArchivo();
            empleado = Leer();
            return resultado;
        }
    }
}
