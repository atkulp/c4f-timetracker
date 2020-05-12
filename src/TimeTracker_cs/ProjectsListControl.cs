using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TimeTracker
{
    public partial class ProjectsListControl : UserControl
    {
        public ProjectsListControl()
        {
            InitializeComponent();
        }

        public TimeTrackerDataSet Dataset
        {
            get
            {
                return projectsBindingSource.DataSource as TimeTrackerDataSet;
            }
            set
            {
                if (value != null)
                {
                    projectsBindingSource.DataSource = value;
                }
            }
        }

        private void projectsDataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DataRowView rowView = e.Row.DataBoundItem as DataRowView;

            DialogResult resp = MessageBox.Show(
                "Deleting a project deletes all associated time entries.  " +
                "You may want to set it as inactive instead.\nPermanently delete this project?",
                "Delete project (" + rowView.Row["ProjectName"] + ") and all its time entries?",
                MessageBoxButtons.YesNo);

            if (resp == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

    }

}
