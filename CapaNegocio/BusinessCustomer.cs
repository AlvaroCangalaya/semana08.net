using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CapaNegocio
{
    public class BusinessCustomer
    {
        private DataCustomer customerDAO = new DataCustomer();

        public List<Customer> ObtenerTodos()
        {
            return customerDAO.GetAll();
        }
        public List<Customer> BuscarClientesPorNombre(string nombre)
        {
            return customerDAO.GetAll()
                              .Where(x => x.Name.Contains(nombre, StringComparison.OrdinalIgnoreCase))
                              .ToList();
        }
        public void RegistrarCustomer(Customer customer)
        {
            customerDAO.InsertCustomer(customer);
        }

        public void ActualizarCustomer(Customer customer)
        {
            customerDAO.UpdateCustomer(customer);
        }

        public void EliminarCustomer(Customer customer)
        {
            customerDAO.DeleteCustomer(customer);
        }
        public bool ExisteNombre(string nombre)
        {
            return customerDAO.GetAll()
                              .Any(x => x.Name.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        }

    }

}
