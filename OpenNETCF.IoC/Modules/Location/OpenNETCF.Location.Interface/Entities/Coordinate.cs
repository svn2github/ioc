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

namespace OpenNETCF.Location
{
    public class Coordinate : IEquatable<Coordinate>
    {
        public DateTime RecordDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public bool Equals(Coordinate other)
        {
            if (Latitude != other.Latitude) return false;
            if (Longitude != other.Longitude) return false;

            return true;
        }

        public static bool operator ==(Coordinate x, Coordinate y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(Coordinate x, Coordinate y)
        {
            return !(x == y);
        }

        public override int GetHashCode()
        {
            return Latitude.GetHashCode() | Longitude.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Coordinate coord = obj as Coordinate;
            if(coord == null) return false;
            return this.Equals(coord);
        }
    }
}
