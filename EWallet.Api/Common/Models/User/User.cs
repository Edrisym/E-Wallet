namespace EWallet.Api.Common.Models.User;

public class User
{
    private User(string userName, string firstName, string lastName, string email, string phoneNumber,
        string password, List<Wallet> wallets)
    {
        Id = UserId.NewId();
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
        Wallets = wallets;
        CreatedAtUtc = DateTime.UtcNow;
    }

    private User()
    {
    }

    public UserId Id { get; private set; }
    public string UserName { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Password { get; private set; }
    public List<Wallet> Wallets { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? ModifiedAtUtc { get; private set; }

    public static User Create(string userName, string firstName, string lastName, string email,
        string phoneNumber, string password, List<Wallet> wallets)
    {
        return new User
        (
            userName,
            firstName,
            lastName,
            email,
            phoneNumber,
            password,
            wallets
        );
    }
}