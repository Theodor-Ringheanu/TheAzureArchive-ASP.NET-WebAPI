﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TheAzureArchiveAPI.DataTransferObjects.PatchObjects
{
    public class PatchNews
    {
        [Key]
        [JsonIgnore]
        public Guid IdNews { get; set; }

        [StringLength(250, ErrorMessage = "Source field may only contain a maximum of 250 characters")]
        public string? Source { get; set; }

        [StringLength(250, ErrorMessage = "Title field may only contain a maximum of 250 characters")]
        public string? Title { get; set; }

        [StringLength(250, ErrorMessage = "Author field may only contain a maximum of 250 characters")]
        public string? Author { get; set; }

        public DateOnly? PublicationDate { get; set; }


        [StringLength(500, ErrorMessage = "ImageUrl field may only contain a maximum of 500 characters")]
        public string? ImageUrl { get; set; }

        public string? Content { get; set; }
    }
}
