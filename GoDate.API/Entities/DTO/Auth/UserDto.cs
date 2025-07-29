﻿namespace GoDate.API.Entities.DTO.Auth
{
    public class UserDto
    {
        public required string UserName { get; set; }
        public required string Token { get; set; }
        public string? PhotoUrl { get; set; }               // Navbar avatar

    }
}
