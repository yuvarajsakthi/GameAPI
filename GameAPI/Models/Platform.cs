namespace GameAPI.Models
{
    public class Platform
    {
        public int PlatformId { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public ICollection<Game>? Games { get; set; }
    }
}
