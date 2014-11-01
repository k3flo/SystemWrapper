﻿using System;
using System.IO;
using System.Security;
using System.Security.Permissions;

namespace SystemInterface
{
    using System.IO.MemoryMappedFiles;

    /// <summary>
    /// Provides information about, and means to manipulate, the current environment and platform.
    /// </summary>
    /// <remarks>
    /// This interface represents the <see cref="Environment"/> class.
    /// </remarks>
    public interface IEnvironment
    {
        /// <summary>
        /// Gets the command line for this process.
        /// </summary>
        /// <value>
        /// A string containing command-line arguments.
        /// </value>
        /// <remarks>
        /// <para>This property provides access to the program name and any arguments specified
        ///   on the command line when the current process was started.</para>
        /// <para>The program name can include path information, but is not required to do so.
        ///   Use the GetCommandLineArgs method to retrieve the command-line information parsed
        ///   and stored in an array of strings.</para>
        /// <para>The maximum size of the command-line buffer is not set to a specific number
        ///   of characters; it varies depending on the Windows operating system that is running
        ///   on the computer.</para>
        /// </remarks>
        /// <permission cref="EnvironmentPermission">
        /// For read access to the PATH environment variable. Associated enumeration: <see cref="EnvironmentPermissionAccess.Read"/>.
        /// </permission>
        string CommandLine { get; }

        /// <summary>
        /// Gets or sets the fully qualified path of the current working directory.
        /// </summary>
        /// <value>
        /// A string containing a directory path.
        /// </value>
        /// <remarks>
        /// By definition, if this process starts in the root directory of a local or network drive,
        /// the value of this property is the drive name followed by a trailing slash
        /// (for example, "C:\"). If this process starts in a subdirectory, the value of this property
        /// is the drive and subdirectory path, without a trailing slash (for example, "C:\mySubDirectory").
        /// </remarks>
        /// <exception cref="ArgumentException">Attempted to set to an empty string ("").</exception>
        /// <exception cref="ArgumentNullException">Attempted to set to null.</exception>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        /// <exception cref="DirectoryNotFoundException">Attempted to set a local path that cannot be found.</exception>
        /// <exception cref="SecurityException">The caller does not have the appropriate permission.</exception>
        /// <permission cref="SecurityPermission">
        /// For writing to files or directories in a set operation. Associated enumeration: <see cref="SecurityPermissionFlag.UnmanagedCode"/>.
        /// </permission>
        /// <permission cref="FileIOPermission">
        /// For access to the information in the path itself in a get operation. Associated enumeration: <see cref="FileIOPermissionAccess.PathDiscovery"/>.
        /// </permission>
        string CurrentDirectory { get; set; }

        /// <summary>
        /// Gets a unique identifier for the current managed thread.
        /// </summary>
        /// <value>
        /// An integer that represents a unique identifier for this managed thread.
        /// </value>
        int CurrentManagedThreadId { get; }

        /// <summary>
        /// Gets or sets the exit code of the process.
        /// </summary>
        /// <value>
        /// A 32-bit signed integer containing the exit code. The default value
        /// is 0 (zero), which indicates that the process completed successfully.
        /// </value>
        /// <remarks>
        /// <para>If the Main method returns void, you can use this property to set the exit
        ///   code that will be returned to the calling environment. If Main does not
        ///   return void, this property is ignored. The initial value of this
        ///   property is zero.</para>
        /// <para>Use a non-zero number to indicate an error. In your application,
        ///   you can define your own error codes in an enumeration, and return
        ///   the appropriate error code based on the scenario. For example, return
        ///   a value of 1 to indicate that the required file is not present and
        ///   a value of 2 to indicate that the file is in the wrong format.
        ///   For a list of exit codes used by the Windows operating system,
        ///   see System Error Codes in the Windows documentation.</para>
        /// </remarks>
        int ExitCode { get; set; }

        /// <summary>
        /// Gets a value that indicates whether the current application
        /// domain is being unloaded or the common language runtime
        /// (CLR) is shutting down.
        /// </summary>
        /// <value>
        /// true if the current application domain is being unloaded or the CLR is shutting down; otherwise, false.
        /// </value>
        /// <remarks>
        /// <para>When the CLR unloads an application domain, it runs the finalizers on all objects
        ///   that have a finalizer method in that application domain. When the CLR shuts down, it starts
        ///   the finalizer thread on all objects that have a finalizer method. The HasShutdownStarted
        ///   property returns true only after the finalizer thread has been started. When the property
        ///   returns true, you can determine whether an application domain is being unloaded or the CLR
        ///   itself is shutting down by calling the <see cref="AppDomain.IsFinalizingForUnload"/> method.
        ///   This method returns true if finalizers are called because the application domain is unloading
        ///   or false if the CLR is shutting down.</para>
        /// <para>The HasShutdownStarted property returns false if the finalizer thread has not been started.</para>
        /// <para>By using this property, you can determine whether to access static variables in your
        ///   finalization code. If either an application domain or the CLR is shutting down, you
        ///   cannot reliably access any object that has a finalization method and that is referenced
        ///   by a static field. This is because these objects may have already been finalized.</para>
        /// </remarks>
        bool HasShutdownStarted { get; }

