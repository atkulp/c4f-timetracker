using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TimeTracker
{
    public class ProjectToolStripMenuItem : ToolStripMenuItem
    {
        private TimeTracker.TimeTrackerDataSet.ProjectsRow _projectRow;

        public TimeTracker.TimeTrackerDataSet.ProjectsRow ProjectRow
        {
            get { return _projectRow; }
            set { _projectRow = value; }
        }

        public ProjectToolStripMenuItem(TimeTracker.TimeTrackerDataSet.ProjectsRow projectRow)
        {
            this.Name = "MenuItem:Project" + projectRow.ProjectID;
            this.Text = projectRow.ProjectName;

            _projectRow = projectRow;
            this.Visible = projectRow.Active;

        }
    }
}
