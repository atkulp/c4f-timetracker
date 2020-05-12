using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

using System.Data;

namespace TimeTracker
{
    class DataTier
    {
        private TimeTrackerDataSet ds;

        // The current time entry row.  This makes it easier when
        // the user punches out.
        private TimeTrackerDataSet.TimeEntriesRow _currentTimeEntryRow = null;
        private TimeTrackerDataSet.ProjectsRow _currentProjectRow = null;

        public event TimeTrackerEventHandler ProjectAdded, ProjectModified, ProjectDeleted;
        public event TimeTrackerEventHandler UserPunchedIn, UserPunchedOut;
        public event TimeTrackerErrorHandler TimeTrackerError;

        public TimeTrackerDataSet DataSet
        {
            get { return ds; }
        }

        public DataTier(TimeTrackerDataSet timeTrackerDataSet)
        {
            ds = timeTrackerDataSet;

            LoadEntries();

            ds.Projects.ProjectsRowDeleted += new TimeTrackerDataSet.ProjectsRowChangeEventHandler(Projects_ProjectsRowDeleted);
            ds.Projects.ProjectsRowChanged += new TimeTrackerDataSet.ProjectsRowChangeEventHandler(Projects_ProjectsRowChanged);

            ds.TimeEntries.TimeEntriesRowChanged += new TimeTrackerDataSet.TimeEntriesRowChangeEventHandler(TimeEntries_TimeEntriesRowChanged);
            ds.TimeEntries.TimeEntriesRowDeleted += new TimeTrackerDataSet.TimeEntriesRowChangeEventHandler(TimeEntries_TimeEntriesRowDeleted);

            CheckForOpenTimeEntries();
        }

        public bool UserCheckedIn
        {
            get { return (_currentTimeEntryRow != null); }
        }

        public TimeTrackerDataSet.ProjectsRow CurrentProjectRow
        {
            get { return _currentProjectRow; }
            set
            {
                //Auto punch out if already checked in and changing current project
                if (UserCheckedIn)
                {
                    PunchOut();
                }
                _currentProjectRow = value;
            }
        }

        public TimeTrackerDataSet.TimeEntriesRow CurrentTimeEntryRow
        {
            get { return _currentTimeEntryRow; }
        }

        void TimeEntries_TimeEntriesRowDeleted(object sender, TimeTrackerDataSet.TimeEntriesRowChangeEvent e)
        {
            SaveEntries();
        }

        void TimeEntries_TimeEntriesRowChanged(object sender, TimeTrackerDataSet.TimeEntriesRowChangeEvent e)
        {
            SaveEntries();
        }

        void Projects_ProjectsRowChanged(object sender, TimeTrackerDataSet.ProjectsRowChangeEvent e)
        {
            if (e.Action == DataRowAction.Add)
            {
                RaiseProjectAdded(e.Row);
            }
            else if (e.Action == DataRowAction.Change)
            {
                RaiseProjectModified(e.Row);
            }
        }

        void Projects_ProjectsRowDeleted(object sender, TimeTrackerDataSet.ProjectsRowChangeEvent e)
        {
            if (e.Row == _currentProjectRow)
            {
                PunchOut();
                _currentProjectRow = null;
            }

            RaiseProjectDeleted(e.Row);
        }

        public void AddNewProject(string projectName)
        {
            TimeTracker.TimeTrackerDataSet.ProjectsRow newProjectRow =
                ds.Projects.NewProjectsRow();
            newProjectRow.ProjectName = projectName;
            ds.Projects.AddProjectsRow(newProjectRow);

            SaveEntries();
        }

        private void CheckForOpenTimeEntries()
        {
            // Check for any open time entries to close
            TimeTracker.TimeTrackerDataSet.TimeEntriesRow[] openTimeEntryRows =
                (TimeTracker.TimeTrackerDataSet.TimeEntriesRow[])
                ds.TimeEntries.Select("EndTime is NULL");

            if (openTimeEntryRows.Length > 0)
            {
                foreach (TimeTracker.TimeTrackerDataSet.TimeEntriesRow row in openTimeEntryRows)
                {
                    row.EndTime = DateTime.Now;
                }

                SaveEntries();

                RaiseTimeTrackerError("Not all time entries (" + openTimeEntryRows.Length +
                    ") were closed.");
            }
        }

        public void PunchIn()
        {
            //If already punched in, no need to punch in
            if (_currentTimeEntryRow != null) return;

            if (_currentProjectRow != null)
            {
                _currentTimeEntryRow = ds.TimeEntries.NewTimeEntriesRow();
                _currentTimeEntryRow.StartTime = DateTime.Now;
                _currentTimeEntryRow.ProjectsRow = _currentProjectRow;
                ds.TimeEntries.AddTimeEntriesRow(_currentTimeEntryRow);

                RaiseUserPunchedIn(_currentProjectRow, _currentTimeEntryRow);
            }
            else throw new InvalidOperationException("Unable to punch in when CurrentProjectRow is not set");
        }

