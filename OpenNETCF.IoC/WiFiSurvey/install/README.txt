OpenNETCF WiFiSurvey
README - June 24, 2009
====================

The application consists of two pieces: a Windows CE/WinMo device piece and an optional desktop application.

Device Application
------------------
Requires: .NET Compact Framework 3.5, SQL Compact 3.5

The device application reports the currently associated Access Point as well as the list of audible/available access points.  It refreshes this list periodically to provide a site survey and each time it is refreshed the data for both the associated and the available APs is stored in a local SQL Compact database.

The History tab records every time the AP association changes and every time desktop connectivity status changes.  These events are also stored in the database.


PC Application
--------------
Requires: .NET Framework 3.5

The PC application is optional.  It is a simple UDP listener that will report devices that connect to it.  The purpose of this application is to help diagnose and report conditions where a device may be running and connected to an Access Point, but is unable to route network traffic to another machine on the same subnet.

When a device loses routability, the desktop application will report the loss and begin a timer. 
