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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Menu
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void btnEmpleado_Click(object sender, RoutedEventArgs e)
        {
            Empleado empleado = new Empleado();
            empleado.Show();
            this.Close();
        }

        private void Productos_Click(object sender, RoutedEventArgs e)
        {
            Productos productos = new Productos();
            productos.Show();
            this.Close();
        }

        private void Clientes_Click(object sender, RoutedEventArgs e)
        {
            Clientes clientes = new Clientes();
            clientes.Show(); //abre la ventana de clientes
            this.Close();
        }

       

        private void Categorias_Click(object sender, RoutedEventArgs e)
        {
            Ventas categorias = new Ventas();
            categorias.Show(); //abre la ventana de categorias
            this.Close();
        }
    }
}
