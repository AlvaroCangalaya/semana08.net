using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CapaDatos;
using CapaNegocio;

namespace Semana07
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window    
    {
        private BusinessCustomer customerService = new BusinessCustomer();
        private Customer clienteSeleccionado;

        public MainWindow()
        {
            InitializeComponent();
            CargarClientes();
        }
        private void CargarClientes()
        {
            var lista = customerService.ObtenerTodos();
            dgClientes.ItemsSource = lista; // dgClientes es tu DataGrid en XAML
        }
        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtBuscar.Text.Trim();
            var resultado = customerService.BuscarClientesPorNombre(nombre);
            dgClientes.ItemsSource = resultado;
        }
        private void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtName.Text.Trim();
            string direccion = txtAddress.Text.Trim();
            string telefono = txtPhone.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El nombre no puede estar vacío.");
                return;
            }

            if (customerService.ExisteNombre(nombre))
            {
                MessageBox.Show("Ya existe un cliente con ese nombre.");
                return;
            }

            Customer c = new Customer
            {
                Name = nombre,
                Address = direccion,
                Phone = telefono,
            };

            try
            {
                customerService.RegistrarCustomer(c);
                MessageBox.Show("Cliente registrado correctamente");
                CargarClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar: " + ex.Message);
            }
        }

        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            if (dgClientes.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un cliente de la lista para actualizar.");
                return;
            }

            Customer selectedCustomer = (Customer)dgClientes.SelectedItem;
            selectedCustomer.Name = txtName.Text;
            selectedCustomer.Address = txtAddress.Text;
            selectedCustomer.Phone = txtPhone.Text;

            try
            {
                customerService.ActualizarCustomer(selectedCustomer);
                MessageBox.Show("Cliente actualizado correctamente.");
                CargarClientes(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dgClientes.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un cliente de la lista para eliminar.");
                return;
            }

            Customer selectedCustomer = (Customer)dgClientes.SelectedItem;

            var result = MessageBox.Show($"¿Está seguro de eliminar al cliente {selectedCustomer.Name}?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    customerService.EliminarCustomer(selectedCustomer);
                    MessageBox.Show("Cliente eliminado correctamente.");
                    CargarClientes(); // Refrescar lista
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message);
                }
            }
        }
        private void dgClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgClientes.SelectedItem is Customer seleccionado)
            {
                txtName.Text = seleccionado.Name;
                txtAddress.Text = seleccionado.Address;
                txtPhone.Text = seleccionado.Phone;


                clienteSeleccionado = seleccionado;
            }
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            txtBuscar.Clear();
            dgClientes.UnselectAll(); 
            clienteSeleccionado = null;
        }

    }
}