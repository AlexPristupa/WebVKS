using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
	public class VksLicensing : TableBasedEntityBase
	{
		public int Idr { get; set; } // int
		public decimal? PersonalLicenseLimit { get; set; } // numeric(18, 0)
		public decimal? SharedLicenseLimit { get; set; } // numeric(18, 0)
		public decimal? CapacityUnitLimit { get; set; } // numeric(18, 0)
		public decimal? Users { get; set; } // numeric(18, 0)
		public decimal? PersonalLicenses { get; set; } // numeric(18, 0)
		public decimal? ParticipantsActive { get; set; } // numeric(18, 0)
		public decimal? CallsActive { get; set; } // numeric(18, 0)
		public decimal? WeightedCallsActive { get; set; } // numeric(6, 3)
		public decimal? CallsWithoutPersonalLicense { get; set; } // numeric(18, 0)
		public decimal? WeightedCallsWithoutPersonalLicense { get; set; } // numeric(6, 3)
		public decimal? CapacityUnitUsage { get; set; } // numeric(6, 3)
		public decimal? CapacityUnitUsageWithoutPersonalLicense { get; set; } // numeric(6, 3)
		public DateTime DateLastRecord { get; set; } // datetime
	}
}
