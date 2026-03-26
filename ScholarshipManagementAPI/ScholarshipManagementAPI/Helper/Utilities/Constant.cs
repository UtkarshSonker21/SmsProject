using System.Security.Cryptography;

namespace ScholarshipManagementAPI.Helper.Utilities
{
    public static class Constant
    {
        public static class ValidationRegex
        {
            // ✔ Email Validation
            // Supports: name@example.com, name.surname@mail.co.uk, numbers, dots, hyphens
            public const string Email =
                @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // ✔ Strong Password Validation
            // Min 8 chars
            // At least 1 uppercase
            // At least 1 lowercase
            // At least 1 number
            // At least 1 special character
            public const string StrongPassword =
                @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&#^()_\-+=])[A-Za-z\d@$!%*?&#^()_\-+=]{8,}$";

            // ✔ Medium Password (optional)
            // Min 6 chars, letters + numbers
            public const string MediumPassword =
                @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$";
        }



        public static class HelperMethods
        {
            // Generates a secure random password. Default length is 12 characters.
            public static string GeneratePassword(int length = 12)
            {
                // All characters that are allowed in the password
                const string allowedCharacters =
                    "ABCDEFGHJKLMNOPQRSTUVWXYZ" +
                    "abcdefghijklmnopqrstuvwxyz" +
                    "0123456789" +
                    "@#$!%*?";

                // Step 1:
                // Create an array to hold random values.
                // Each element will later become ONE character in the password.
                var randomBytes = new byte[length];
                // Example before filling:
                // randomBytes = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]

                // Step 2:
                // Create a cryptographically secure random number generator
                using var rng = RandomNumberGenerator.Create();

                // Step 3:
                // Fill the byte array with random values (0–255)
                rng.GetBytes(randomBytes);
                // Example after filling:
                // randomBytes = [193, 12, 88, 7, 241, 64, 19, 200, 33, 91, 5, 174]

                // Step 4:
                // Convert each random byte into a valid character
                //  - b % allowedCharacters.Length ensures the index is in range
                //  - allowedCharacters[index] gives us one character
                var passwordCharacters = randomBytes
                    .Select(b => allowedCharacters[b % allowedCharacters.Length])
                    .ToArray();

                // Step 5:
                // Convert the character array into a string and return it
                return new string(passwordCharacters);
            }


            //public static string HashPassword(string password)
            //{
            //    return BCrypt.Net.BCrypt.HashPassword(password);
            //}

            //public static bool VerifyPassword(string password, string hash)
            //{
            //    return BCrypt.Net.BCrypt.Verify(password, hash);
            //}


            // Generates a username using StaffType prefix and LoginId (IDENTITY).
            public static string GenerateUsername(long staffType, long loginId)
            {
                string prefix = staffType switch
                {
                    1 => "SA",    // SuperAdmin
                    2 => "NGO",   // NGO
                    3 => "SC",    // School
                    4 => "UN",    // University
                    _ => "USR"
                };

                // D6 = pad with zeros (000128)
                return $"{prefix}_{loginId:D6}";
            }

        }



    }
}
