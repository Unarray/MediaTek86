using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaTek86.controller;
using MediaTek86.view.manager;

namespace MediaTek86.view
{
    public partial class Login : Form
    {

        private readonly AuthController authController;

        public Login()
        {
            InitializeComponent();
            authController = new AuthController();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text;
            string pwd = txtPassword.Text;

            if (String.IsNullOrEmpty(login) || String.IsNullOrEmpty(pwd)) return;

            if(!authController.isAuthValid(login, pwd))
            {
                MessageBox.Show("Authentification incorrecte", "Alerte");
                return;
            }

            this.Hide();

            Manager managerForm = new Manager();

            managerForm.ShowDialog();

            txtPassword.Text = "";

            this.Show();
        }   
    }
}