        /// <summary>
        /// Determines whether the current operating system is a 64-bit operating system.
        /// </summary>
        /// <value>
        /// true if the operating system is 64-bit; otherwise, false.
        /// </value>
        bool Is64BitOperatingSystem { get; }

        /// <summary>
        /// Determines whether the current process is a 64-bit process.
        /// </summary>
        /// <value>
        /// true if the process is 64-bit; otherwise, false.
        /// </value>
        bool Is64BitProcess { get; }

        /// <summary>
        /// Gets the NetBIOS name of this local computer.
        /// </summary>
        /// <value>
        /// A string containing the name of this computer.
        /// </value>
        /// <exception cref="InvalidOperationException">The name of this computer cannot be obtained.</exception>
        /// <remarks>
        /// The name of this computer is established at system startup when the name is read from the registry. If this computer is a node in a cluster, the name of the node is returned.
        /// </remarks>
        /// <permission cref="EnvironmentPermission">
        /// For read access to the COMPUTERNAME environment variable. Associated enumeration: <see cref="EnvironmentPermissionAccess.Read"/>.
        /// </permission>
        string MachineName { get; }

        /// <summary>
        /// Gets the newline string defined for this environment.
        /// </summary>
        /// <value>
        /// A string containing "\r\n" for non-Unix platforms, or a string containing "\n" for Unix platforms.
        /// </value>
        /// <remarks>
        /// <para>The property value of NewLine is a constant customized specifically for the current platform
        ///   and implementation of the .NET Framework. For more information about the escape characters
        ///   in the property value, see Character Escapes in Regular Expressions.</para>
        /// <para>The functionality provided by NewLine is often what is meant by the terms newline,
        ///   line feed, line break, carriage return, CRLF, and end of line.</para>
        /// <para>NewLine can be used in conjunction with language-specific newline support such as the
        ///   escape characters '\r' and '\n' in Microsoft C# and C/C++, or vbCrLf in Microsoft Visual Basic.</para>
        /// <para>NewLine is automatically appended to text processed by the Console.WriteLine and StringBuilder.AppendLine methods.</para>
        /// </remarks>
        string NewLine { get; }

        /// <summary>
        /// Gets an <see cref="OperatingSystem"/> object that contains the current platform identifier and version number.
        /// </summary>
        /// <value>
        /// An object that contains the platform identifier and version number.
        /// </value>
        /// <exception cref="InvalidOperationException">
        /// This property was unable to obtain the system version.
        /// -or-
        /// The obtained platform identifier is not a member of <see cref="PlatformID"/>.
        /// </exception>
        /// <remarks>
        /// <para>Typically, the OSVersion property is used to ensure that an app is running on some base version
        ///   of an operating system in which a particular feature was introduced. When this is the case,
        ///   you should perform a version check by testing whether the current operating system version
        ///   returned by the OSVersion property is the same as, or greater than, the base operating system
        ///   version. For more information, see the Version class topic.</para>
        /// <para>The OSVersion property returns the version reported by the Windows GetVersionEx function.</para>
        /// <para>The OSVersion property reports the same version number (6.2.0.0) for both Windows 8 and Windows 8.1.</para>
        /// <para>In some cases, the OSVersion property may not return the operating system version that
        ///   matches the version specified for the Windows Program Compatibility mode feature.</para>
        /// </remarks>
        OperatingSystem OSVersion { get; }

        /// <summary>
        /// Gets the number of processors on the current machine.
        /// </summary>
        /// <value>
        /// The 32-bit signed integer that specifies the number of processors on the
        /// current machine. There is no default. If the current machine contains multiple
        /// processor groups, this property returns the number of logical processors that are
        /// available for use by the common language runtime (CLR).
        /// </value>
        /// <remarks>
        /// For more information about processor groups and logical processors, see Processor Groups.
        /// </remarks>
        int ProcessorCount { get; }

        /// <summary>
        /// Gets current stack trace information.
        /// </summary>
        /// <value>
        /// A string containing stack trace information. This value can be <see cref="String.Empty"/>.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">The requested stack trace information is out of range.</exception>
        string StackTrace { get; }

        /// <summary>
        /// Gets the fully qualified path of the system directory.
        /// </summary>
        /// <value>
        /// A string containing a directory path.
        /// </value>
        /// <remarks>
        /// An example of the value returned is the string "C:\WinNT\System32".
        /// </remarks>
        /// <permission cref="FileIOPermission ">
        /// For access to the information in the path itself. Associated enumeration: <see cref="FileIOPermissionAccess.PathDiscovery"/>.
        /// </permission>
        string SystemDirectory { get; }

