using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Repositoriosk
{
    public class RepositorioProductos
    {
        EmpleadoCruud accionesArchivo;
        List<ProductosClasess> productos;
        public RepositorioProductos()
        {
            accionesArchivo = new EmpleadoCruud("Productos.poo");
            productos = new List<ProductosClasess>();
        }

        public bool Agregar(ProductosClasess inv)
        {
            productos.Add(inv);
            bool accion = ActualizarArchivo();
            productos = Leer();
            return accion;
        }

        private bool ActualizarArchivo()
        {
            string elementos = "";
            foreach (ProductosClasess item in productos)
            {
                elementos += string.Format("{0}| {1}| {2}| {3}| {4}\n", item.NombreMedicamento, item.Descripcion, item.PrecioCompra, item.PrecioVenta, item.Presentacion);
            }
            return accionesArchivo.Guardar(elementos);
        }

        public List<ProductosClasess> Leer()
        {
            string elementos = accionesArchivo.Leer();
            if (elementos != null)
            {
                List<ProductosClasess> inv = new List<ProductosClasess>();
                string[] fila = elementos.Split('\n');
                for (int i = 0; i < fila.Length - 1; i++)
                {
                    string[] espacio = fila[i].Split('|');
                    ProductosClasess a = new ProductosClasess();
                    a.NombreMedicamento = (espacio[0]);
                    a.Descripcion = (espacio[1]);
                    a.PrecioCompra = (espacio[2]);
                    a.PrecioVenta= espacio[3];
                    a.Presentacion = espacio[4];

                    inv.Add(a);
                }
                productos = inv;
                return inv;
            }
            else
            {
                return null;
            }
        }

        public bool Eliminar(ProductosClasess cat)
        {
            ProductosClasess categori = new ProductosClasess();
            foreach (var Buscador in productos)
            {
                if (Buscador.NombreMedicamento == cat.NombreMedicamento)
                {
                    categori = Buscador;
                }
            }
            productos.Remove(categori);
            bool accion = ActualizarArchivo();
            productos = Leer();
            return accion;
        }

        public bool Modificar(ProductosClasess original, ProductosClasess modificado)
        {
            ProductosClasess t = new ProductosClasess();
            foreach (var buscador in productos)
            {
                if (original.NombreMedicamento == buscador.NombreMedicamento)
                {
                    t = buscador;
                }
            }
            t.NombreMedicamento = modificado.NombreMedicamento;
            t.Descripcion = modificado.Descripcion;
            t.PrecioCompra = modificado.PrecioCompra;
            t.PrecioVenta = modificado.PrecioVenta;
            t.Presentacion = modificado.Presentacion;
            bool resultado = ActualizarArchivo();
            productos = Leer();
            return resultado;
        }
    }
}