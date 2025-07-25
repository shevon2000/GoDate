﻿using System.ComponentModel.DataAnnotations.Schema;

namespace GoDate.API.Entities.Domain
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }
        public required string Url { get; set; }
        public bool IsMain { get; set; }
        public string? PublicId { get; set; }
        public int UserId { get; set; }

        // Navigation properties
        public User User { get; set; } = null!;
    }
}