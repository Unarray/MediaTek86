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
using MediaTek86.model;

namespace MediaTek86.view.manager
{
    public partial class Manager : Form
    {

        private readonly PersonnelController personnelController;

        public Manager()
        {
            personnelController = new PersonnelController();
            InitializeComponent();

            List<Personnel> personnels = personnelController.GetPersonnels();
            dataGridPersonnel.AutoGenerateColumns = false;
            dataGridPersonnel.DataSource = personnels;

            // Créer les colonnes
            DataGridViewTextBoxColumn nomColumn = new DataGridViewTextBoxColumn();
            nomColumn.DataPropertyName = "nom";
            nomColumn.HeaderText = "Nom";

            DataGridViewTextBoxColumn prenomColumn = new DataGridViewTextBoxColumn();
            prenomColumn.DataPropertyName = "prenom";
            prenomColumn.HeaderText = "Prénom";

            DataGridViewTextBoxColumn telColumn = new DataGridViewTextBoxColumn();
            telColumn.DataPropertyName = "tel";
            telColumn.HeaderText = "Téléphone";

            DataGridViewTextBoxColumn mailColumn = new DataGridViewTextBoxColumn();
            mailColumn.DataPropertyName = "mail";
            mailColumn.HeaderText = "Email";

            DataGridViewTextBoxColumn serviceColumn = new DataGridViewTextBoxColumn();
            serviceColumn.DataPropertyName = "service.nom";
            serviceColumn.HeaderText = "Service";

            // Ajouter les colonnes au DataGridView
            dataGridPersonnel.Columns.AddRange(nomColumn, prenomColumn, telColumn, mailColumn, serviceColumn);
        }

        private void dataGridPersonnel_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridPersonnel_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Get values from child object
            DataGridViewColumn column = dataGridPersonnel.Columns[e.ColumnIndex];
            if (column.DataPropertyName.Contains("."))
            {
                object data = dataGridPersonnel.Rows[e.RowIndex].DataBoundItem;
                string[] properties = column.DataPropertyName.Split('.');
                for (int i = 0; i < properties.Length && data != null; i++)
                    data = data.GetType().GetProperty(properties[i]).GetValue(data);
                dataGridPersonnel.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = data;
            }
        }
    }
}
