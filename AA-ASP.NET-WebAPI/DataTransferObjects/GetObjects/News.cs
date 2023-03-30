using System.ComponentModel.DataAnnotations;

namespace TheAzureArchiveAPI.DataTransferObjects.GetObjects
{
    public class News
    {
        [Key]
        public Guid IdNews { get; set; }

        [StringLength(250, ErrorMessage = "Source field may only contain a maximum of 250 characters")]
        public string Source { get; set; }

        [Required(ErrorMessage = "This field is mandatory")]
        [StringLength(250, ErrorMessage = "Title field may only contain a maximum of 250 characters")]
        public string Title { get; set; }

        [StringLength(250, ErrorMessage = "Author field may only contain a maximum of 250 characters")]
        public string Author { get; set; }

        [Required(ErrorMessage = "This field is mandatory")]
        public DateOnly PublicationDate { get; set; }

        [Required(ErrorMessage = "This field is mandatory")]
        [StringLength(500, ErrorMessage = "ImageUrl field may only contain a maximum of 500 characters")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "This field is mandatory")]
        public string Content { get; set; }
    }
}
