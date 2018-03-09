using Menu;
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
    /// Lógica de interacción para Empleado.xaml
    /// </summary>
    public partial class Empleado : Window
    {
        Repositorio repositorio;
        bool esNuevo;
        public Empleado()
        {
            InitializeComponent();
            repositorio = new Repositorio();
            HabilitarCajas(false);
            HabilitarBotones(true);
            ActualizarTabla();
        }

        private void btnRegresar_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void HabilitarCajas(bool habilitadas)
        {
            NombreEmpleado.Clear();
            Edad.Clear();
            Sexo.Clear();
            NombreEmpleado.IsEnabled = habilitadas;
            Edad.IsEnabled = habilitadas;
            Sexo.IsEnabled = habilitadas;
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
                MessageBox.Show("Tu empleado fue eliminado", "Agrega mas empleados", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (EmpleadosTabla.SelectedItem != null)
                {
                    Clases.ClassEmpleado a = EmpleadosTabla.SelectedItem as Clases.ClassEmpleado;
                    if (MessageBox.Show("Realmente deseas eliminar a " + a.Nombre + "?", "Eliminar????", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (repositorio.Eliminar(a))
                        {
                            MessageBox.Show("Tu emplead@ ha sido removido", "Empleado", MessageBoxButton.OK, MessageBoxImage.Information);
                            ActualizarTabla();
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar a tu emplead@", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("¿A Quien???", "Empleado", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NombreEmpleado.Text) || string.IsNullOrEmpty(Edad.Text) || string.IsNullOrEmpty(Sexo.Text))
            {
                MessageBox.Show("Faltan datos...", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (esNuevo)
            {

                Clases.ClassEmpleado a = new Clases.ClassEmpleado();

                a.Nombre= NombreEmpleado.Text;
                a.Edad = Edad.Text;
                a.sexo = Sexo.Text;

                if (repositorio.Agregar(a))
                {
                    MessageBox.Show("Guardado con Éxito", "Empleado", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("Error al guardar a tu emplead@", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Clases.ClassEmpleado original = EmpleadosTabla.SelectedItem as Clases.ClassEmpleado;
                Clases.ClassEmpleado a = new Clases.ClassEmpleado();
                a.Nombre = NombreEmpleado.Text;
                a.Edad = Edad.Text;
                a.sexo = Sexo.Text;
                if (repositorio.Modificar(original, a))
                {
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                    ActualizarTabla();
                    MessageBox.Show("Tu emplead@ a sido actualizado", "Empleado", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Error al guardar a tu emplead@", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ActualizarTabla()
        {
            EmpleadosTabla.ItemsSource = null;
            EmpleadosTabla.ItemsSource = repositorio.Leer();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            HabilitarCajas(true);
            HabilitarBotones(false);
            esNuevo = true;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (repositorio.Leer().Count == 0)
            {
                MessageBox.Show("Agrega emplead@s", "No tienes ningun emplead@", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (EmpleadosTabla.SelectedItem != null)
                {
                    Clases.ClassEmpleado a = EmpleadosTabla.SelectedItem as Clases.ClassEmpleado;
                    HabilitarCajas(true);
                    NombreEmpleado.Text = a.Nombre;
                    Sexo.Text = a.sexo;
                    Edad.Text = a.Edad;
                    HabilitarBotones(false);
                    esNuevo = false;
                }
                else
                {
                    MessageBox.Show("¿A Quien???", "Emplead@", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            HabilitarCajas(false);
            HabilitarBotones(true);
        }
    }
}