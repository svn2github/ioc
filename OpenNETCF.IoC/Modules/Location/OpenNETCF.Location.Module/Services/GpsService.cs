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
using OpenNETCF.Location;
using OpenNETCF.IoC;
using System.Threading;
using OpenNETCF.Location.Simulation;

namespace OpenNETCF.Location
{
    internal class GpsService : IGpsService
    {
        public event EventHandler GpsStarted;
        public event EventHandler GpsStopped;
        public event EventHandler<GenericEventArgs<float>> AltitudeChanged;
        public event EventHandler<GenericEventArgs<float>> HeadingChanged;
        public event EventHandler<GenericEventArgs<Speed>> SpeedChanged;
        public event EventHandler<GenericEventArgs<Coordinate>> PositionChanged;
        public event EventHandler<GenericEventArgs<int>> VisibleSatelliteCountChanged;

        private IGpsReceiver m_gps;
        private EventWaitHandle m_timeWaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
        private DateTime m_satelliteTime;
        private DateTime m_lastDataReceived;

        public float LastAltitude { get; private set; }
        public float LastHeading { get; private set; }
        public Speed LastSpeed { get; private set; }
        public Coordinate LastCoordinate { get; private set; }
        public int LastVisibleSatelliteCount { get; private set; }

        public GpsService(bool useSimulation)
        {
            if (useSimulation)
            {
                m_gps = new SimulatedGpsReceiver();
            }
            else
            {
                m_gps = new GPSIDReceiver();
            }

            m_gps.Initialize();

            LastSpeed = new Speed();
            LastCoordinate = new Coordinate();
            LastAltitude = float.NaN;
            LastHeading = float.NaN;

            m_gps.GPSStateChange += new EventHandler<GenericEventArgs<IGpsDevice>>(m_gps_GPSStateChange);
            m_gps.PositionChange += new EventHandler<GenericEventArgs<PositionRecord>>(m_gps_PositionChange);
            m_gps.SatelliteChange += new EventHandler<GenericEventArgs<SatelliteRecord>>(m_gps_SatelliteChange);
        }

        public Satellite[] SatellitesInCurrentSolution { get; private set; }

        void m_gps_SatelliteChange(object sender, GenericEventArgs<SatelliteRecord> e)
        {
            SatellitesInCurrentSolution = e.Value.SatellitesInSolution;

            if (e.Value.SatellitesInView != LastVisibleSatelliteCount)
            {
                LastVisibleSatelliteCount = e.Value.SatellitesInView;
                if (VisibleSatelliteCountChanged != null)
                {
                    VisibleSatelliteCountChanged(this, new GenericEventArgs<int>(LastVisibleSatelliteCount));
                }
            }
        }

        void m_gps_PositionChange(object sender, GenericEventArgs<PositionRecord> e)
        {
            // let any call to GetSatelliteTime unblock
            if (e.Value.RecordDate != DateTime.MinValue)
            {
                m_satelliteTime = e.Value.RecordDate;
                m_timeWaitHandle.Set();
            }

            if (float.IsNaN(e.Value.Altitude) && (e.Value.Altitude != LastAltitude))
            {
                LastAltitude = e.Value.Altitude;
                if (AltitudeChanged != null)
                {
                    AltitudeChanged(this, new GenericEventArgs<float>(LastAltitude));
                }
            }

            if ((!float.IsNaN(e.Value.Speed)) && (e.Value.Speed != LastSpeed.Knots))
            {
                LastSpeed.Knots = e.Value.Speed;
                if (SpeedChanged != null)
                {
                    SpeedChanged(this, new GenericEventArgs<Speed>(LastSpeed));
                }
            }

            if (!float.IsNaN(e.Value.Heading) && (e.Value.Heading != LastHeading))
            {
                LastHeading = e.Value.Heading;
                if (HeadingChanged != null)
                {
                    HeadingChanged(this, new GenericEventArgs<float>(LastHeading));
                }
            }

            Coordinate c = new Coordinate { RecordDate = e.Value.RecordDate, Latitude = e.Value.Latitude, Longitude = e.Value.Longitude };
            if (c != LastCoordinate)
            {
                LastCoordinate = c;

                if (PositionChanged != null)
                {
                    PositionChanged(this, new GenericEventArgs<Coordinate>(LastCoordinate));
                }
            }
        }

        public DateTime GetSatelliteTime()
        {
            m_timeWaitHandle.Reset();
            if (!m_timeWaitHandle.WaitOne(1000, false))
            {
                return DateTime.MinValue;
            }
            return m_satelliteTime;
        }

        public void StartReceiver()
        {
            m_gps.Start();
        }

        public void StopReceiver()
        {
            m_gps.Stop();
        }

        void m_gps_GPSStateChange(object sender, GenericEventArgs<IGpsDevice> e)
        {
            ServiceState = e.Value.ServiceState;
            m_lastDataReceived = e.Value.LastDataReceived;

            switch (ServiceState)
            {
                case GpsServiceState.On:
                    ReceiverName = e.Value.FriendlyName;

                    if (GpsStarted != null) GpsStarted(this, null);
                    break;
                case GpsServiceState.Off:
                    if (GpsStopped != null) GpsStopped(this, null);
                    break;
            }
        }

        public GpsServiceState ServiceState { get; private set; }
        public string ReceiverName { get; private set; }

        ~GpsService()
        {
            m_gps.Deinintialize();
        }
    }
}
