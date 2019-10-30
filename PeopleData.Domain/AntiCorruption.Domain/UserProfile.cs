namespace PeopleInformation.Domain.AntiCorruption.Domain
{
    public class UserProfile
    {
        public UserProfile()
        {
            var Addresses = new Address();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string DateOfBirth { get; set; }

    }
}