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
using System.Runtime.InteropServices;

namespace OpenNETCF.Location
{
    //typedef struct _GPS_DEVICE {
    //00 //   DWORD    dwVersion;
    //04 //   DWORD    dwSize;
    //08 //   DWORD    dwServiceState;
    //0C //   DWORD    dwDeviceState;
    //10 //   FILETIME ftLastDataReceived;
    //18 //   WCHAR    szGPSDriverPrefix[GPS_MAX_PREFIX_NAME];
    //38 //   WCHAR    szGPSMultiplexPrefix[GPS_MAX_PREFIX_NAME];
    //58 //   WCHAR    szGPSFriendlyName[GPS_MAX_FRIENDLY_NAME];
    //   GPS_DEVICE_STATUS    gdsDeviceStatus;
    //} *PGPS_DEVICE, GPS_DEVICE;
    [StructLayout(LayoutKind.Sequential)]
    internal class GpsidDevice : IGpsDevice
    {
        private const int GPS_MAX_PREFIX_NAME = 16;
        private const int GPS_MAX_FRIENDLY_NAME = 64;

        public GpsidDevice()
        {
            Buffer.BlockCopy(BitConverter.GetBytes(1), 0, m_buffer, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(m_buffer.Length), 0, m_buffer, 4, 4);
        }

        public static implicit operator byte[] (GpsidDevice d)
        {
            return d.m_buffer;
        }

        byte[] m_buffer = new byte[216];

        public GpsServiceState ServiceState
        {
            get { return (GpsServiceState)BitConverter.ToInt32(m_buffer, 0x08); }
        }

        public GpsServiceState DeviceState
        {
            get { return (GpsServiceState)BitConverter.ToInt32(m_buffer, 0x0C); }
        }

        public DateTime LastDataReceived
        {
            get { return DateTime.FromFileTime(BitConverter.ToInt64(m_buffer, 0x10)); }
        }

        public string FriendlyName
        {
            get
            {
                return Encoding.Unicode.GetString(m_buffer, 0x58, GPS_MAX_FRIENDLY_NAME * 2).TrimEnd('\0');
            }
        }

//        public StringBuilder szGPSDriverPrefix;
//        public StringBuilder szGPSMultiplexPrefix;
//        public StringBuilder szGPSFriendlyName;
//        public int gdsDeviceStatus;
    }
}
