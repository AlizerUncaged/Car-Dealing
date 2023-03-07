using CarDealing.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarDealing.Views
{
    public partial class CustomerTable : Form
    {
        private readonly Form1 form1;

        private BindingSource _customersBindingSource;

        public CustomerTable(Form1 form1)
        {
            InitializeComponent();

            this.form1 = form1;

            this.form1.CustomersChanged += (s, e) =>
            {
                RefreshTable();
            };
        }

        void RefreshTable()
        {
            _customersBindingSource.AllowNew = false;
            _customersBindingSource.ResetBindings(false);
            _customersBindingSource.DataSource = form1.Customers;
            CustomersTable.Invalidate();
        }

        private void CustomerTable_Load(object sender, EventArgs e)
        {
            CustomersTable.AutoGenerateColumns = true;
            CustomersTable.ReadOnly = true;
            CustomersTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            CustomersTable.AllowUserToAddRows = false;

            _customersBindingSource = new BindingSource();
            CustomersTable.DataSource = _customersBindingSource;
            _customersBindingSource.DataSource = form1.Customers;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // remove customer
            var selectedItems = CustomersTable.SelectedRows.Cast<DataGridViewRow>()
                .Select(y => y.DataBoundItem as Customer);

            MessageBox.Show($"Removed customer{(selectedItems.Count() > 1 ? "s" : "")} {string.Join(", ", selectedItems.Select(x => x.Email))}",
                $"Removed {selectedItems.Count()} item{(selectedItems.Count() > 1 ? "s" : "")}!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            foreach (var i in selectedItems)
            {
                form1.RemoveCustomer(i);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // add customer
            var customerForm = new CustomerForm();

            customerForm.ShowDialog();

            if (customerForm.Customer != null)
            {
                form1.AddCustomer(customerForm.Customer);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // subscribe

            var selectedItems = CustomersTable.SelectedRows.Cast<DataGridViewRow>()
                .Select(y => y.DataBoundItem as Customer);

            MessageBox.Show($"Added customer{(selectedItems.Count() > 1 ? "s" : "")} {string.Join(", ", selectedItems.Select(x => x.Email))} to mailbox!",
                $"Added {selectedItems.Count()} customers{(selectedItems.Count() > 1 ? "s" : "")}!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            form1.SetCustomersNotifications(true, selectedItems.ToArray());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // subscribe

            var selectedItems = CustomersTable.SelectedRows.Cast<DataGridViewRow>()
                .Select(y => y.DataBoundItem as Customer);

            MessageBox.Show($"Removed customer{(selectedItems.Count() > 1 ? "s" : "")} {string.Join(", ", selectedItems.Select(x => x.Email))} to mailbox!",
                $"Unsubscribed {selectedItems.Count()} customers{(selectedItems.Count() > 1 ? "s" : "")}!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            form1.SetCustomersNotifications(false, selectedItems.ToArray());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // simulate
            var selectedItems = CustomersTable.SelectedRows.Cast<DataGridViewRow>()
                .Select(y => y.DataBoundItem as Customer);

            foreach (var customer in selectedItems)
            {
                new Simulator(form1, customer).Show();
            }
        }
    }
}