        /// <summary>
        /// Gets the number of bytes in the operating system's memory page.
        /// </summary>
        /// <value>
        /// The number of bytes in the system memory page.
        /// </value>
        /// <remarks>
        /// <para>This information can be useful when determining whether to use the
        ///   <see cref="MemoryMappedFileOptions.DelayAllocatePages"/> option when you work with memory-mapped files.</para>
        /// <para>In Windows, this value is the dwPageSize member in the SYSTEM_INFO structure.</para>
        /// </remarks>
        /// <permission cref="EnvironmentPermission">
        /// for access to system and user environment variables. Associated exception: <see cref="SecurityException.PermissionState"/>.
        /// </permission>
        int SystemPageSize { get; }

        /// <summary>
        /// Gets the number of milliseconds elapsed since the system started.
        /// </summary>
        /// <value>
        /// A 32-bit signed integer containing the amount of time in milliseconds that has passed since the last time the computer was started.
        /// </value>
        /// <remarks>
        /// <para>The value of this property is derived from the system timer and is stored as a 32-bit signed integer.
        ///   Consequently, if the system runs continuously, TickCount will increment from zero to Int32.MaxValue for
        ///   approximately 24.9 days, then jump to Int32.MinValue, which is a negative number, then increment back
        ///   to zero during the next 24.9 days.</para>
        /// <para>TickCount is different from the Ticks property, which is the number of 100-nanosecond intervals that
        ///   have elapsed since 1/1/0001, 12:00am.</para>
        /// <para>Use the DateTime.Now property to obtain the current local date and time on this computer.</para>
        /// </remarks>
        int TickCount { get; }

        /// <summary>
        /// Gets the network domain name associated with the current user.
        /// </summary>
        /// <value>
        /// The network domain name associated with the current user.
        /// </value>
        /// <exception cref="PlatformNotSupportedException">The operating system does not support retrieving the network domain name.</exception>
        /// <exception cref="InvalidOperationException">The network domain name cannot be retrieved.</exception>
        /// <remarks>
        /// <para>The domain account credentials for a user are formatted as the user's domain name, the '\' character,
        ///   and user name. Use the UserDomainName property to obtain the user's domain name without the user name,
        ///   and the UserName property to obtain the user name without the domain name. For example, if a user's domain
        ///   name and user name are CORPORATENETWORK\john, the UserDomainName property returns "CORPORATENETWORK".</para>
        /// <para>The UserDomainName property first attempts to get the domain name component of the Windows account
        ///   name for the current user. If that attempt fails, this property attempts to get the domain name associated
        ///   with the user name provided by the UserName property. If that attempt fails because the host computer is not
        ///   joined to a domain, then the host computer name is returned.</para>
        /// </remarks>
        /// <permission cref="EnvironmentPermission">
        /// For read access to the USERDOMAIN environment variable. Associated enumeration: <see cref="EnvironmentPermissionAccess.Read"/>.
        /// </permission>
        string UserDomainName { get; }

        /// <summary>
        /// Gets a value indicating whether the current process is running in user interactive mode.
        /// </summary>
        /// <value>
        /// true if the current process is running in user interactive mode; otherwise, false.
        /// </value>
        /// <remarks>
        /// The UserInteractive property reports false for a Windows process or a service like IIS that runs
        /// without a user interface. If this property is false, do not display modal dialogs or message boxes
        /// because there is no graphical user interface for the user to interact with.
        /// </remarks>
        bool UserInteractive { get; }

        /// <summary>
        /// Gets the user name of the person who is currently logged on to the Windows operating system.
        /// </summary>
        /// <value>
        /// The user name of the person who is logged on to Windows.
        /// </value>
        /// <remarks>
        /// <para>You can use the UserName property to identify the user on the current thread, to the system
        ///   and application for security or access purposes. It can also be used to customize a particular
        ///   application for each user.</para>
        /// <para>The UserName property wraps a call to the Windows GetUserName function. The domain account
        ///   credentials for a user are formatted as the user's domain name, the '\' character, and user name.
        ///   Use the UserDomainName property to obtain the user's domain name and the UserName property to
        ///   obtain the user name.</para>
        /// <para>If an ASP.NET application runs in a development environment, the UserName property returns
        ///   the name of the current user. In a published ASP.NET application, this property returns the name
        ///   of the application pool account (such as Default AppPool).</para>
        /// </remarks>
        /// <permission cref="EnvironmentPermission">
        /// For read access to the USERNAME environment variable. Associated enumeration: <see cref="EnvironmentPermissionAccess.Read"/>.
        /// </permission>
        string UserName { get; }

        /// <summary>
        /// Gets a <see cref="Version"/> object that describes the major, minor, build, and revision numbers
        /// of the common language runtime.
        /// </summary>
        /// <value>
        /// An object that displays the version of the common language runtime.
        /// </value>
        /// <remarks>
        /// For more information about the version of the common language runtime that is installed with
        /// each version of the .NET Framework, see .NET Framework Versions and Dependencies.
        /// </remarks>
        IVersion Version { get; }
    }
}
