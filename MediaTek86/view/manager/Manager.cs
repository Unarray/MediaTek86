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

        /// <summary>
        /// Manager form constructor
        /// </summary>
        public Manager()
        {
            this.personnelController = new PersonnelController();
            this.absenceController = new AbsenceController();
            this.InitializeComponent();

            //// PERSONNEL DATAGRID
            this.dataGridPersonnel.AutoGenerateColumns = false;
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
            this.dataGridPersonnel.Columns.AddRange(nomColumn, prenomColumn, telColumn, mailColumn, serviceColumn);


            //// ABSENCE DATAGRID
            this.dataGridAbsence.AutoGenerateColumns = false;

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
            this.dataGridAbsence.Columns.AddRange(debutColumn, finColumn, motifColumn);
        }

        // Refresh content of the personnel data grid
        private void refreshPersonnelData()
        {
            List<Personnel> personnels = this.personnelController.GetPersonnels();

            personnels.Sort((x, y) => x.nom.CompareTo(y.nom));

            this.dataGridPersonnel.DataSource = personnels;
        }

        private void refreshAbsenceData()
        {
            Personnel personnel = (Personnel)this.dataGridPersonnel.CurrentRow.DataBoundItem;
            List<Absence> absences = this.absenceController.GetAbsences(personnel.id);

            absences.Sort((x, y) => x.dateDebut.CompareTo(y.dateDebut));

            this.dataGridAbsence.DataSource = absences;
        }

        private void dataGridPersonnel_SelectionChanged(object sender, EventArgs e)
        {
            this.refreshAbsenceData();
        }

        /// <summary>
        /// Format cell permit to render property of object property for dataGridPersonnel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridPersonnel_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Get values from child object
            DataGridViewColumn column = this.dataGridPersonnel.Columns[e.ColumnIndex];
            if (column.DataPropertyName.Contains("."))
            {
                object data = this.dataGridPersonnel.Rows[e.RowIndex].DataBoundItem;
                string[] properties = column.DataPropertyName.Split('.');
                for (int i = 0; i < properties.Length && data != null; i++)
                    data = data.GetType().GetProperty(properties[i]).GetValue(data);
                this.dataGridPersonnel.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = data;
            }
        }

        /// <summary>
        /// Format cell permit to render property of object property for dataGridAbsence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridAbsence_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewColumn column = this.dataGridAbsence.Columns[e.ColumnIndex];
            if (column.DataPropertyName.Contains("."))
            {
                object data;
                // Render a dates
                if (column.DataPropertyName.EndsWith(".dateString"))
                {
                    data = this.dataGridAbsence.Rows[e.RowIndex].DataBoundItem;
                    string propertie = column.DataPropertyName.Split('.')[0];
                    DateTime date = (DateTime)data.GetType().GetProperty(propertie).GetValue(data);
                    this.dataGridAbsence.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = date.ToString("dd/MM/yyyy HH:mm:ss");
                    return;
                }

                data = this.dataGridAbsence.Rows[e.RowIndex].DataBoundItem;
                string[] properties = column.DataPropertyName.Split('.');
                for (int i = 0; i < properties.Length && data != null; i++)
                    data = data.GetType().GetProperty(properties[i]).GetValue(data);
                this.dataGridAbsence.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = data;
            }
        }

        /// <summary>
        /// Add personnel to DB & dataGridPersonnel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddPersonnel_Click(object sender, EventArgs e)
        {
            PersonnelDataInput personnelData = new PersonnelDataInput();

            if (personnelData.ShowDialog() == DialogResult.Cancel) return;

            this.personnelController.CreatePersonnel(personnelData.personnel);
            this.refreshPersonnelData();
        }

        /// <summary>
        /// Edit personnel from DB & dataGridPersonnel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditPersonnel_Click(object sender, EventArgs e)
        {
            Personnel personnel = (Personnel)this.dataGridPersonnel.SelectedRows[0].DataBoundItem;
            PersonnelDataInput personnelData = new PersonnelDataInput(personnel);

            if (personnelData.ShowDialog() != DialogResult.OK) return;

            this.personnelController.UdpatePersonnel(personnelData.personnel);
            this.refreshPersonnelData();
        }

        /// <summary>
        /// Delete personnel from DB & dataGridPersonnel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeletePersonnel_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Êtes-vous sur de vouloir supprimer cette personne ?", "Supprimer un personnel", MessageBoxButtons.YesNo);

            if (confirm != DialogResult.Yes) return;

            Personnel personnel = (Personnel)this.dataGridPersonnel.SelectedRows[0].DataBoundItem;

            this.personnelController.DeletePersonnels(personnel.id);
            this.absenceController.DeleteAllAbsences(personnel.id);
            this.refreshPersonnelData();
        }

        /// <summary>
        /// Add absence to DB & dataGridAbsence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddAbsence_Click(object sender, EventArgs e)
        {
            Personnel personnel = (Personnel)this.dataGridPersonnel.SelectedRows[0].DataBoundItem;
            AbsenceDataInput absenceData = new AbsenceDataInput(personnel);

            if (absenceData.ShowDialog() != DialogResult.OK) return;

            try
            {
                this.absenceController.CreateAbsence(absenceData.absence);
            }
            catch
            {
                MessageBox.Show("Une erreur est survenue.\nLa date de début est peut-être deja défini pour cet utilisateur", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.refreshAbsenceData();
        }

        // Edit absence from DB & dataGridAbsence
        private void btnEditAbsence_Click(object sender, EventArgs e)
        {
            Personnel personnel = (Personnel)this.dataGridPersonnel.SelectedRows[0].DataBoundItem;
            Absence absence = (Absence)this.dataGridAbsence.SelectedRows[0].DataBoundItem;
            AbsenceDataInput absenceData = new AbsenceDataInput(personnel, absence);

            if (absenceData.ShowDialog() != DialogResult.OK) return;

            this.absenceController.UpdateAbsence(absenceData.absence);
            this.refreshAbsenceData();
        }

        /// <summary>
        /// Remove absence from DB & dataGridAbsence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveAbsence_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Êtes-vous sur de vouloir supprimer cette absence ?", "Supprimer une absence", MessageBoxButtons.YesNo);
            
            if (confirm != DialogResult.Yes) return;
            
            Absence absence = (Absence)this.dataGridAbsence.SelectedRows[0].DataBoundItem;

            this.absenceController.DeleteAbsence(absence.personnel.id, absence.dateDebut);
            this.refreshAbsenceData();
        }
    }
}
