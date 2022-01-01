using MentolVKS.Model.Bases;
using System;

namespace MentolVKS.Model.BaseModel
{
	/// <summary>
	/// Модель участников бронирования
	/// </summary>
	public class VksUser : TableBasedEntityBase
	{
		public int Id { get; set; } 
		public Guid? GUID { get; set; } 
		public string JID { get; set; } 
		public string Name { get; set; } 
		public string Phone { get; set; } 
		public string Org { get; set; } 
		public string UserFunction { get; set; }
		public string Email { get; set; } 
		public bool? UserActive { get; set; }
		public bool? IsDeleted { get; set; } 
		public DateTime? DateLastRecord { get; set; } 
		public Guid? ProfileGUID { get; set; } 
		public bool? HasLicense { get; set; } 
	}
}
