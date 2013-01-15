// LICENSE
// -------
// This software was originally authored by Christopher Tacke of OpenNETCF Consulting, LLC
// On March 10, 2009 is was placed in the public domain, meaning that all copyright has been disclaimed.
//
// You may use this code for any purpose, commercial or non-commercial, free or proprietary with no legal 
// obligation to acknowledge the use, copying or modification of the source.
//
// OpenNETCF will maintain an "official" version of this software at www.opennetcf.com and public 
// submissions of changes, fixes or updates are welcomed but not required
//

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.IoC;
using OpenNETCF;

namespace OpenNETCF.Location
{
    public interface IGpsService
    {
        event EventHandler GpsStarted;
        event EventHandler GpsStopped;
        event EventHandler<GenericEventArgs<float>> AltitudeChanged;
        event EventHandler<GenericEventArgs<float>> HeadingChanged;
        event EventHandler<GenericEventArgs<Speed>> SpeedChanged;
        event EventHandler<GenericEventArgs<Coordinate>> PositionChanged;
        event EventHandler<GenericEventArgs<int>> VisibleSatelliteCountChanged;

        void StartReceiver();
        void StopReceiver();
        DateTime GetSatelliteTime();

        GpsServiceState ServiceState { get; }
        string ReceiverName { get; }
        float LastAltitude { get; }
        float LastHeading { get; }
        Speed LastSpeed { get; }
        Coordinate LastCoordinate { get; }
        int LastVisibleSatelliteCount { get; }
        Satellite[] SatellitesInCurrentSolution { get; }
    }
}
