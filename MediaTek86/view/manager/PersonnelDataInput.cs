using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaTek86.model;

namespace MediaTek86.view.manager
{
    public partial class PersonnelDataInput : Form
    {
        public PersonnelDataInput(Personnel personnel = null)
        {
            InitializeComponent();

            if(personnel is Personnel){
                this.Text = "Modifier un membre du personnel";
                this.txtSurname.Text = personnel.nom;
                this.txtName.Text = personnel.prenom;
                this.txtPhone.Text = personnel.tel;
                this.txtEmail.Text = personnel.mail;
            }else{
                this.Text = "Ajouter un membre au personnel";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
