using System.ComponentModel.DataAnnotations;

namespace MentolVKS.Models
{
    /// <summary>
    /// Модель данных для PUT-запросов.
    /// </summary>
    public class BookingPutViewModel : BookingPostViewModel
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [Required(ErrorMessage = "Поле Id должно быть заполнено")]
        public int? Id { get; set; }
    }
}
