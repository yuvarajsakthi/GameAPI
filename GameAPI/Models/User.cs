namespace GameAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }  // e.g. "Admin", "Publisher", "Viewer"

        public ICollection<Game> Games { get; set; } = [];
        public ICollection<GameCompany> GameCompanies { get; set; } = [];
        public ICollection<Publisher> Publishers { get; set; } = [];
        public ICollection<Platform> Platforms { get; set; } = [];
        public ICollection<GameDetail> GameDetails { get; set; } = [];
    }
}
