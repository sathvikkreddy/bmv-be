namespace Backend.DTO
{
    public class ProviderDTO
    {
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow.AddHours(5).AddMinutes(30);
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
    }
}
