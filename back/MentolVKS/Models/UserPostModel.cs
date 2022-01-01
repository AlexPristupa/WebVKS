using MentolVKS.Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace MentolVKS.Models
{
    public class UserPostModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Post { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [EnumDataType(typeof(Provider))]
        [JsonConverter(typeof(StringEnumConverter))]
        public Provider Provider { get; set; } = Provider.Integrated;
    }
}
