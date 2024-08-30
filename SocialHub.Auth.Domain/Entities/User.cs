﻿using SocialHub.Shared;

namespace SocialHub.Auth.Domain.Entities;

public class User : BaseEntity
{
    /// <summary>
    /// Почта пользователя 
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// Хэш пароля
    /// </summary>
    public string PasswordHash { get; set; }
    
    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; }
}