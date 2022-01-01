using MentolVKS.Model.Bases;

namespace MentolVKS.Model.BaseModel
{
	/// <summary>
	/// Бронирование - Комнаты
	/// </summary>
	public class SpaceUserRightsView : TableBasedEntityBase
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Участник
		/// </summary>
		public string VksUser { get; set; }

		/// <summary>
		/// Профайл настроек вызова
		/// </summary>
		public string CallLegProfileGuid { get; set; }

		/// <summary>
		/// Удаление комнаты
		/// </summary>
		public bool? CanDestroy { get; set; }

		/// <summary>
		/// Добавление/удаление участников
		/// </summary>
		public bool? CanAddRemoveMember { get; set; }

		/// <summary>
		/// Изменение имени комнаты
		/// </summary>
		public bool? CanChangeName { get; set; }

		/// <summary>
		/// Может редактировать доступ к комнате
		/// </summary>
		public bool? CanChangeNonMemberAccessAllowed { get; set; }

		/// <summary>
		/// Изменение URI
		/// </summary>
		public bool? CanChangeUri { get; set; }

		/// <summary>
		/// Изменение идентификатора конференции
		/// </summary>
		public bool? CanChangeCallId { get; set; }

		/// <summary>
		/// Изменение пароля конференции
		/// </summary>
		public bool? CanChangePassCode { get; set; }

		/// <summary>
		/// Удаление себя (из участников)
		/// </summary>
		public bool? CanRemoveSelf { get; set; }
	}
}
