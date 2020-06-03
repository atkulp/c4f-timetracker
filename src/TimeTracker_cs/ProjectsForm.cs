using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows.Forms;

using Microsoft.Win32;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;

namespace TimeTracker
{
    public partial class ProjectsForm : Form
    {
        readonly string notifyIconTitle = "Time Tracker";
        DateTime standbyTime = DateTime.MinValue;

        // Keep track of all project menu items so nothing is lost
        // when they go out of scope.
        List<ToolStripMenuItem> projectMenuItems =
            new List<ToolStripMenuItem>();

        // Keeping tracking of the current project menu item makes
        // it easier to uncheck the previous entery when a new one
        // is clicked.
        ProjectToolStripMenuItem currentProjectMenuItem = null;

        // Shared event handler for all projects when selected.
        EventHandler projectSelectEventHandler;

        DataTier dt;

        public ProjectsForm()
        {
            InitializeComponent();

            dt = new DataTier(timeTrackerDataSet);

            // Deal with standby/resume, logoff, application exit
            SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);
            SystemEvents.SessionEnding += new SessionEndingEventHandler(SystemEvents_SessionEnding);
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

            // The punch in/out menu item is not available until
            // a default project is selected.
            punchInOutToolStripMenuItem.Enabled = false;

            projectSelectEventHandler =
                new EventHandler(projectSelectMenuItem_Click);

            dt.UserPunchedIn += new TimeTrackerEventHandler(dt_UserPunchedIn);
            dt.UserPunchedOut += new TimeTrackerEventHandler(dt_UserPunchedOut);
            dt.ProjectAdded += new TimeTrackerEventHandler(dt_ProjectAdded);
            dt.ProjectModified += new TimeTrackerEventHandler(dt_ProjectModified);
            dt.ProjectDeleted += new TimeTrackerEventHandler(dt_ProjectDeleted);
            dt.TimeTrackerError += new TimeTrackerErrorHandler(dt_TimeTrackerError);

            projectsListControl1.Dataset = timeTrackerDataSet;
            timeDetailsControl1.Dataset = timeTrackerDataSet;

            PopulateProjectsMenu();

        }

        void dt_TimeTrackerError(object sender, TimeTrackerErrorEvent e)
        {
            timeTrackerNotifyIcon.ShowBalloonTip(10000, "Error!", e.Message, ToolTipIcon.Error);
        }

        void UpdatePunchInOutOptions()
        {
            string tooltip = "", projectName = dt.CurrentProjectRow.ProjectName;

            if (dt.UserCheckedIn)
            {
                if (currentProjectMenuItem != null)
                {
                    punchInOutToolStripMenuItem.Text = "Punch Out";
                    punchOutAtToolStripMenuItem.Enabled = true;

                    tooltip = $"{notifyIconTitle} - Punched in @ {DateTime.Now:t} ({projectName})";

                    timeTrackerNotifyIcon.Icon = Properties.Resources.ClockIcon_in;
                }
            }
            else
            {
                punchInOutToolStripMenuItem.Text = "Punch In";
                punchOutAtToolStripMenuItem.Enabled = false;

                // Even if already punched out, update the text
                tooltip = $"{notifyIconTitle} - Punched out ({projectName})";

                timeTrackerNotifyIcon.Icon = Properties.Resources.ClockIcon;
            }

                if (tooltip.Length > 63) tooltip = tooltip.Substring(0, 60) + "...";
            timeTrackerNotifyIcon.Text = tooltip;
        }

