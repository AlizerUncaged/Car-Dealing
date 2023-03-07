using System;
using System.Windows.Forms;
using CarDealing.Models;

namespace CarDealing.Views
{
    public partial class AddCar : Form
    {
        public Car Car { get; set; }

        public AddCar()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // add
            try
            {
                Car = new Car()
                {
                    Model = textBox1.Text,
                    Year = int.Parse(textBox2.Text),
                    Mileage = double.Parse(textBox3.Text),
                    Price = double.Parse(textBox4.Text),
                };
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured adding car.", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // cancel
            Car = null;
            this.Close();
        }
    }
}