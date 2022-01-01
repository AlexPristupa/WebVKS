using MentolVKS.Model.Bases;

namespace MentolVKS.Model.BaseModel
{
	/// <summary>
	/// Модель сервера NFS
	/// </summary>
	public class NfsServers : TableBasedEntityBase
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		public int Id { get; set; } 

		/// <summary>
		/// ip-адрес сервера
		/// </summary>
		public string Ip { get; set; } 

		/// <summary>
		/// Точка монтирования сервера
		/// </summary>
		public string Mount { get; set; } 
	}

}
