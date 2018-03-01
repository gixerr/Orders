using System;
using System.Net.Mail;
using Orders.Core.Exceptions;

namespace Orders.Core.Domain
{
    public class User : Entity
    {
        protected User() { }
        public User(string name, string email, string password)
        {
            this.Name = Validate(name);
            this.Email = ValidateEmail(email);
            this.Password = password;
            this.Role = Role.User;
            this.CreatedAt = DateTime.UtcNow;
        }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public Role Role { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

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
    }
}