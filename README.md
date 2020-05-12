# c4f-timetracker
MSDN - Coding 4 Fun - Desktop Time Tracker
_copied from [CodePlex archive](https://archive.codeplex.com/?p=timetracker)_


Keep track of time spent on your projects with this C\# Windows forms application that includes fully-commented source code. It runs from your system tray and makes it easy to punch in and out and view totals.


* * * * *

**Project Description**
Keep track of time spent on your projects with this C\# Windows forms application that includes fully-commented source code. It runs from your system tray and makes it easy to punch in and out and view totals.

Introduction
------------

Time and how we use it is a matter of great importance to many people. I am certainly no exception, frequently working on two to five projects at a given time. Whether it's just being curious about how much time you are spending on various classes at school, or billable projects, time tracking can be tedious. Do you use a paper notebook (remember those)? Do you create a spreadsheet in Excel? What about a custom time accounting software package? For this project, I decided it would be good to create a basic time tracking tool to sit quietly in the system tray.

Usage
-----

Just double-click the executable and the icon will appear in the notification area (the system tray).

![ScreenCapture\_01 2009.04.14
00.04.jpg](docs/images/f0d4ca1a-4a4c-4a8c-b0f8-357f614aac01 "ScreenCapture_01 2009.04.14 00.04.jpg")

Right-click the icon in the system tray to view the menu. You can add projects, punch in/out, punch out at a specific time (like ten minutes ago when you should have punched out...) or view settings.

![ScreenCapture\_02 2009.04.14
00.05.jpg](docs/images/7e9c86f7-9ff0-4a09-a5f4-aa1da1840a88 "ScreenCapture_02 2009.04.14 00.05.jpg")

Click **Add Project** to enter the name for a project. Then you can simply double-click the icon to sign in or out. The tooltip always shows you in or out, in addition to the icon overlay. When you are ready to do some accounting, view the details:

![ScreenCapture\_03 2009.04.14
00.06.jpg](docs/images/30c09e04-87a9-4f28-a10b-a716e04adc8f "ScreenCapture_03 2009.04.14 00.06.jpg")

There isn't any way to export this information, but it wouldn't be that difficult to extend. All data is held in a DataTable serialized as XML. It would be a great challenge to extend this with the Microsoft ReportViewer control to create some cool output options.

Finally, you can enable or disable projects (just to cull down your list of inactive projects) in the second tab:

![ScreenCapture\_04 2009.04.14
00.06.jpg](docs/images/844e0075-64b6-4c1a-9354-5eff282224b0 "ScreenCapture_04 2009.04.14 00.06.jpg")

If you want commit permission to improve this, please let me know. It's served me well and can be even better.

This project has been tested up to Windows 7 RC1.

*This sample application was originally published as part of the MSDN Coding 4 Fun article [Keeping Track of Time](https://web.archive.org/web/20140919131056/http://channel9.msdn.com/coding4fun/articles/Keeping-Track-of-Time).*

* * * * *
