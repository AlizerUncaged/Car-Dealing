using System;
using System.Windows.Forms;
using CarDealing.Models;

namespace CarDealing.Views
{
    public partial class CustomerForm : Form
    {
        public Customer Customer { get; set; }

        public CustomerForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // add
            Customer = new Customer()
            {
                Username = textBox2.Text,
                Email = textBox1.Text
            };
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // cancel
            Customer = null;
            this.Close();
        }
    }
}