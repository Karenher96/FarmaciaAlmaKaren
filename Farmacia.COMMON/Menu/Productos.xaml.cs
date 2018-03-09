using Menu.Repositoriosk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Menu
{
    /// <summary>
    /// Lógica de interacción para Productos.xaml
    /// </summary>
    public partial class Productos : Window
    {
       RepositorioProductos repositorio;
        bool esNuevo;

        public Productos()
        {
            InitializeComponent();
            repositorio = new RepositorioProductos();
            HabilitarCajas(false);
            HabilitarBotones(true);
            ActualizarTabla();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void HabilitarCajas(bool habilitadas)
        {
            NombreMedicamento.Clear();
            Descripcion.Clear();
            PrecioCompra.Clear();
            PrecioVenta.Clear();
            Presentacion.Clear();
            NombreMedicamento.IsEnabled = habilitadas;
            Descripcion.IsEnabled = habilitadas;
            PrecioCompra.IsEnabled = habilitadas;
            PrecioVenta.IsEnabled = habilitadas;
            Presentacion.IsEnabled = habilitadas;
        }

        private void HabilitarBotones(bool habilitados)
        {
            btnNuevo.IsEnabled = habilitados;
            btnEditar.IsEnabled = habilitados;
            btnEliminar.IsEnabled = habilitados;
            btnGuardar.IsEnabled = !habilitados;
            btnCancelar.IsEnabled = !habilitados;
        }

        private void btnRegresar_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (repositorio.Leer().Count == 0)
            {
                MessageBox.Show("Tu Producto fue eliminado", "Agrega mas Productos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (ProductosTabla.SelectedItem != null)
                {
                    ProductosClasess a = ProductosTabla.SelectedItem as ProductosClasess;
                    if (MessageBox.Show("Realmente deseas eliminar a " + a.NombreMedicamento + "?", "Eliminar????", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (repositorio.Eliminar(a))
                        {
                            MessageBox.Show("Tu producto ha sido removido", "Producto", MessageBoxButton.OK, MessageBoxImage.Information);
                            ActualizarTabla();
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar a tu producto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("¿A Quien???", "Producto", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NombreMedicamento.Text) || string.IsNullOrEmpty(Descripcion.Text) || string.IsNullOrEmpty(PrecioCompra.Text) || string.IsNullOrEmpty(PrecioVenta.Text) || string.IsNullOrEmpty(Presentacion.Text))
            {
                MessageBox.Show("Faltan datos...", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (esNuevo)
            {

                ProductosClasess a = new ProductosClasess();

                a.NombreMedicamento = NombreMedicamento.Text;
                a.Descripcion = Descripcion.Text;
                a.PrecioCompra = PrecioCompra.Text;
                a.PrecioVenta = PrecioVenta.Text;
                a.Presentacion = Presentacion.Text;

                if (repositorio.Agregar(a))
                {
                    MessageBox.Show("Guardado con Éxito", "Producto", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("Error al guardar a tu Producto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                ProductosClasess original = ProductosTabla.SelectedItem as ProductosClasess;
                ProductosClasess a = new ProductosClasess();
                a.NombreMedicamento = NombreMedicamento.Text;
                a.Descripcion = Descripcion.Text;
                a.PrecioCompra = PrecioCompra.Text;
                a.PrecioVenta = PrecioVenta.Text;
                a.Presentacion = Presentacion.Text;
                if (repositorio.Modificar(original, a))
                {
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                    ActualizarTabla();
                    MessageBox.Show("Tu Producto a sido actualizado", "Producto", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Error al guardar a tu Producto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ActualizarTabla()
        {
            ProductosTabla.ItemsSource = null;
            ProductosTabla.ItemsSource = repositorio.Leer();
        }
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            HabilitarCajas(true);
            HabilitarBotones(false);
            esNuevo = true;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            HabilitarCajas(false);
            HabilitarBotones(true);
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (repositorio.Leer().Count == 0)
            {
                MessageBox.Show("Agrega Productos", "No tienes ningun Producto", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (ProductosTabla.SelectedItem != null)
                {
                    ProductosClasess a = ProductosTabla.SelectedItem as ProductosClasess;
                    HabilitarCajas(true);
                    NombreMedicamento.Text = a.NombreMedicamento;
                    Descripcion.Text = a.Descripcion;
                    PrecioCompra.Text = a.PrecioCompra;
                    PrecioVenta.Text = a.PrecioVenta;
                    Presentacion.Text = a.Presentacion;
                    HabilitarBotones(false);
                    esNuevo = false;
                }
                else
                {
                    MessageBox.Show("¿A Quien???", "Producto", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
        }
    }
}
