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

       /// <summary>
       /// Login form constructor
       /// </summary>
        public Login()
        {
            this.InitializeComponent();
            this.authController = new AuthController();
        }

        /// <summary>
        /// OnClick event on connect button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            string login = this.txtLogin.Text;
            string pwd = this.txtPassword.Text;

            if (String.IsNullOrEmpty(login) || String.IsNullOrEmpty(pwd)) return;

            if(!this.authController.isAuthValid(login, pwd))
            {
                MessageBox.Show("Authentification incorrecte", "Alerte");
                return;
            }

            this.Hide();

            Manager managerForm = new Manager();

            managerForm.ShowDialog();
            this.txtPassword.Text = "";

            this.Show();
        }   
    }
}
