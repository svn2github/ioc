using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OpenNETCF.IoC;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Schedule.Module")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("OpenNETCF Consulting, LLC")]
[assembly: AssemblyProduct("Schedule.Module")]
[assembly: AssemblyCopyright("Copyright ©2011 OpenNETCF Consulting, LLC")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("0d4d828a-04f6-4f73-aec8-3bd1f5aac8e8")]

[assembly: AssemblyVersion("0.9")]

[assembly:InternalsVisibleTo("Schedule.Unit.Test")]

[assembly:IoCModuleEntry(typeof(OpenNETCF.Schedule.Module))]

