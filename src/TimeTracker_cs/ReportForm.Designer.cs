namespace TimeTracker
{
    partial class ReportForm
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.TimeTrackerDataSet = new TimeTracker.TimeTrackerDataSet();
            this.ProjectsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.TimeEntriesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.TimeTrackerDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProjectsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeEntriesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "TimeTrackerDataSet_Projects";
            reportDataSource1.Value = this.ProjectsBindingSource;
            reportDataSource2.Name = "TimeTrackerDataSet_TimeEntries";
            reportDataSource2.Value = this.TimeEntriesBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "TimeTracker.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(292, 266);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // TimeTrackerDataSet
            // 
            this.TimeTrackerDataSet.DataSetName = "TimeTrackerDataSet";
            this.TimeTrackerDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ProjectsBindingSource
            // 
            this.ProjectsBindingSource.DataMember = "Projects";
            this.ProjectsBindingSource.DataSource = this.TimeTrackerDataSet;
            // 
            // TimeEntriesBindingSource
            // 
            this.TimeEntriesBindingSource.DataMember = "TimeEntries";
            this.TimeEntriesBindingSource.DataSource = this.TimeTrackerDataSet;
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReportForm";
            this.Text = "ReportForm";
            this.Load += new System.EventHandler(this.ReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TimeTrackerDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProjectsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeEntriesBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ProjectsBindingSource;
        private System.Windows.Forms.BindingSource TimeEntriesBindingSource;
        public TimeTrackerDataSet TimeTrackerDataSet;
    }
}