
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
    /// Lógica de interacción para Ventas.xaml
    /// </summary>
    public partial class Ventas : Window
    {
        Repositoriosk.RepositorioCategorias repositorio;
        bool esNuevo;
        public Ventas()
        {
           
            InitializeComponent();
            repositorio = new Repositoriosk.RepositorioCategorias();
            HabilitarCajas(false);
            HabilitarBotones(true);
            ActualizarTabla();
        }
        private void HabilitarCajas(bool habilitadas)
        {
           txbNombre.Clear();
            
            
            txbNombre.IsEnabled = habilitadas;
        }
        private void HabilitarBotones(bool habilitados)
        {
            btnNuevo.IsEnabled = habilitados;
            btnEditar.IsEnabled = habilitados;
            btnEliminar.IsEnabled = habilitados;
            btnGuardar.IsEnabled = !habilitados;
            btnCancelar.IsEnabled = !habilitados;
           
        }
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            HabilitarCajas(true);
            HabilitarBotones(false);
            esNuevo = true;
        }
        private void ActualizarTabla()
        {
            dtgTabla.ItemsSource = null;
            dtgTabla.ItemsSource = repositorio.Leer();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbNombre.Text))
            {
                MessageBox.Show("Faltan datos...", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (esNuevo)
            {

                Clases.Categoria a = new Clases.Categoria();

                a.Medicamentos = txbNombre.Text;
                

                if (repositorio.Agregar(a))
                {
                    MessageBox.Show("Guardado con Éxito", "categoria", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("Error al guardar a tu categoria", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Clases.Categoria original = dtgTabla.SelectedItem as Clases.Categoria;
                Clases.Categoria a = new Clases.Categoria();
                a.Medicamentos = txbNombre.Text;
                
                if (repositorio.Modificar(original, a))
                {
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                    ActualizarTabla();
                    MessageBox.Show("Tu categoria a sido actualizado", "categoria", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Error al guardar a tu categoria", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (repositorio.Leer().Count == 0)
            {
                MessageBox.Show("Agrega categoria", "No tienes ningun categoria", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (dtgTabla.SelectedItem != null)
                {
                    Clases.Categoria a = dtgTabla.SelectedItem as Clases.Categoria;
                    HabilitarCajas(true);
                    txbNombre.Text = a.Medicamentos;
                    
                    HabilitarBotones(false);
                    esNuevo = false;
                }
                else
                {
                    MessageBox.Show("¿A Cual???", "categoria", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            HabilitarCajas(false);
            HabilitarBotones(true);
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (repositorio.Leer().Count == 0)
            {
                MessageBox.Show("Tu categoria fue eliminada", "Agrega mas categorias", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (dtgTabla.SelectedItem != null)
                {
                    Clases.Categoria a = dtgTabla.SelectedItem as Clases.Categoria;
                    if (MessageBox.Show("Realmente deseas eliminar a " + a.Medicamentos + "?", "Eliminar????", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (repositorio.Eliminar(a))
                        {
                            MessageBox.Show("Tu emplead@ ha sido removido", "categoria", MessageBoxButton.OK, MessageBoxImage.Information);
                            ActualizarTabla();
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar a tu categoria", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("¿A cual???", "categoria", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
