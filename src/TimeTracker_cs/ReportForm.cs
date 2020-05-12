using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TimeTracker
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
            
        }

        public TimeTrackerDataSet ReportDataSet
        {
            get { return this.TimeTrackerDataSet; }
            set
            {
                this.TimeTrackerDataSet = value;
                this.TimeEntriesBindingSource.DataSource = value;
                this.ProjectsBindingSource.DataSource = value;

            }
        }
	
        private void ReportForm_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}