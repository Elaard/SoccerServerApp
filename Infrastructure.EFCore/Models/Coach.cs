namespace Infrastructure.EFCore.Models
{
    public class Coach
    {
        public int Id { get; set; }
        public int FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
    }
}
