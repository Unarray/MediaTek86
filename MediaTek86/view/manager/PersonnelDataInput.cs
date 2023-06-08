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
using MediaTek86.controller;

namespace MediaTek86.view.manager
{
    public partial class PersonnelDataInput : Form
    {
        const int defaultId = 0;

        /// <summary>
        /// Personnel object generate with value passed in inputs
        /// </summary>
        public Personnel personnel { get; set; }
        /// <summary>
        /// Base Personnel passed in constructor
        /// </summary>
        public readonly Personnel oldPersonnel;
        private readonly ServiceController serviceController;

        /// <summary>
        /// Constructor of PersonnelDataInput
        /// </summary>
        /// <param name="personnel">Fill inputs with thit datas</param>
        public PersonnelDataInput(Personnel personnel = null)
        {
            this.InitializeComponent();

            this.oldPersonnel = personnel;
            this.serviceController = new ServiceController();
            List<Service> services = serviceController.GetServices();
            this.ddService.DataSource = services;
            this.ddService.DisplayMember = "nom";
            this.ddService.SelectedIndex = -1;

            if (personnel is Personnel){
                this.Text = "Modifier un membre du personnel";
                this.txtSurname.Text = personnel.nom;
                this.txtName.Text = personnel.prenom;
                this.txtPhone.Text = personnel.tel;
                this.txtEmail.Text = personnel.mail;
                this.ddService.SelectedIndex = services.FindIndex(service => service.id == personnel.service.id);
            }
            else{
                this.Text = "Ajouter un membre au personnel";
            }
        }

        /// <summary>
        /// Triggered when validation button is pushed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAccept_Click(object sender, EventArgs e)
        {
            string surname = txtSurname.Text;
            string name = txtName.Text;
            string tel = txtPhone.Text;
            string mail = txtEmail.Text;
            Service service = (Service)this.ddService.SelectedItem;

            if (String.IsNullOrEmpty(surname) || String.IsNullOrEmpty(name) || String.IsNullOrEmpty(tel) || String.IsNullOrEmpty(mail) || service == null) return;

            int id = PersonnelDataInput.defaultId;
            
            if(this.oldPersonnel != null)
            {
                DialogResult confirm = MessageBox.Show("Êtes-vous sûr de vouloir modifier cette personne ?", "Modifier cette utilisateur ?", MessageBoxButtons.YesNo);

                if (confirm == DialogResult.No)
                {
                    this.Close();
                    return;
                }

                id = this.oldPersonnel.id;
            }

            this.personnel = new Personnel(
                id,
                service,
                surname,
                name,
                tel,
                mail
            );

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
