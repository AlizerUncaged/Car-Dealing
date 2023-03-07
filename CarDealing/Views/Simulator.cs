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
    public partial class Simulator : Form
    {
        private readonly Form1 form1;
        private readonly Customer customer;

        public Simulator(Form1 form1, Customer customer)
        {
            InitializeComponent();

            this.Text = $"{customer.Email}'s Notifications";
            this.form1 = form1;
            this.customer = customer;

            // To track if this customer was unsubscribed or subscribed.
            form1.SpecificCustomerChanged += CustomerDataChanged;

            isSubscribed = customer.IsSubscribed;
            if (isSubscribed)
                Subscribe();

            label1.Text = $"Below is a list of notifications {customer.Email} received:";
        }

        private void NotificationReceived(object sender, string e)
        {
            richTextBox1.Text += $"{e.Replace("<username>", customer.Username)}{Environment.NewLine}{Environment.NewLine}";
        }

        bool isSubscribed = false;

        void Subscribe()
        {
            if (!isSubscribed)
            {
                form1.Notifications += NotificationReceived;
                isSubscribed = true;
            }
        }

        void Unsubscibe()
        {
            if (isSubscribed)
            {
                form1.Notifications -= NotificationReceived;
                isSubscribed = false;
            }
        }

        private void CustomerDataChanged(object sender, Customer e)
        {
            if (e == customer)
            {
                if (e.IsSubscribed)
                    Subscribe();
                else Unsubscibe();
            }
        }
    }
}
