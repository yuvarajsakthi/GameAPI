namespace GameAPI.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public string? Title { get; set; }
        public int GameCompanyId { get; set; }
        public int PublisherId { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public GameCompany? GameCompany { get; set; }
        public Publisher? Publisher { get; set; }
        public ICollection<Platform>? Platforms { get; set; }
        public GameDetail? GameDetail { get; set; }
    }
}
