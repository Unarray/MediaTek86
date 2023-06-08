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
    /// <summary>
    /// Create a form to insert Absence datas
    /// </summary>
    public partial class AbsenceDataInput : Form
    {
        /// <summary>
        /// Absence object from inputs
        /// </summary>
        public Absence absence { get; set; }
        /// <summary>
        /// Base Absence passed in constructor
        /// </summary>
        public Absence oldAbsence;
        private readonly Personnel personnel;
        private readonly MotifController motifController;

        /// <summary>
        /// Constructor of AbsenceDataInput
        /// </summary>
        /// <param name="personnel">Personnel relative to this absence</param>
        /// <param name="absence">Absence object ot pre-fill inputs</param>
        public AbsenceDataInput(Personnel personnel, Absence absence = null)
        {
            this.InitializeComponent();

            this.oldAbsence = absence;
            this.personnel = personnel;
            this.motifController = new MotifController();
            List<Motif> motifs = motifController.GetMotifs();
            this.ddReason.DataSource = motifs;
            this.ddReason.DisplayMember = "libelle";
            this.ddReason.SelectedIndex = -1;

            if (absence is Absence)
            {
                this.Text = "Modifier une absence";
                this.dateStart.Value = absence.dateDebut;
                this.dateStart.Enabled = false;
                this.dateEnd.Value = absence.dateFin;
                this.ddReason.SelectedIndex = motifs.FindIndex(motif => motif.id == absence.motif.id);
            }
            else
            {
                this.Text = "Ajouter une absence";
            }
        }

        /// <summary>
        /// Triggered when validation btn is pushed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAccept_Click(object sender, EventArgs e)
        {
            DateTime start = this.dateStart.Value;
            DateTime end = this.dateEnd.Value;
            Motif motif = (Motif)this.ddReason.SelectedItem;

            if (start.CompareTo(end) > 0)
            {
                MessageBox.Show("La date de début est inférieur ou égale a la date de fin !", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (motif == null) return;

            if (this.oldAbsence != null)
            {
                DialogResult confirm = MessageBox.Show("Êtes-vous sûr de vouloir modifier cette absence ?", "Modifier cette absence ?", MessageBoxButtons.YesNo);

                if (confirm == DialogResult.No)
                {
                    this.Close();
                    return;
                }
            }

            this.absence = new Absence(
                personnel,
                start,
                end,
                motif
            );

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
