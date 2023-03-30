using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TheAzureArchiveAPI.DataTransferObjects.UpdateObjects
{
    public class UpdateEmailSubscribed
    {
        [Key]
        [JsonIgnore]
        public Guid IdEmail { get; set; }

        [Required(ErrorMessage = "This field is mandatory")]
        [StringLength(50, ErrorMessage = "Title field may only contain a maximum of 50 characters")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "This field is mandatory")]
        public bool? IsSubscribed { get; set; }

        [Required(ErrorMessage = "This field is mandatory")]
        public DateTime? DateAdded { get; set; }
    }
}
