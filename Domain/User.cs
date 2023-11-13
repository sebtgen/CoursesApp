using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public abstract class User
    {
        string email;
        string password;
        string phoneNumber;
        static int lastID = 0;
        int userID;

        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public static int LastID { get => lastID; set => lastID = value; }
        public int UserID { get => userID; set => userID = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }

        public User() { }
        public User(string password, string email, string phoneNumber)
        {
            this.password = password;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.userID = lastID;
            lastID++;
        }

        public void ValidateUser()
        {
            ValidateEmail();
            ValidateStrings();
        }

        private void ValidateEmail()
        {
            if (!string.IsNullOrEmpty(email))
            {
                if (!email.Contains("@"))
                {
                    throw new Exception("Email must contain @ character");
                }

                if (email.Contains('@'))
                {
                    if (email[0].Equals('@') || email[email.Length - 1].Equals('@'))
                    {
                        throw new Exception("Character @ cannot be at the start or end");
                    }
                }
            }
            else
            {
                throw new Exception("Email cant be empty");
            }
        }

        private void ValidateStrings()
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("Password cannot be empty");
            }

            if (string.IsNullOrEmpty(phoneNumber))
            {
                throw new Exception("Phone cannot be empty");
            }
        }

        public override string ToString()
        {
            return $"Email: {email}\n" +
                   $"Password: {password}\n" +
                   $"Phone number: {phoneNumber}\n" +
                   $"User ID: {userID}\n";
        }
    }
}
