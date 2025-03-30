// -----------------------------------------------------------------------
// <copyright file="LoginResponse.cs" company="Kvesic, Matkovic, FSRE">
// Copyright (c) Kvesic, Matkovic, FSRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace ColetorA41.Models
{
    /// <summary>
    /// Represents the response from a login attempt, including an authentication token and user ID.
    /// </summary>
    /// 
    public class LoginRequest
    {
        /// <summary>
        /// Gets or sets the username for login.
        /// </summary>
        [JsonPropertyName("username")]
        public string? Username { get; set; }

        /// <summary>
        /// Gets or sets the password for login.
        /// </summary>
        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }

    public class LoginResponse
    {
        /// <summary>
        /// Gets or sets the authentication token for the user.
        /// </summary>
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
    }
}