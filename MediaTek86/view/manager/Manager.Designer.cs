
namespace MediaTek86.view.manager
{
    partial class Manager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDeletePersonnel = new System.Windows.Forms.Button();
            this.btnEditPersonnel = new System.Windows.Forms.Button();
            this.btnAddPersonnel = new System.Windows.Forms.Button();
            this.dataGridPersonnel = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnRemoveAbsence = new System.Windows.Forms.Button();
            this.dataGridAbsence = new System.Windows.Forms.DataGridView();
            this.btnEditAbsence = new System.Windows.Forms.Button();
            this.btnAddAbsence = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPersonnel)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAbsence)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDeletePersonnel);
            this.groupBox1.Controls.Add(this.btnEditPersonnel);
            this.groupBox1.Controls.Add(this.btnAddPersonnel);
            this.groupBox1.Controls.Add(this.dataGridPersonnel);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(666, 368);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Personnel";
            // 
            // btnDeletePersonnel
            // 
            this.btnDeletePersonnel.Location = new System.Drawing.Point(167, 339);
            this.btnDeletePersonnel.Name = "btnDeletePersonnel";
            this.btnDeletePersonnel.Size = new System.Drawing.Size(75, 23);
            this.btnDeletePersonnel.TabIndex = 3;
            this.btnDeletePersonnel.Text = "supprimer";
            this.btnDeletePersonnel.UseVisualStyleBackColor = true;
            this.btnDeletePersonnel.Click += new System.EventHandler(this.btnDeletePersonnel_Click);
            // 
            // btnEditPersonnel
            // 
            this.btnEditPersonnel.Location = new System.Drawing.Point(86, 339);
            this.btnEditPersonnel.Name = "btnEditPersonnel";
            this.btnEditPersonnel.Size = new System.Drawing.Size(75, 23);
            this.btnEditPersonnel.TabIndex = 2;
            this.btnEditPersonnel.Text = "modifier";
            this.btnEditPersonnel.UseVisualStyleBackColor = true;
            this.btnEditPersonnel.Click += new System.EventHandler(this.btnEditPersonnel_Click);
            // 
            // btnAddPersonnel
            // 
            this.btnAddPersonnel.Location = new System.Drawing.Point(5, 339);
            this.btnAddPersonnel.Name = "btnAddPersonnel";
            this.btnAddPersonnel.Size = new System.Drawing.Size(75, 23);
            this.btnAddPersonnel.TabIndex = 1;
            this.btnAddPersonnel.Text = "ajouter";
            this.btnAddPersonnel.UseVisualStyleBackColor = true;
            this.btnAddPersonnel.Click += new System.EventHandler(this.btnAddPersonnel_Click);
            // 
            // dataGridPersonnel
            // 
            this.dataGridPersonnel.AllowUserToAddRows = false;
            this.dataGridPersonnel.AllowUserToDeleteRows = false;
            this.dataGridPersonnel.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridPersonnel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridPersonnel.Location = new System.Drawing.Point(6, 19);
            this.dataGridPersonnel.MultiSelect = false;
            this.dataGridPersonnel.Name = "dataGridPersonnel";
            this.dataGridPersonnel.ReadOnly = true;
            this.dataGridPersonnel.RowHeadersVisible = false;
            this.dataGridPersonnel.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridPersonnel.Size = new System.Drawing.Size(654, 314);
            this.dataGridPersonnel.TabIndex = 0;
            this.dataGridPersonnel.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridPersonnel_CellFormatting);
            this.dataGridPersonnel.SelectionChanged += new System.EventHandler(this.dataGridPersonnel_SelectionChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnRemoveAbsence);
            this.groupBox2.Controls.Add(this.dataGridAbsence);
            this.groupBox2.Controls.Add(this.btnEditAbsence);
            this.groupBox2.Controls.Add(this.btnAddAbsence);
            this.groupBox2.Location = new System.Drawing.Point(684, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(302, 368);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Absences";
            // 
            // btnRemoveAbsence
            // 
            this.btnRemoveAbsence.Location = new System.Drawing.Point(168, 339);
            this.btnRemoveAbsence.Name = "btnRemoveAbsence";
            this.btnRemoveAbsence.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveAbsence.TabIndex = 6;
            this.btnRemoveAbsence.Text = "supprimer";
            this.btnRemoveAbsence.UseVisualStyleBackColor = true;
            this.btnRemoveAbsence.Click += new System.EventHandler(this.btnRemoveAbsence_Click);
            // 
            // dataGridAbsence
            // 
            this.dataGridAbsence.AllowUserToAddRows = false;
            this.dataGridAbsence.AllowUserToDeleteRows = false;
            this.dataGridAbsence.AllowUserToOrderColumns = true;
            this.dataGridAbsence.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridAbsence.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridAbsence.Location = new System.Drawing.Point(7, 20);
            this.dataGridAbsence.MultiSelect = false;
            this.dataGridAbsence.Name = "dataGridAbsence";
            this.dataGridAbsence.ReadOnly = true;
            this.dataGridAbsence.RowHeadersVisible = false;
            this.dataGridAbsence.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridAbsence.Size = new System.Drawing.Size(289, 313);
            this.dataGridAbsence.TabIndex = 0;
            this.dataGridAbsence.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridAbsence_CellFormatting);
            // 
            // btnEditAbsence
            // 
            this.btnEditAbsence.Location = new System.Drawing.Point(87, 339);
            this.btnEditAbsence.Name = "btnEditAbsence";
            this.btnEditAbsence.Size = new System.Drawing.Size(75, 23);
            this.btnEditAbsence.TabIndex = 5;
            this.btnEditAbsence.Text = "modifier";
            this.btnEditAbsence.UseVisualStyleBackColor = true;
            this.btnEditAbsence.Click += new System.EventHandler(this.btnEditAbsence_Click);
            // 
            // btnAddAbsence
            // 
            this.btnAddAbsence.Location = new System.Drawing.Point(6, 339);
            this.btnAddAbsence.Name = "btnAddAbsence";
            this.btnAddAbsence.Size = new System.Drawing.Size(75, 23);
            this.btnAddAbsence.TabIndex = 4;
            this.btnAddAbsence.Text = "ajouter";
            this.btnAddAbsence.UseVisualStyleBackColor = true;
            this.btnAddAbsence.Click += new System.EventHandler(this.btnAddAbsence_Click);
            // 
            // Manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 392);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Manager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion du personnel";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPersonnel)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAbsence)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridPersonnel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDeletePersonnel;
        private System.Windows.Forms.Button btnEditPersonnel;
        private System.Windows.Forms.Button btnAddPersonnel;
        private System.Windows.Forms.DataGridView dataGridAbsence;
        private System.Windows.Forms.Button btnRemoveAbsence;
        private System.Windows.Forms.Button btnEditAbsence;
        private System.Windows.Forms.Button btnAddAbsence;
    }
}