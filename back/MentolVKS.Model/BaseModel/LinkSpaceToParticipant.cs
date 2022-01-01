using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
	public class LinkSpaceToParticipant : TableBasedEntityBase
	{
		public int Id { get; set; }
		public int SpaceId { get; set; }
		public int? VksUserId { get; set; }
		public string CallLegProfileGuid { get; set; }
		public bool? CanDestroy { get; set; }
		public bool? CanAddRemoveMember { get; set; }
		public bool? CanChangeName { get; set; }
		public bool? CanChangeNonMemberAccessAllowed { get; set; }
		public bool? CanChangeUri { get; set; }
		public bool? CanChangeCallId { get; set; }
		public bool? CanChangePassCode { get; set; }
		public bool? CanRemoveSelf { get; set; }
		public Space Space { get; set; }
	}
}
