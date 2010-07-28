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
using System.Runtime.InteropServices;
using OpenNETCF.Threading;

namespace OpenNETCF.Location
{
    internal class GPSIDReceiver : IGpsReceiver
    {
        public event EventHandler<GenericEventArgs<IGpsDevice>> GPSStateChange;
        public event EventHandler<GenericEventArgs<PositionRecord>> PositionChange;
        public event EventHandler<GenericEventArgs<SatelliteRecord>> SatelliteChange;

        private EventWaitHandle m_stateChangeEvent;
        private EventWaitHandle m_newLocationEvent;
        private EventWaitHandle m_stopEvent;

        private IntPtr m_gpsServiceHandle;

        public GPSIDReceiver()
        {
        }

        ~GPSIDReceiver()
        {
            Deinintialize();
        }

        public void Initialize()
        {
            m_stateChangeEvent = new EventWaitHandle(false, EventResetMode.AutoReset);
            m_newLocationEvent = new EventWaitHandle(false, EventResetMode.AutoReset);
            m_stopEvent = new EventWaitHandle(false, EventResetMode.AutoReset);
        }

        public void Start()
        {
            if (m_gpsServiceHandle != IntPtr.Zero) return;

            m_gpsServiceHandle = GPSOpenDevice(m_newLocationEvent.Handle, m_stateChangeEvent.Handle, null, 0);

            System.Threading.Thread t = new System.Threading.Thread(EventThreadProc);
            t.IsBackground = true;
            t.Start();
        }

        private enum Events
        {
            Stop = 0,
            StateChanged = 1,
            NewLocation = 2
        }

        private void EventThreadProc()
        {
            EventWaitHandle[] eventHandles = new EventWaitHandle[3];
            eventHandles[(int)Events.Stop] = m_stopEvent;
            eventHandles[(int)Events.StateChanged] = m_stateChangeEvent;
            eventHandles[(int)Events.NewLocation] = m_newLocationEvent;

            while (true)
            {
                Events setEvent = (Events)EventWaitHandle.WaitAny(eventHandles, 1000, false);

                switch (setEvent)
                {
                    case Events.Stop:
                        return;
                    case Events.StateChanged:
                        if (GPSStateChange != null)
                        {
                            GpsidDevice device = new GpsidDevice();
                            int i = GPSGetDeviceState(device);

                            GPSStateChange(this, new GenericEventArgs<IGpsDevice>(device));
                        }
                        break;
                    case Events.NewLocation:
                        GPS_POSITION position = new GPS_POSITION();
                        int i2 = GPSGetPosition(m_gpsServiceHandle, position, 500000, 0);

                        if (PositionChange != null)
                        {
                            PositionRecord p = new PositionRecord();

                            p.RecordDate = (position.TimeValid) ? position.Time : DateTime.MinValue;
                            if (position.LatitudeValid && position.LongitudeValid)
                            {
                                p.Latitude = position.Latitude;
                                p.Longitude = position.Longitude;
                            }
                            p.Altitude = (position.SeaLevelAltitudeValid) ? position.SeaLevelAltitude : float.NaN;
                            p.Speed = position.SpeedValid ? position.Speed : float.NaN;
                            p.Heading = position.HeadingValid ? position.Heading : float.NaN;

                            PositionChange(this, new GenericEventArgs<PositionRecord>(p));
                        }
                        if(SatelliteChange != null)
                        {
                            SatelliteRecord si = new SatelliteRecord { SatellitesInView = -1, TotalSatellites = -1 };
                            si.RecordDate = (position.TimeValid) ? position.Time : DateTime.MinValue;
                            if (position.SatelliteCountValid) si.TotalSatellites = position.SatelliteCount;
                            if (position.SatellitesInViewValid) si.SatellitesInView = position.SatellitesInViewCount;
                            if (position.SatellitesInSolutionValid) si.SatellitesInSolution = position.GetSatellitesInSolution();
                            
                            SatelliteChange(this, new GenericEventArgs<SatelliteRecord>(si));
                        }
                        break;
                }
            }
        }

        public void Stop()
        {
            if (m_gpsServiceHandle == IntPtr.Zero) return;

            GPSCloseDevice(m_gpsServiceHandle);
            m_gpsServiceHandle = IntPtr.Zero;
            m_stopEvent.Set();
        }

        public void Deinintialize()
        {
            Stop();

            if (m_stateChangeEvent != null)
            {
                m_stateChangeEvent.Close();
                m_stateChangeEvent = null;
            }
            if (m_newLocationEvent != null)
            {
                m_newLocationEvent.Close();
                m_newLocationEvent = null;
            }
        }

        [DllImport("gpsapi.dll")]
        static extern IntPtr GPSOpenDevice(IntPtr hNewLocationData, IntPtr hDeviceStateChange, string szDeviceName, int dwFlags);

        [DllImport("gpsapi.dll")]
        static extern int GPSCloseDevice(IntPtr hGPSDevice);

        [DllImport("gpsapi.dll")]
        static extern int GPSGetPosition(IntPtr hGPSDevice, GPS_POSITION position, int dwMaximumAge, int dwFlags);

//        [DllImport("gpsapi.dll")]
//        static extern int GPSGetDeviceState(GPS_DEVICE device);

        [DllImport("gpsapi.dll")]
        static extern int GPSGetDeviceState(byte[] device);
    }
}
