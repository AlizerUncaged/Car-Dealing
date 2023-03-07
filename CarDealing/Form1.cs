using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarDealing.Models;
using CarDealing.Views;

namespace CarDealing
{
    // main code
    public partial class Form1 : Form
    {
        private List<Customer> _customers
            = new List<Customer>()
            {
                new Customer(){
                    Email = "sample@gmail.com",
                    Username = "Julia",
                    IsSubscribed = true
                }
            };

        private List<Car> _cars
            = new List<Car>();

        public IEnumerable<Customer> Customers => _customers;

        public Form1() =>
            InitializeComponent();

        /// <summary>
        /// This allows us to make sure the data is the same per CustomerTable form.
        /// </summary>
        public event EventHandler CustomersChanged;

        public event EventHandler<Customer> SpecificCustomerChanged;

        public event EventHandler<string> Notifications;

        #region Customer
        public void AddCustomer(params Customer[] customers)
        {
            _customers.AddRange(customers);
            CustomersChanged?.Invoke(this, EventArgs.Empty);
        }

        public void RemoveCustomer(params Customer[] customers)
        {
            _customers.RemoveAll(x => customers.Contains(x));
            CustomersChanged?.Invoke(this, EventArgs.Empty);
        }

        public void SetCustomersNotifications(bool subscribe, params Customer[] customers)
        {
            foreach (var i in customers)
            {
                i.IsSubscribed = subscribe;
                SpecificCustomerChanged?.Invoke(this, i);
            }

            CustomersChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region UI

        private void button4_Click(object sender, EventArgs e)
        {
            new CustomerTable(this).Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // add car
            var carDialog = new AddCar();
            carDialog.ShowDialog();

            if (carDialog.Car != null)
            {
                _cars.Add(carDialog.Car);

                Notifications?.Invoke(this, $"Good morning! <username>, we've got a brand " +
                    $"new car you might want to check out!{Environment.NewLine}" +
                    $"Model: {carDialog.Car.Model} at {carDialog.Car.Price} Pesos!{Environment.NewLine}" +
                    $"Year: {carDialog.Car.Year}{Environment.NewLine}" +
                    $"Mileage: {carDialog.Car.Mileage}");
            }

        }


        #endregion

    }
}