namespace MentolVKS.Models
{
	/// <summary>
	/// Модель демонстрации информации об участнике бронирования
	/// </summary>
	public class VksUserPutViewModel
    {
		/// <summary>
		/// Имя участника
		/// </summary>
		public string VksUserName { get; set; } 

		/// <summary>
		/// Uri участника (в базе как Phone)
		/// </summary>
		public string Uri { get; set; }  

		/// <summary>
		/// Электронный адрес участника
		/// </summary>
		public string Email { get; set; } 
	}
}
