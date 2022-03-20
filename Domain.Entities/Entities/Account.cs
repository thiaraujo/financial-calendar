using Domain.Entities.Shared;
using Domain.Entities.Validators;
using Domain.Entities.ValueObjects;
using Middleware.Extensions;

namespace Domain.Entities.Entities
{
    public class Account : BaseEntity
    {
        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public string Picture { get; private set; }
        public string Password { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        public bool IsActive { get; private set; }

        public Account(Name name, Email email, string picture, string password)
        {
            Name = name;
            Email = email;
            Picture = picture;
            Password = password;
            RegistrationDate = DateTime.Now; // Normal use
            IsActive = true; // Normal use

            Validate(this, new AccountValidator());
        }

        // Change Name
        public void ChangeName(Name name)
        {
            Name = name;
        }

        // Change the email
        public void ChangeEmail(Email email)
        {
            Email = email;
        }

        public void ChangePicture(string picture)
        {
            Picture = picture;
        }

        // Change the registration date
        public void ChangeRegistrationDate(DateTime newDateRegistration)
        {
            RegistrationDate = newDateRegistration;
        }

        // Change the account to Inactive
        public void ChangeToInactive()
        {
            IsActive = false;
        }

        // Change the account to Active
        public void ChangeToActive()
        {
            IsActive = true;
        }

        public void EncryptPassword()
        {
            Password = Password.EncryptToSha512();
        }

        // Change the password
        public void ChangePassword(string newPassword)
        {
            if (newPassword.Length <= 6)
            {
                AddNotification("PasswordValidator", "A senha deve ter no minímo 6 caracteres");
                return;
            }

            Password = newPassword.EncryptToSha512();
        }
    }
}