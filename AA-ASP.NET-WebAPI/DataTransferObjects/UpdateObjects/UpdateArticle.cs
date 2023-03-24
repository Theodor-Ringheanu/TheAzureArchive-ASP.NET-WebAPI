﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TheAzureArchiveAPI.DataTransferObjects.UpdateObjects
{
    public class UpdateArticle
    {
        [Key]
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "This field is mandatory")]
        [StringLength(250, ErrorMessage = "Title field may only contain a maximum of 250 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "This field is mandatory")]
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
