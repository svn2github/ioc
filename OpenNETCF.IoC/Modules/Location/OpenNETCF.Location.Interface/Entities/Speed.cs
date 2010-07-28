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
    public class Speed : IEquatable<Speed>
    {
        public Speed()
        {
            Knots = float.NaN;
        }

        public Speed(float knots)
        {
            Knots = knots;
        }

        public float Knots { get; set; }
        public float MilesPerHour
        {
            get { throw new NotImplementedException(); }
        }
        public float KilometersPerHour
        {
            get { throw new NotImplementedException(); }
        }

        public bool Equals(Speed other)
        {
            return Knots == other.Knots;
        }

        public static bool operator ==(Speed x, float y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(Speed x, float y)
        {
            return !(x == y);
        }

        public static bool operator ==(float x, Speed y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(float x, Speed y)
        {
            return !(x == y);
        }

        public static bool operator ==(Speed x, Speed y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(Speed x, Speed y)
        {
            return !(x == y);
        }

        public override bool Equals(object obj)
        {
            Speed other = obj as Speed;

            if (other == null) return false;

            return other.Knots == this.Knots;
        }

        public override int GetHashCode()
        {
            return Knots.GetHashCode();
        }
    }
}
