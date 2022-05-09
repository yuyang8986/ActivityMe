namespace ActivityMe.Groups.API.Models
{
    public class GroupMember
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public int RelevantExperience { get; set; }
    }
}