        public void PunchOut()
        {
            PunchOut(DateTime.Now);
        }

        public void PunchOut(DateTime when)
        {
            // When punching out, set the end time and clear
            // the current entry
            if (_currentTimeEntryRow != null)
            {
                _currentTimeEntryRow.EndTime = when;

                TimeTrackerDataSet.TimeEntriesRow _punchedOutTimeEntryRow = _currentTimeEntryRow;

                _currentTimeEntryRow = null;

                RaiseUserPunchedOut(_currentProjectRow, _punchedOutTimeEntryRow);
            }
        }

        #region "Loading and saving"
        /// <summary>
        /// Loads the entries using XML deserialization
        /// </summary>
        public void LoadEntries()
        {
            bool success = false;
            try
            {
                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string fullPath = Path.Combine(folderPath, "TimeTracking.xml");
                ds.ReadXml(fullPath, XmlReadMode.IgnoreSchema);
                success = true;
            }
            catch (Exception x)
            {
                //success = ReadBackupEntries();
            }

            if (!success )
            {
                RaiseTimeTrackerError("Error reading time entries and no good backup found.  Using new database.");
            }
        }

        private bool ReadBackupEntries()
        {
            bool success = false;

            try
            {
                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string fullPath = Path.Combine(folderPath, "TimeTracking.xml");
                string fullPathBackup = Path.Combine(folderPath, "TimeTracking.xml.bak");

                if (File.Exists(fullPathBackup))
                {
                    ds.ReadXml(fullPathBackup, XmlReadMode.IgnoreSchema);

                    if (File.Exists(fullPath))
                        File.Delete(fullPath);

                    File.Copy(fullPathBackup, fullPath);

                    RaiseTimeTrackerError("Error occured reading data file.  Using backup copy (may be missing data).");

                    success = true;
                }
            }
            catch { }

            return success;
        }

        /// <summary>
        /// Saves the entries using XML serialization
        /// </summary>
        public void SaveEntries()
        {
            // Don't bother if no changes...
            if (!ds.HasChanges()) return;

            try
            {
                //if (File.Exists("TimeTracking.xml.bak"))
                //    File.Delete("TimeTracking.xml.bak");

                //if (File.Exists("TimeTracking.xml"))
                //    File.Move("TimeTracking.xml", "TimeTracking.xml.bak");

                //ds.AcceptChanges();
                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string fullPath = Path.Combine(folderPath, "TimeTracking.xml");

                ds.WriteXml(fullPath, XmlWriteMode.IgnoreSchema);
            }
            catch (Exception x)
            {
                RaiseTimeTrackerError("Error saving entries", x);
            }
        }
#endregion

        #region "Internal convenience methods for raising events"
        private void RaiseProjectAdded(TimeTrackerDataSet.ProjectsRow projectEntry)
        {
            if (ProjectAdded != null)
                ProjectAdded(this, new TimeTrackerEvent(projectEntry, null));
        }

        private void RaiseProjectModified(TimeTrackerDataSet.ProjectsRow projectEntry)
        {
            if (ProjectModified != null)
                ProjectModified(this, new TimeTrackerEvent(projectEntry, null));
        }

        private void RaiseProjectDeleted(TimeTrackerDataSet.ProjectsRow projectEntry)
        {
            if (ProjectDeleted != null)
                ProjectDeleted(this, new TimeTrackerEvent(projectEntry, null));
        }

        private void RaiseUserPunchedIn(TimeTrackerDataSet.ProjectsRow projectEntry, TimeTrackerDataSet.TimeEntriesRow timeEntryRow)
        {
            if (UserPunchedIn != null)
                UserPunchedIn(this, new TimeTrackerEvent(projectEntry, timeEntryRow));
        }

        private void RaiseUserPunchedOut(TimeTrackerDataSet.ProjectsRow projectEntry, TimeTrackerDataSet.TimeEntriesRow timeEntryRow)
        {
            if (UserPunchedOut != null)
                UserPunchedOut(this, new TimeTrackerEvent(projectEntry, timeEntryRow));
        }

        private void RaiseTimeTrackerError(string message)
        {
            if( TimeTrackerError != null )
                TimeTrackerError(this, new TimeTrackerErrorEvent(message));
        }

        private void RaiseTimeTrackerError(string message, Exception nestedException)
        {
            if( TimeTrackerError != null )
                TimeTrackerError(this, new TimeTrackerErrorEvent(message, nestedException));
        }
        #endregion
    }
}
