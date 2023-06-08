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
        private readonly AbsenceController absenceController;

        public Manager()
        {
            personnelController = new PersonnelController();
            absenceController = new AbsenceController();
            InitializeComponent();

            //// PERSONNEL DATAGRID
            List<Personnel> personnels = personnelController.GetPersonnels();
            dataGridPersonnel.AutoGenerateColumns = false;
            dataGridPersonnel.DataSource = personnels;

            // Création des colonnes
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

            // Ajout des colonnes au DataGridView `dataGridPersonnel`
            dataGridPersonnel.Columns.AddRange(nomColumn, prenomColumn, telColumn, mailColumn, serviceColumn);


            //// ABSENCE DATAGRID
            List<Absence> absences = absenceController.GetAbsences(personnels.First().id);
            dataGridAbsence.AutoGenerateColumns = false;
            dataGridAbsence.DataSource = absences;

            // Création des colonnes
            DataGridViewTextBoxColumn debutColumn = new DataGridViewTextBoxColumn();
            debutColumn.DataPropertyName = "dateDebut.dateString";
            debutColumn.HeaderText = "Date début";

            DataGridViewTextBoxColumn finColumn = new DataGridViewTextBoxColumn();
            finColumn.DataPropertyName = "dateFin.dateString";
            finColumn.HeaderText = "Date fin";

            DataGridViewTextBoxColumn motifColumn = new DataGridViewTextBoxColumn();
            motifColumn.DataPropertyName = "motif.libelle";
            motifColumn.HeaderText = "Motif";

            // Ajout des colonnes au DataGridView `dataGridPersonnel`
            dataGridAbsence.Columns.AddRange(debutColumn, finColumn, motifColumn);
        }

        // Refresh content of the personnel data grid
        private void refreshPersonnelData()
        {
            List<Personnel> personnels = personnelController.GetPersonnels();
            dataGridPersonnel.DataSource = personnels;
        }

        private void dataGridPersonnel_SelectionChanged(object sender, EventArgs e)
        {
            Personnel personnel = (Personnel)dataGridPersonnel.CurrentRow.DataBoundItem;
            dataGridAbsence.DataSource = absenceController.GetAbsences(personnel.id);
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

        private void dataGridAbsence_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewColumn column = dataGridAbsence.Columns[e.ColumnIndex];
            if (column.DataPropertyName.Contains("."))
            {
                object data;

                if (column.DataPropertyName.EndsWith(".dateString"))
                {
                    data = dataGridAbsence.Rows[e.RowIndex].DataBoundItem;
                    string propertie = column.DataPropertyName.Split('.')[0];
                    DateTime date = (DateTime)data.GetType().GetProperty(propertie).GetValue(data);
                    dataGridAbsence.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = date.ToString("MM/dd/yyyy HH:mm");
                    return;
                }

                data = dataGridAbsence.Rows[e.RowIndex].DataBoundItem;
                string[] properties = column.DataPropertyName.Split('.');
                for (int i = 0; i < properties.Length && data != null; i++)
                    data = data.GetType().GetProperty(properties[i]).GetValue(data);
                dataGridAbsence.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = data;
            }
        }

        private void btnAddPersonnel_Click(object sender, EventArgs e)
        {
            PersonnelDataInput personnelData = new PersonnelDataInput();

            if (personnelData.ShowDialog() == DialogResult.Cancel) return;

            personnelController.CreatePersonnel(personnelData.personnel);
            this.refreshPersonnelData();
        }

        private void btnEditPersonnel_Click(object sender, EventArgs e)
        {
            Personnel personnel = (Personnel)dataGridPersonnel.SelectedRows[0].DataBoundItem;
            PersonnelDataInput personnelData = new PersonnelDataInput(personnel);

            if (personnelData.ShowDialog() != DialogResult.OK) return;

            personnelController.UdpatePersonnel(personnelData.personnel);
            this.refreshPersonnelData();
        }

        private void btnDeletePersonnel_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Êtes-vous sur de vouloir supprimer cette personne ?", "Supprimer un personnel", MessageBoxButtons.YesNo);

            if (confirm != DialogResult.Yes) return;

            Personnel personnel = (Personnel)dataGridPersonnel.SelectedRows[0].DataBoundItem;
            personnelController.DeletePersonnels(personnel.id);
            this.refreshPersonnelData();
        }
    }
}
