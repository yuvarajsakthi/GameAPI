namespace GameAPI.Models
{
    public class GameCompany
    {
        public int GameCompanyId { get; set; }
        public string? Name { get; set; }
        public int FoundedYear { get; set; }
        public string? HeadQuarter { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public ICollection<Game>? Games { get; set; }
    }
}
