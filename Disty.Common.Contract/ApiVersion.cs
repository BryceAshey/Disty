using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disty.Common.Contract
{
    public class ApiVersion : IComparable
    {
        public const string ReleaseDateTimeFormat = "yyyy-MM-dd";

        public int Version { get; private set; }
        public DateTime Release { get; private set; }

        public ApiVersion(int version, DateTime release)
        {
            Version = version;
            Release = release;
        }

        public int CompareTo(object obj)
        {
            var other = obj as ApiVersion;
            if (other == null)
            {
                throw new ArgumentException("Only able to compare ApiVersion against another ApiVersion");
            }

            if (Version.Equals(other.Version))
            {
                return Release.CompareTo(other.Release);
            }

            return Version.CompareTo(other.Version);
        }

        public static bool operator >(ApiVersion a, ApiVersion b)
        {
            return a.CompareTo(b) > 0;
        }

        public static bool operator <(ApiVersion a, ApiVersion b)
        {
            return a.CompareTo(b) < 0;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return ToString().Equals(obj.ToString());
        }

        public override int GetHashCode()
        {
            return Tuple.Create(Version, Release).GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0};{1}", Version, Release.ToString(ReleaseDateTimeFormat));
        }

        public static explicit operator ApiVersion(string source)
        {
            var split = source.Split(';');
            if (split.Length != 2)
            {
                throw new ArgumentException("Unable to locate ';' in '{0}', expecting for example '2;2014-01-01'", source);
            }

            int version;
            if (!int.TryParse(split[0], out version))
            {
                throw new ArgumentException("Unable to parse version as integer in '{0}', expecting for example '2;2014-01-01'", source);
            }

            DateTime release;
            if (!DateTime.TryParseExact(split[1], ReleaseDateTimeFormat, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out release))
            {
                throw new ArgumentException("Unable to parse release as date in '{0}', expecting for example '2;2014-01-01'", source);
            }

            return new ApiVersion(version, release);
        }
    }
}
