using System;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using Orders.Core.Exceptions;

namespace Orders.Core.Domain
{
    public class User : Entity
    {
        protected User() { }
        public User(Guid id, string name, string email, Role role = Role.User)
        {
            this.Id = id;
            this.Name = Validate(name);
            this.Email = ValidateEmail(email);
            this.Role = role;
            this.CreatedAt = DateTime.UtcNow;
        }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string HashedPassword { get; protected set; }
        public string Salt { get; protected set; }
        public Role Role { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        public bool PasswordIsInvalid(string password, IPasswordHasher<User> passwordHasher)
            => passwordHasher.VerifyHashedPassword(this, HashedPassword, password) == PasswordVerificationResult.Failed;
        private string ValidateEmail(string email)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);
                
                return email;
            }
            catch (FormatException ex)
            {
                throw new OrdersException(ErrorCode.invalid_email, ex.Message);
            }
        }

        public void SetPassword(string password, IPasswordHasher<User> passwordHasher)
        {
            if(string.IsNullOrWhiteSpace(password))
            {
                throw new OrdersException(ErrorCode.invalid_password, "Password cannot be empty.");
            }
            this.HashedPassword = passwordHasher.HashPassword(this, password);
        }
    }
}