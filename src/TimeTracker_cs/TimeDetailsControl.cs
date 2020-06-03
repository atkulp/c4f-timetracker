using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TimeTracker
{
    public partial class TimeDetailsControl : UserControl
    {
        TimeTrackerDataSet ds;

        public TimeDetailsControl()
        {
            InitializeComponent();

            // Initialize DateTimePicker controls
            startDateTimePicker.Value = DateTime.Now.Date.AddDays(-30);
            endDateTimePicker.Value = DateTime.Now.Date;

        }

        public TimeTrackerDataSet Dataset
        {
            get
            {
                return ds;
            }
            set
            {
                if (value != null)
                {
                    ds = value;
                }
            }
        }


        private void updateButton_Click(object sender, EventArgs e)
        {
            UpdateProjectsList();
        }

        /// <summary>
        /// Iterates over list of projects to populate the
        /// hierarchical list.
        public void UpdateProjectsList()
        {
            if (ds == null) return;

            // Clear any existing entries
            this.projectsTreeView.Nodes.Clear();
            computeLabel.Text = "Calculating...";

            
            TimeSpan projectTime, totalCumulative = new TimeSpan();
            TreeNode projectNode;

            foreach (TimeTrackerDataSet.ProjectsRow pRow in ds.Projects.OrderBy(p => p.ProjectName))
            {
                projectNode = projectsTreeView.Nodes.Add(pRow.ProjectName);

                projectTime = AddProjectToList(projectNode, pRow);

                if (projectTime.Seconds == 0 && !showZeroHourCheckBox.Checked)
                {
                    projectNode.Remove();
                }
                else
                {
                    totalCumulative += projectTime;

                    projectNode.Text += FormatTimeSpan(projectTime);
                }
            }

            // At this point totalCumulative time truly is
            computeLabel.Text = $"Total time is {FormatTimeSpan(totalCumulative)}";
            
        }

        /// <summary>
        /// Iterates over list of time entries for a project to populate the
        /// hierarchical list.
        private TimeSpan AddProjectToList(TreeNode parent, TimeTrackerDataSet.ProjectsRow project)
        {
            TimeSpan entryTime, dayCumulative = new TimeSpan(), projectCumulative = new TimeSpan();
            TreeNode dayNode = null, entryNode;
            DateTime projectDay = DateTime.MinValue;

            foreach (TimeTrackerDataSet.TimeEntriesRow tRow in project.GetTimeEntriesRows())
            {
                // Ignore open entries
                if (tRow.IsEndTimeNull()) continue;

                // Scope for date range (not the most efficient...)
                if (tRow.StartTime.Date < startDateTimePicker.Value.Date ||
                    tRow.EndTime.Date > endDateTimePicker.Value.Date) continue;

                // See if date has rolled over
                if (tRow.StartTime.Date > projectDay)
                {
                    if (dayNode != null)
                    {
                        dayNode.Text += FormatTimeSpan(dayCumulative);
                        dayCumulative = new TimeSpan();
                    }

                    projectDay = tRow.StartTime.Date;

                    dayNode = parent.Nodes.Add($"{projectDay:d} ");
                }

                entryTime = tRow.EndTime.Subtract(tRow.StartTime);

                // Add current time entry
                entryNode = dayNode.Nodes.Add($"{tRow.StartTime:t}-{tRow.EndTime:t} ");

                projectCumulative += entryTime;
                dayCumulative += entryTime;

                entryNode.Text += FormatTimeSpan(entryTime);
            }

            // Last day must be updated
            if (dayNode != null)
            {
                dayNode.Text += FormatTimeSpan(dayCumulative);
            }

            return projectCumulative;
        }

        /// <summary>
        /// Utility method to display a TimeSpan with a custom format.
        /// The default format goes to six digits of milliseconds, so
        /// is overkill.
        /// </summary>
        private string FormatTimeSpan(TimeSpan span)
        {
            StringBuilder buf = new StringBuilder();

            double totalHours = (span.Days * 24.0) + span.Hours + (span.Minutes / 60.0);
            buf.AppendFormat(" ({0:#0.#} hours", totalHours);
            buf.AppendFormat(" - {0:#0} hrs, {1:00} mins, {2:00 secs})",
                ((span.Days * 24.0) + span.Hours), span.Minutes, span.Seconds);

            return buf.ToString();
        }

    }
}
