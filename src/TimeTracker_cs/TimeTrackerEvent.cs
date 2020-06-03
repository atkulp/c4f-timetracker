using System;

namespace TimeTracker
{
    delegate void TimeTrackerEventHandler( object sender, TimeTrackerEvent e);
    delegate void TimeTrackerErrorHandler( object sender, TimeTrackerErrorEvent e);

    public class TimeTrackerErrorEvent : EventArgs
    {
        public TimeTrackerErrorEvent(string message) : this(message, null)
        {
        }

        public TimeTrackerErrorEvent(string message, Exception nestedException)
        {
            Message = message;
            InternalException = nestedException;
        }

        public Exception InternalException { get; }

        public string Message { get; }
    }

    public class TimeTrackerEvent : EventArgs
    {
        public TimeTrackerEvent(TimeTrackerDataSet.ProjectsRow projectEntry, TimeTrackerDataSet.TimeEntriesRow timeEntry)
        {
            ProjectEntry = projectEntry;
            TimeEntry = timeEntry;
        }

        public TimeTrackerDataSet.TimeEntriesRow TimeEntry { get; }

        public TimeTrackerDataSet.ProjectsRow ProjectEntry { get; }
    }
}
