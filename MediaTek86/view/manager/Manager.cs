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
            dataGridPersonnel.AutoGenerateColumns = false;
            this.refreshPersonnelData();

            // Création des colonnes
            DataGridViewTextBoxColumn nomColumn = new DataGridViewTextBoxColumn();
            nomColumn.Name = "surname";
            nomColumn.DataPropertyName = "nom";
            nomColumn.HeaderText = "Nom";

            DataGridViewTextBoxColumn prenomColumn = new DataGridViewTextBoxColumn();
            prenomColumn.Name = "name";
            prenomColumn.DataPropertyName = "prenom";
            prenomColumn.HeaderText = "Prénom";

            DataGridViewTextBoxColumn telColumn = new DataGridViewTextBoxColumn();
            telColumn.Name = "phone";
            telColumn.DataPropertyName = "tel";
            telColumn.HeaderText = "Téléphone";

            DataGridViewTextBoxColumn mailColumn = new DataGridViewTextBoxColumn();
            mailColumn.Name = "email";
            mailColumn.DataPropertyName = "mail";
            mailColumn.HeaderText = "Email";

            DataGridViewTextBoxColumn serviceColumn = new DataGridViewTextBoxColumn();
            serviceColumn.Name = "serviceName";
            serviceColumn.DataPropertyName = "service.nom";
            serviceColumn.HeaderText = "Service";

            // Ajout des colonnes au DataGridView `dataGridPersonnel`
            dataGridPersonnel.Columns.AddRange(nomColumn, prenomColumn, telColumn, mailColumn, serviceColumn);

            //// ABSENCE DATAGRID
            dataGridAbsence.AutoGenerateColumns = false;

            // Création des colonnes
            DataGridViewTextBoxColumn debutColumn = new DataGridViewTextBoxColumn();
            debutColumn.Name = "dateStart";
            debutColumn.DataPropertyName = "dateDebut.dateString";
            debutColumn.HeaderText = "Date début";

            DataGridViewTextBoxColumn finColumn = new DataGridViewTextBoxColumn();
            finColumn.Name = "dateEnd";
            finColumn.DataPropertyName = "dateFin.dateString";
            finColumn.HeaderText = "Date fin";

            DataGridViewTextBoxColumn motifColumn = new DataGridViewTextBoxColumn();
            motifColumn.Name = "reason";
            motifColumn.DataPropertyName = "motif.libelle";
            motifColumn.HeaderText = "Motif";

            // Ajout des colonnes au DataGridView `dataGridPersonnel`
            dataGridAbsence.Columns.AddRange(debutColumn, finColumn, motifColumn);
        }

        // Refresh content of the personnel data grid
        private void refreshPersonnelData()
        {
            List<Personnel> personnels = personnelController.GetPersonnels();

            personnels.Sort((x, y) => x.nom.CompareTo(y.nom));

            dataGridPersonnel.DataSource = personnels;
        }

        private void refreshAbsenceData()
        {
            Personnel personnel = (Personnel)dataGridPersonnel.CurrentRow.DataBoundItem;
            List<Absence> absences = absenceController.GetAbsences(personnel.id);

            absences.Sort((x, y) => x.dateDebut.CompareTo(y.dateDebut));

            dataGridAbsence.DataSource = absences;
        }

        private void dataGridPersonnel_SelectionChanged(object sender, EventArgs e)
        {
            this.refreshAbsenceData();
           // Personnel personnel = (Personnel)dataGridPersonnel.CurrentRow.DataBoundItem;
           // dataGridAbsence.DataSource = absenceController.GetAbsences(personnel.id);
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
                    dataGridAbsence.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = date.ToString("dd/MM/yyyy HH:mm:ss");
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

        private void btnAddAbsence_Click(object sender, EventArgs e)
        {
            Personnel personnel = (Personnel)dataGridPersonnel.SelectedRows[0].DataBoundItem;
            AbsenceDataInput absenceData = new AbsenceDataInput(personnel);

            if (absenceData.ShowDialog() != DialogResult.OK) return;

            try
            {
                absenceController.CreateAbsence(absenceData.absence);
            }
            catch
            {
                MessageBox.Show("Une erreur est survenue.\nLa date de début est peut-être deja défini pour cet utilisateur", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.refreshAbsenceData();
        }

        private void btnEditAbsence_Click(object sender, EventArgs e)
        {
            Personnel personnel = (Personnel)dataGridPersonnel.SelectedRows[0].DataBoundItem;
            Absence absence = (Absence)dataGridAbsence.SelectedRows[0].DataBoundItem;
            AbsenceDataInput absenceData = new AbsenceDataInput(personnel, absence);

            if (absenceData.ShowDialog() != DialogResult.OK) return;

            absenceController.UpdateAbsence(absenceData.absence);
            this.refreshAbsenceData();
        }

        private void btnRemoveAbsence_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Êtes-vous sur de vouloir supprimer cette absence ?", "Supprimer une absence", MessageBoxButtons.YesNo);
            
            if (confirm != DialogResult.Yes) return;
            
            Absence absence = (Absence)dataGridAbsence.SelectedRows[0].DataBoundItem;

            absenceController.DeleteAbsence(absence.personnel.id, absence.dateDebut);
            this.refreshAbsenceData();
        }
    }
}
