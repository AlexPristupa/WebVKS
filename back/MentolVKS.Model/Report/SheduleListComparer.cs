using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MentolVKS.Model.Report
{
    public class SheduleListComparer : IEqualityComparer<SheduleList>
    {
        public bool Equals([AllowNull] SheduleList x, [AllowNull] SheduleList y)
        {
            if (x.EndTime >= y.StartTime && x.StartTime <= y.EndTime) return true;
            return false;
        }

        public int GetHashCode([DisallowNull] SheduleList obj)
        {
            return 0;
        }
    }
}
