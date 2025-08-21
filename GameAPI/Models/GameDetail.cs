namespace GameAPI.Models
{
    public class GameDetail
    {
        public int GameDetailId { get; set; }
        public int GameId { get; set; }
        public string? Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Description { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public Game? Game { get; set; }
    }
}
