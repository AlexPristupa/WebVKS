using MentolVKS.Model.Bases;

namespace MentolVKS.Model.BaseModel
{
    /// <summary>
    /// Внешние участники
    /// </summary>
    public class VksUsersOther : TableBasedEntityBase
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Uri
        /// </summary>
        public string Uri { get; set; }
        /// <summary>
        ///Электронный адрес
        /// </summary>
        public string Email { get; set; }
    }
}
