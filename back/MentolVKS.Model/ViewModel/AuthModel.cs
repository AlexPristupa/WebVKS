using MentolVKS.Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MentolVKS.Model
{
    /// <summary>
    /// Авторизация пользователя
    /// </summary>
    public class AuthModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EnumDataType(typeof(Provider))]
        [JsonConverter(typeof(StringEnumConverter))]
        public Provider Provider { get; set; } = Provider.Integrated;
    }
}
