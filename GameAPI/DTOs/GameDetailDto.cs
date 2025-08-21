namespace GameAPI.DTOs
{
    public class GameDetailDto
    {
        public int GameId { get; set; }
        public string? Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Description { get; set; }
    }
}
