using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
    public class PinPolitics : TableBasedEntityBase
	{
		public int Id { get; set; } // int
		public string Name { get; set; }
		public string PrivateName { get; set; }
	}
}
