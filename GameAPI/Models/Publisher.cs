namespace GameAPI.Models
{
    public class Publisher
    {
        public int PublisherId { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public ICollection<Game>? Games { get; set; }
    }
}
