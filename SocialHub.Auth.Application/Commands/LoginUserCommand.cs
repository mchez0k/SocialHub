using Microsoft.EntityFrameworkCore;
using SocialHub.Auth.Application.Commands.Models;
using SocialHub.Auth.Domain.Entities;
using SocialHub.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SocialHub.Auth.Application.Commands
{
    public class LoginUserCommand
    {
        #region Fields

        //private readonly ILogger<> _logger;
        private readonly IRepository<User> usersRepository;

        #endregion

        #region Constructors

        public LoginUserCommand(IRepository<User> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        #endregion

        public void Execute(LoginUserModel model)
        {
            var existingUser = usersRepository.Get().Where(u => u.Name == model.Name).FirstOrDefault();
            if (existingUser == null) throw new Exception("Пользователя с таким именем не существует");

            // Проверяем пароль по хэшу
            string savedPasswordHash = existingUser.PasswordHash;
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(model.Password, salt, 100000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    throw new UnauthorizedAccessException();
        }
    }
}
