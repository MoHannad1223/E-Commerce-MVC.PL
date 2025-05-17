public class User
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    // Navigation
    public ICollection<Cart> Carts { get; set; }
}