        void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            // User is logging out of Windows (or shutting down)
            dt.PunchOut();
        }

        void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (dt.UserCheckedIn)
            {
                // User is going into Standby.  Record time this occured
                if (e.Mode == PowerModes.Suspend)
                {
                    standbyTime = DateTime.Now;
                }
                else if (e.Mode == PowerModes.Resume && standbyTime != DateTime.MinValue)
                {
                    string msg = string.Format("You were signed in during standby.  " + 
                        "Do you want to log out as of when the system went into " +
                        "standby ({0:t})?", standbyTime);

                    // Upon resume, see if they want to sign out retro to standby
                    if (MessageBox.Show(msg, "Time tracker", MessageBoxButtons.YesNo)
                        == DialogResult.Yes)
                    {
                        dt.PunchOut(standbyTime);
                    }

                    standbyTime = DateTime.MinValue;
                }
            }
        }

        void Application_ApplicationExit(object sender, EventArgs e)
        {
            dt.PunchOut();
            this.timeTrackerNotifyIcon.Visible = false;
        }

        /// <summary>
        /// Event handler for the punch in/out menu item.
        /// When it is clicked, alternate between in and out, and manage
        /// the time entry accordingly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void punchInOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Make sure there is a valid project
            if (currentProjectMenuItem == null) return;

            // When punching in, create a new time entry row,
            // set the project ID, and set the start time
            if (punchInOutToolStripMenuItem.Text == "Punch In")
            {
                dt.PunchIn();
            }
            else
            {
                dt.PunchOut();
            }
        }

        private void punchOutAtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string prompt = $"Please specify punch out time (In at {dt.CurrentTimeEntryRow.StartTime:HH:mm})";

            StringInputForm input =
                new StringInputForm(prompt,
                DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
                "00/00/0000 90:00",
                typeof(System.DateTime));

            if (input.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DateTime outTime = DateTime.Parse(input.Response);
                    if (outTime.CompareTo(dt.CurrentTimeEntryRow.StartTime) > 0)
                    {
                        dt.PunchOut(outTime);
                    }
                    else
                    {
                        MessageBox.Show("You can't punch out prior to punch in time!");
                    }
                }
                catch
                {
                    MessageBox.Show("The time/date you entered was invalid.  Please try again.");
                }
            }
        }

        /// <summary>
        /// Event handler for the View Time Details menu item.  Causes the
        /// main form to appear.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewTimeDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Visible = true;
        }

        /// <summary>
        /// Event handler for the Exit menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dt.PunchOut();
            this.timeTrackerNotifyIcon.Visible = false;
            Application.Exit();
        }

        /// <summary>
        /// Event handler for the Add Project menu item.  When clicked,
        /// prompt the user for a project name using the StringInputForm.
        /// A new ProjectsRow is then created, added to the dataset, and
        /// reflected in the menu.  The DataSet is then saved to disk.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringInputForm input =
                new StringInputForm("Please enter a name for the new project");

            if (input.ShowDialog() == DialogResult.OK)
            {
                dt.AddNewProject(input.Response);
            }
        }

        /// <summary>
        /// Adds a new project menu item given a ProjectsRow object.
        /// </summary>
        /// <param name="newProjectRow"></param>
        private ToolStripMenuItem AddProjectMenuItem(TimeTracker.TimeTrackerDataSet.ProjectsRow newProjectRow)
        {
            ProjectToolStripMenuItem newMenuItem = new ProjectToolStripMenuItem(newProjectRow);

            newMenuItem.Click += projectSelectEventHandler;

            this.changeProjectToolStripMenuItem.DropDownItems.AddRange(
                new System.Windows.Forms.ToolStripItem[] {
            newMenuItem});

            projectMenuItems.Add(newMenuItem);

            return newMenuItem;
        }

        /// <summary>
        /// Event handler invoked when any project is clicked in the menu.
        /// Alternate the checkboxes, and make the Punch In menu item
        /// enabled.  If the user was already punched in, then the
        /// existing project is first punched out.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void projectSelectMenuItem_Click(object sender, EventArgs e)
        {
            if (currentProjectMenuItem != null)
            {
                currentProjectMenuItem.Checked = false;
            }

            ProjectToolStripMenuItem currentItem = (ProjectToolStripMenuItem)sender;
            currentItem.Checked = true;

            // With a project selected, the user can check in or out
            punchInOutToolStripMenuItem.Enabled = true;

            currentProjectMenuItem = currentItem;

            // Remember the selected project
            string projectName = currentItem.ProjectRow.ProjectName;
            Properties.Settings.Default.LastSelectedProject = projectName;

            Properties.Settings.Default.Save();

            dt.CurrentProjectRow = currentItem.ProjectRow;

            // Update punch out status for new selected project
            UpdatePunchInOutOptions();
        }

        /// <summary>
        /// Event handler invoked when the Close button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeButton_Click(object sender, EventArgs e)
        {
            dt.SaveEntries();
            Visible = false;
        }

        /// <summary>
        /// Event handler invoked when ProjectsForm has visibility changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProjectsForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                timeDetailsControl1.UpdateProjectsList();
            }
        }

        private void PopulateProjectsMenu()
        {
            // Fill projects into the menu, auto-select last
            foreach (TimeTracker.TimeTrackerDataSet.ProjectsRow row in dt.DataSet.Projects.Select("", "ProjectName"))
            {
                ToolStripMenuItem newItem = AddProjectMenuItem(row);
                if (row.ProjectName == Properties.Settings.Default.LastSelectedProject)
                {
                    projectSelectMenuItem_Click(newItem, null);

                    timeTrackerNotifyIcon.Text = $"{notifyIconTitle} - Punched out ({row.ProjectName})";
                }
            }
        }

        private void toolStripMenuItemExport_Click(object sender, EventArgs e)
        {
            ExportData();
        }

        private void ExportData()
        {
            // Load last set save location
            var savePath = string.IsNullOrWhiteSpace(Properties.Settings.Default.LastFileExportPath)
                ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TimeTracking.xlsx")
                : Properties.Settings.Default.LastFileExportPath;

            // Prompt for save path
            var saveFileDialog1 = new SaveFileDialog
            {
                Title = "Export in Excel Format",
                Filter = "Excel Document|*.xlsx",
                FileName = savePath
            };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName != "")
            {
                savePath = saveFileDialog1.FileName;

                using (ExcelPackage pck = new ExcelPackage())
                {
                    using (ExcelWorksheet workSheet = pck.Workbook.Worksheets.Add("Time Entries"))
                    {
                        var tableCells = workSheet.Cells["A1:D1"];
                        tableCells.Style.Font.Bold = true;
                        tableCells.Style.Font.Color.SetColor(Color.Ivory);
                        tableCells.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        tableCells.Style.Fill.BackgroundColor.SetColor(Color.Navy);

                        var table = workSheet.Tables.Add(tableCells, "Entries");
                        table.Columns[0].Name = "Project";
                        table.Columns[1].Name = "Start";
                        table.Columns[2].Name = "Stop";
                        table.Columns[3].Name = "Duration (minutes)";
                        table.Columns[3].TotalsRowFunction = RowFunctions.Sum;
                        table.ShowHeader = true;
                        table.ShowTotal = true;

                        var row = 2;

                        foreach (var line in dt.DataSet.TimeEntries)
                        {
                            workSheet.InsertRow(row, 1);
                            workSheet.Cells[$"A{row}"].Value = line.ProjectsRow.ProjectName;
                            workSheet.Cells[$"B{row}"].Value = line.StartTime;
                            workSheet.Cells[$"B{row}"].Style.Numberformat.Format = "mm/dd/yyyy hh:mm";
                            workSheet.Cells[$"C{row}"].Value = line.EndTime;
                            workSheet.Cells[$"C{row}"].Style.Numberformat.Format = "mm/dd/yyyy hh:mm";
                            workSheet.Cells[$"D{row}"].Value = line.EndTime.Subtract(line.StartTime).TotalMinutes;
                            workSheet.Cells[$"D{row}"].Style.Numberformat.Format = "#.00";
                            row++;
                        }

                        // Set decimal format for total row sum
                        workSheet.Cells[$"D{row}"].Style.Numberformat.Format = "#.00";

                        workSheet.Cells.AutoFitColumns();
                        workSheet.Workbook.CalcMode = ExcelCalcMode.Automatic;

                        pck.SaveAs(new FileInfo(savePath));
                    }

                    // Update save location setting
                    Properties.Settings.Default.LastFileExportPath = savePath;
                    Properties.Settings.Default.Save();

                    // Find and display save location for convenience
                    System.Diagnostics.Process.Start("explorer.exe", $"/select,\"{savePath}\"");
                }
            }
        }

        #region "DataTier Event Handlers"
        void dt_UserPunchedIn(object sender, TimeTrackerEvent e)
        {
            UpdatePunchInOutOptions();
        }

        void dt_UserPunchedOut(object sender, TimeTrackerEvent e)
        {
            UpdatePunchInOutOptions();
        }

        void dt_ProjectAdded(object sender, TimeTrackerEvent e)
        {

            ToolStripMenuItem newMenuItem = AddProjectMenuItem(e.ProjectEntry);

            // Check new project right away if user not punched in
            if (!dt.UserCheckedIn)
            {
                projectSelectMenuItem_Click(newMenuItem, null);
            }
        }

        void dt_ProjectModified(object sender, TimeTrackerEvent e)
        {
            TimeTrackerDataSet.ProjectsRow row = e.ProjectEntry;

            // Modify the project menu list accordingly
            foreach (ToolStripMenuItem menuItem in projectMenuItems)
            {
                if (menuItem.Tag == row)
                {
                    // Update project menu item accordingly
                    menuItem.Text = row.ProjectName;
                    menuItem.Visible = row.Active;
                    break;
                }
            }
        }

        void dt_ProjectDeleted(object sender, TimeTrackerEvent e)
        {
            TimeTrackerDataSet.ProjectsRow row = e.ProjectEntry;
            ToolStripMenuItem menuItem = null;

            // Modify the project menu list accordingly
            foreach (ToolStripMenuItem mi in projectMenuItems)
            {
                if (mi.Tag == row)
                {
                    menuItem = mi;
                    break;
                }
            }

            if (menuItem != null)
            {
                menuItem.Visible = false;
                projectMenuItems.Remove(menuItem);

                // If the deleted project is the current project
                if (currentProjectMenuItem == menuItem)
                {
                    // Now there is no current project
                    currentProjectMenuItem = null;
                }
            }
        }
        #endregion

        private void locateXmlFilebutton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", $"/select,\"{dt.XmlDataPath}\"");
        }
    }
}