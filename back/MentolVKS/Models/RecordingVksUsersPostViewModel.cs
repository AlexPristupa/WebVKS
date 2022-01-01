using System;

namespace MentolVKS.Models
{
    /// <summary>
    /// Модель для POST-запросов
    /// </summary>
    public class RecordingVksUsersPostViewModel
    {
        public int RecordingId { get; set; }
        public int UserId { get; set; }
        public DateTime DateRecord { get; set; }
        public bool IsPlay { get; set; }
        public bool IsDownload { get; set; }
        public string Description { get; set; }
    }
}
