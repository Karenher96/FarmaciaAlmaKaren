using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Repositoriosk
{
    public class RepositorioClientes
    {
        EmpleadoCruud accionesArchivo;
        List<ClientesClasess> clientes;
        public RepositorioClientes()
        {
            accionesArchivo = new EmpleadoCruud("Clientes.poo");
            clientes = new List<ClientesClasess>();
        }

        public bool Agregar(ClientesClasess inv)
        {
            clientes.Add(inv);
            bool accion = ActualizarArchivo();
            clientes = Leer();
            return accion;
        }

        private bool ActualizarArchivo()
        {
            string elementos = "";
            foreach (ClientesClasess item in clientes)
            {
                elementos += string.Format("{0}| {1} | {2} | {3} | {4}\n", item.Nombre, item.RFC, item.Direccion, item.Telefono, item.mail);
            }
            return accionesArchivo.Guardar(elementos);
        }

        public List<ClientesClasess> Leer()
        {
            string elementos = accionesArchivo.Leer();
            if (elementos != null)
            {
                List<ClientesClasess> inv = new List<ClientesClasess>();
                string[] fila = elementos.Split('\n');
                for (int i = 0; i < fila.Length - 1; i++)
                {
                    string[] espacio = fila[i].Split('|');
                    ClientesClasess a = new ClientesClasess();
                    a.Nombre = (espacio[0]);
                    a.RFC = (espacio[1]);
                    a.Direccion = espacio[2];
                    a.Telefono = espacio[3];
                    a.mail = espacio[4];

                    inv.Add(a);
                }
                clientes = inv;
                return inv;
            }
            else
            {
                return null;
            }
        }

        public bool Eliminar(ClientesClasess cat)
        {
            ClientesClasess categori = new ClientesClasess();
            foreach (var Buscador in clientes)
            {
                if (Buscador.Nombre == cat.Nombre)
                {
                    categori = Buscador;
                }
            }
            clientes.Remove(categori);
            bool accion = ActualizarArchivo();
            clientes = Leer();
            return accion;
        }

        public bool Modificar(ClientesClasess original, ClientesClasess modificado)
        {
            ClientesClasess t = new ClientesClasess();
            foreach (var buscador in clientes)
            {
                if (original.Nombre == buscador.Nombre)
                {
                    t = buscador;
                }
            }
            t.Nombre = modificado.Nombre;
            t.RFC = modificado.RFC;
            t.Direccion = modificado.Direccion;
            t.Telefono = modificado.Telefono;
            t.mail = modificado.mail;
            bool resultado = ActualizarArchivo();
            clientes = Leer();
            return resultado;
        }
    }
}