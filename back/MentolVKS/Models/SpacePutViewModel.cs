using MentolVKS.Model.Validation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MentolVKS.Models
{
    public class SpacePutViewModel : IValidatableObject
    {
        [Required]
        public int Id { get; set; }
        public int? ServersGroupsId { get; set; }
        public string Name { get; set; }
        [Required]
        public string Uri { get; set; }
        public string TagCdr { get; set; }
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
        public List<LinkSpaceToParticipantView> LinkSpaceToParticipants { get; set; } = new List<LinkSpaceToParticipantView>();
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (Id < 1)
                errors.Add(new ValidationResult(">0", new[] { nameof(this.Id) }));
            if (ServersGroupsId == 0)
                errors.Add(new ValidationResult("null or >0", new[] { nameof(this.ServersGroupsId) }));
            if (SpaceGroupsId == 0)
                errors.Add(new ValidationResult("null or >0", new[] { nameof(this.SpaceGroupsId) }));
            if (Uri.ToLower() == UriAlt.ToLower())
            {
                errors.Add(new ValidationResult("URI и Альтернативный URI должны отличаться", new[] { nameof(this.Uri), nameof(this.UriAlt) }));
            }

            return errors;
        }
    }
}
