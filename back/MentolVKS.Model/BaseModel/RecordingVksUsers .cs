using MentolVKS.Model.Bases;
using System;

namespace MentolVKS.Model.BaseModel
{
    public class RecordingVksUsers : TableBasedEntityBase
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Идентификатор записи
		/// </summary>
		public int RecordingId { get; set; }

		/// <summary>
		/// Идентификатор пользователя
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// Дата записи
		/// </summary>
		public DateTime DateRecord { get; set; }

		/// <summary>
		/// Признак просмотра
		/// </summary>
		public bool IsPlay { get; set; }

		/// <summary>
		/// Признак загрузки
		/// </summary>
		public bool IsDownload { get; set; }

		/// <summary>
		/// Описание
		/// </summary>
		public string Description { get; set; }

		public Recording Recording { get; set; }
		public AspNetUser User { get; set; }
	}
}
