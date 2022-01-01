using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
	public class TimeZone : TableBasedEntityBase
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string PrivateName { get; set; }
		public int OffsetMinute { get; set; }
		public string StandartId { get; set; }
	}
}
