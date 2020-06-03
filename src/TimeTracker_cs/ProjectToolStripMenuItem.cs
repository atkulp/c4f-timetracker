using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TimeTracker
{
    public class ProjectToolStripMenuItem : ToolStripMenuItem
    {
        public TimeTracker.TimeTrackerDataSet.ProjectsRow ProjectRow { get; set; }

        public ProjectToolStripMenuItem(TimeTracker.TimeTrackerDataSet.ProjectsRow projectRow)
        {
            this.Name = $"MenuItem:Project{projectRow.ProjectID}";
            this.Text = projectRow.ProjectName;

            ProjectRow = projectRow;
            this.Visible = projectRow.Active;
        }
    }
}
