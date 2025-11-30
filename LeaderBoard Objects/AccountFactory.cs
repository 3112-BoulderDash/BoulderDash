namespace Boulder_Dash;

public class AccountFactory
{

    private int _nextId = 1;
    private readonly List<IAccount> _accounts = new();
    
    private class Account : IAccount
    {
        public string Username { get; set; }
        public int Id { get; set; }

        public Account(string username, int id)
        {
            Username = username;
            Id = id;
        }
    }
    
    public IAccount CreateAccount(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be null or whitespace.", nameof(username));
        
        if (FindByUsername(username) != null)
            throw new InvalidOperationException("That username is already taken.");

        int id = _nextId++;
        var account = new Account(username, id);
        _accounts.Add(account);
        return account;          // returns as IAccount
    }
    public IAccount? FindByUsername(string username)
    {
        if (username == null)
            return null;

        foreach (var account in _accounts)
        {
            if (account.Username == username)  
            {
                return account;
            }
        }

        return null;
    }
}