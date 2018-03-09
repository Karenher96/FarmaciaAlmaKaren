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
    /// Lógica de interacción para Clientes.xaml
    /// </summary>
    public partial class Clientes : Window
    {
        RepositorioClientes repositorio;
        bool esNuevo;
        public Clientes()
        {
            InitializeComponent();
            repositorio = new RepositorioClientes();
            HabilitarCajas(false);
            HabilitarBotones(true);
            ActualizarTabla();
        }
       
        private void btnRegresar_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void HabilitarCajas(bool habilitadas)
        {
            Nombre.Clear();
            RFC.Clear();
            Direccion.Clear();
            Telefono.Clear();
            mail.Clear();
            Nombre.IsEnabled = habilitadas;
            RFC.IsEnabled = habilitadas;
            Direccion.IsEnabled = habilitadas;
            Telefono.IsEnabled = habilitadas;
            mail.IsEnabled = habilitadas;
        }

        private void HabilitarBotones(bool habilitados)
        {
            btnNuevo.IsEnabled = habilitados;
            btnEditar.IsEnabled = habilitados;
            btnEliminar.IsEnabled = habilitados;
            btnGuardar.IsEnabled = !habilitados;
            btnCancelar.IsEnabled = !habilitados;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (repositorio.Leer().Count == 0)
            {
                MessageBox.Show("Tu empleado fue cliente", "Agrega mas clientes", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (ClientesTabla.SelectedItem != null)
                {
                    ClientesClasess a = ClientesTabla.SelectedItem as ClientesClasess;
                    if (MessageBox.Show("Realmente deseas eliminar a " + a.Nombre + "?", "Eliminar????", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (repositorio.Eliminar(a))
                        {
                            MessageBox.Show("Tu cliente ha sido removido", "Clientes", MessageBoxButton.OK, MessageBoxImage.Information);
                            ActualizarTabla();
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar a tu cliente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("¿¿¿A Quien???", "Cliente", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Nombre.Text) || string.IsNullOrEmpty(RFC.Text) || string.IsNullOrEmpty(Direccion.Text) || string.IsNullOrEmpty(Telefono.Text) || string.IsNullOrEmpty(mail.Text))
            {
                MessageBox.Show("Faltan datos...", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (esNuevo)
            {

                ClientesClasess a = new ClientesClasess();

                a.Nombre = Nombre.Text;
                a.RFC = RFC.Text;
                a.Direccion = Direccion.Text;
                a.Telefono = Telefono.Text;
                a.mail = mail.Text;

                if (repositorio.Agregar(a))
                {
                    MessageBox.Show("Guardado con Éxito", "Cliente", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("Error al guardar a tu cliente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                ClientesClasess original = ClientesTabla.SelectedItem as ClientesClasess;
                ClientesClasess a = new ClientesClasess();
                a.Nombre = Nombre.Text;
                a.RFC = RFC.Text;
                a.Direccion = Direccion.Text;
                a.Telefono = Telefono.Text;
                a.mail = mail.Text;
                if (repositorio.Modificar(original, a))
                {
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                    ActualizarTabla();
                    MessageBox.Show("Tu cliente a sido actualizado", "clientes", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Error al guardar a tu cliente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ActualizarTabla()
        {
            ClientesTabla.ItemsSource = null;
            ClientesTabla.ItemsSource = repositorio.Leer();
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
                MessageBox.Show("Agrega Clientes", "No tienes ningun Cliente", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (ClientesTabla.SelectedItem != null)
                {
                    ClientesClasess a = ClientesTabla.SelectedItem as ClientesClasess;
                    HabilitarCajas(true);
                    Nombre.Text = a.Nombre;
                    RFC.Text = a.RFC;
                    Direccion.Text = a.Direccion;
                    Telefono.Text = a.Telefono;
                    mail.Text = a.mail;
                    HabilitarBotones(false);
                    esNuevo = false;
                }
                else
                {
                    MessageBox.Show("¿A Quien???", "Clientes", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
        }
    }
}
