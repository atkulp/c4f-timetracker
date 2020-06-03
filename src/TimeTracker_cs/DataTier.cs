using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

using System.Data;
using System.Linq;
using System.Diagnostics;
using static TimeTracker.TimeTrackerDataSet;

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

        public string XmlDataPath { get; }
        public string XmlBackupPath { get; }

        public DataTier(TimeTrackerDataSet timeTrackerDataSet)
        {
            ds = timeTrackerDataSet;

            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            XmlDataPath = Path.Combine(folderPath, "TimeTracking.xml");
            XmlBackupPath = Path.Combine(folderPath, "TimeTracking.xml.bak");

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
            switch (e.Action)
            {
                case DataRowAction.Add:
                    RaiseProjectAdded(e.Row);
                    break;
                case DataRowAction.Change:
                    RaiseProjectModified(e.Row);
                    break;
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
            var newProjectRow = ds.Projects.NewProjectsRow();
            newProjectRow.ProjectName = projectName;
            ds.Projects.AddProjectsRow(newProjectRow);

            SaveEntries();
        }

        private void CheckForOpenTimeEntries()
        {
            // Check for any open time entries to close
            var openTimeEntryRows = (TimeEntriesRow[])ds.TimeEntries.Select("EndTime is NULL");

            if (openTimeEntryRows.Length > 0)
            {
                foreach (var row in openTimeEntryRows)
                {
                    row.EndTime = DateTime.Now;
                }

                SaveEntries();

                RaiseTimeTrackerError($"Not all time entries ({openTimeEntryRows.Length}) were closed.");
            }
        }

        public void PunchIn()
        {
            //If already punched in, no need to punch in
            if (_currentTimeEntryRow != null) return;

            // This should never happen...
            Debug.Assert(_currentProjectRow != null, "Unable to punch in when CurrentProjectRow is not set");

            _currentTimeEntryRow = ds.TimeEntries.NewTimeEntriesRow();
            _currentTimeEntryRow.StartTime = DateTime.Now;
            _currentTimeEntryRow.ProjectsRow = _currentProjectRow;
            ds.TimeEntries.AddTimeEntriesRow(_currentTimeEntryRow);

            RaiseUserPunchedIn(_currentProjectRow, _currentTimeEntryRow);
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
            try
            {
                ds.ReadXml(XmlDataPath, XmlReadMode.IgnoreSchema);
            }
            catch (Exception)
            {
                //success = ReadBackupEntries();
                RaiseTimeTrackerError("Error reading time entries and no good backup found.  Using new database.");
            }
        }

        private bool ReadBackupEntries()
        {
            bool success = false;

            try
            {
                if (File.Exists(XmlBackupPath))
                {
                    ds.ReadXml(XmlBackupPath, XmlReadMode.IgnoreSchema);

                    if (File.Exists(XmlDataPath))
                        File.Delete(XmlDataPath);

                    File.Copy(XmlBackupPath, XmlDataPath);

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
                //if (File.Exists(XmlBackupPath))
                //    File.Delete(XmlBackupPath);

                //if (File.Exists(XmlDataPath))
                //    File.Move(XmlDataPath, XmlBackupPath);

                //ds.AcceptChanges();

                ds.WriteXml(XmlDataPath, XmlWriteMode.IgnoreSchema);
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
            ProjectAdded?.Invoke(this, new TimeTrackerEvent(projectEntry, null));
        }

        private void RaiseProjectModified(TimeTrackerDataSet.ProjectsRow projectEntry)
        {
            ProjectModified?.Invoke(this, new TimeTrackerEvent(projectEntry, null));
        }

        private void RaiseProjectDeleted(TimeTrackerDataSet.ProjectsRow projectEntry)
        {
            ProjectDeleted?.Invoke(this, new TimeTrackerEvent(projectEntry, null));
        }

        private void RaiseUserPunchedIn(TimeTrackerDataSet.ProjectsRow projectEntry, TimeTrackerDataSet.TimeEntriesRow timeEntryRow)
        {
            UserPunchedIn?.Invoke(this, new TimeTrackerEvent(projectEntry, timeEntryRow));
        }

        private void RaiseUserPunchedOut(TimeTrackerDataSet.ProjectsRow projectEntry, TimeTrackerDataSet.TimeEntriesRow timeEntryRow)
        {
            UserPunchedOut?.Invoke(this, new TimeTrackerEvent(projectEntry, timeEntryRow));
        }

        private void RaiseTimeTrackerError(string message)
        {
            TimeTrackerError?.Invoke(this, new TimeTrackerErrorEvent(message));
        }

        private void RaiseTimeTrackerError(string message, Exception nestedException)
        {
            TimeTrackerError?.Invoke(this, new TimeTrackerErrorEvent(message, nestedException));
        }
        #endregion
    }
}
