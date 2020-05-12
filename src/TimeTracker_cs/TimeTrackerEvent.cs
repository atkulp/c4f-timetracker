using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTracker
{
    delegate void TimeTrackerEventHandler( object sender, TimeTrackerEvent e);
    delegate void TimeTrackerErrorHandler( object sender, TimeTrackerErrorEvent e);

    public class TimeTrackerErrorEvent : EventArgs
    {
        private Exception _internalException;
        private string _message;

        public TimeTrackerErrorEvent(string message) : this(message, null)
        {
        }

        public TimeTrackerErrorEvent(string message, Exception nestedException)
        {
            _message = message;
            _internalException = nestedException;
        }

        public Exception InternalException
        {
            get { return _internalException; }
        }

        public string Message
        {
            get { return _message; }
        }
    }

    public class TimeTrackerEvent : EventArgs
    {
        private TimeTrackerDataSet.TimeEntriesRow _timeEntry;
        private TimeTrackerDataSet.ProjectsRow _projectEntry;

        public TimeTrackerEvent(TimeTrackerDataSet.ProjectsRow projectEntry, TimeTrackerDataSet.TimeEntriesRow timeEntry)
        {
            _projectEntry = projectEntry;
            _timeEntry = timeEntry;
        }

        public TimeTrackerDataSet.TimeEntriesRow TimeEntry
        {
            get { return _timeEntry; }
        }

        public TimeTrackerDataSet.ProjectsRow ProjectEntry
        {
            get { return _projectEntry; }
        }
    }
}
