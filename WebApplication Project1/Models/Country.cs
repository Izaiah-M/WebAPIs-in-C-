namespace WebApplication_Project1.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }

        // Don't need to do a migration for this one, its just going to tell the class that if you are getting info on the Country
        // Include associated Hotels
        // Thats why we make it a virtual IList
        public virtual IList<Hotel>? Hotels { get; set; }
    }
}
