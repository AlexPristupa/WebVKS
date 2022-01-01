using MentolVKS.Model.Bases;
using System;
using System.Collections.Generic;
using System.Text;

namespace MentolVKS.Model.BaseModel
{
    /// <summary>
    /// RefreshToken
    /// </summary>
    public class RefreshToken : TableBasedEntityBase
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// TODO
        /// </summary>
        public int AspNetUserId { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreateDt { get; set; }
        /// <summary>
        /// Дата окончания дата создания + время жизни из конфига
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Отпечаток устройства если есть
        /// </summary>
        public string FingerPrint { get; set; }
        /// <summary>
        /// IP адрес
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// Токен
        /// </summary>
        public string Token { get; set; }
    }
}
