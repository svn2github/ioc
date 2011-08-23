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
using System.Threading;

namespace OpenNETCF.Location.Simulation
{
    internal class SimulatedGpsReceiver : IGpsReceiver
    {
        public event EventHandler<GenericEventArgs<IGpsDevice>> GPSStateChange;
        public event EventHandler<GenericEventArgs<PositionRecord>> PositionChange;
        public event EventHandler<GenericEventArgs<SatelliteRecord>> SatelliteChange;
        
        private Thread m_receiverThread;
        private bool m_shutdown;

        private bool IsRunning { get; set; }

        public void Initialize()
        {
            IsRunning = false;

            m_receiverThread = new Thread(SimulationProc);
            m_receiverThread.IsBackground = true;
            m_receiverThread.Start();
        }

        public void Start()
        {
            IsRunning = true;
        }

        public void Stop()
        {
            IsRunning = false;
        }

        public void Deinintialize()
        {
            m_shutdown = true;
        }

        private void SimulationProc()
        {
            m_shutdown = false;

            // for now simulate receiving data 1x per second
            while (!m_shutdown)
            {
                Thread.Sleep(1000);

                PositionRecord position = GetNextPosition();

                if (IsRunning)
                {
                    if (PositionChange != null)
                    {
                        PositionChange(this, new GenericEventArgs<PositionRecord>(position));
                    }
                }
            }
        }

        private double CurrentLatitude { get; set; }
        private double CurrentLongitude { get; set; }
        private float CurrentAltitude { get; set; }
        private float CurrentHeading { get; set; }
        private float CurrentSpeed { get; set; }
        private Random m_random = new Random(Environment.TickCount);

        private PositionRecord GetNextPosition()
        {
            // TODO: get this stuff from a simulation file
            CurrentSpeed += 1;
            CurrentLatitude += ((m_random.NextDouble() - 0.5) / 10);
            CurrentLongitude += ((m_random.NextDouble() - 0.5) / 10);
            CurrentHeading += (float)((m_random.NextDouble() - 0.5) / 5);
            CurrentAltitude = 100;

            PositionRecord record = new PositionRecord
            {
                Latitude = CurrentLatitude,
                Longitude = CurrentLongitude,
                Altitude = CurrentAltitude,
                Heading = CurrentHeading,
                Speed = CurrentSpeed,
                RecordDate = DateTime.Now
            };

            return record;
        }
    }
}
