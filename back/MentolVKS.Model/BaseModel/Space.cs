using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
    /// <summary>
    /// Комнаты
    /// </summary>
    public class Space: TableBasedEntityBase
    {
        public int Id { get; set; }
        public int? ServersGroupsId { get; set; }
        public string Name { get; set; }
        public string Uri { get; set; }
        public string TagCdr {get;set;}
	    public string Guid { get; set; }
	    public string Password { get; set; }
	    public string UriAlt { get; set; }
	    public string PasswordGuest { get; set; }
	    public string UriVideo { get; set; }
	    public bool? IsGuestAccessible { get; set; }
        public bool? IsAvailableForBooking { get; set; }
        public int? SpaceGroupsId { get; set; }
        public string CallId { get; set; }
        public string CallLegProfileGuid { get; set; }
        public string CallBrandingProfileGuid { get; set; }
        public int? OwnerId { get; set; }
        public ICollection<LinkSpaceToParticipant> LinkSpaceToParticipants { get; set; } = new HashSet<LinkSpaceToParticipant>();
    }
}
