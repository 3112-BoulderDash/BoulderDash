namespace Boulder_Dash;

public class AccountFactory
{

    private int _nextId = 1;
    private readonly List<IAccount> _accounts = new();
    
    private class Account : IAccount
    {
        public string Username { get; set; }
        public int Id { get; set; }
        public ISkin SelectedSkin { get; set; }

        public List<ISkin> PlayerSkins { get; set; }
        public bool IsAdmin => false;
        public int TotalPoints { get; set; }
        public Account(string username, int id)
        {
            Username = username;
            Id = id;
            //Account Skin inventory
            PlayerSkins = new List<ISkin>();
            //all accounts should have default skin in inventory
            PlayerSkins.Add(new DefaultSkin());
            TotalPoints = 0;
            SelectedSkin = PlayerSkins[0];
        }
    }
    // admin account
    private class AdminAccount : IAccount
    {
        public string Username { get; set; }
        public int Id { get; set; }
        public ISkin SelectedSkin { get; set; }
        public List<ISkin> PlayerSkins { get; set; }
        public bool IsAdmin => true;
        public int TotalPoints { get; set; }
        public AdminAccount(string username, int id)
        {
            Username = username;
            Id = id;
            PlayerSkins = new List<ISkin>();
            SelectedSkin = new DefaultSkin();
            PlayerSkins.Add(new DefaultSkin());
            TotalPoints = 0;
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
    public IAccount CreateAdminAccount(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be null or whitespace.", nameof(username));
        if (FindByUsername(username) != null)
            throw new InvalidOperationException("That username is already taken.");
        int id = _nextId++;
        var account = new AdminAccount(username, id);
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
    }public IAccount? FindById(int id)
    {
        foreach (var account in _accounts)
        {
            if (account.Id == id)
                return account;
        }
        return null;
    }

    public void CreateAccountFromFile(int id, string username, bool isAdmin)
    {
        if (FindById(id) != null)
            return;
        
        IAccount account;
        if (isAdmin)
            account = new AdminAccount(username, id);
        else
            account = new Account(username, id);
        _accounts.Add(account);

        if (id >= _nextId)
            _nextId = id + 1;
    } 
    public IReadOnlyList<IAccount> GetAllAccounts()
    {
        return _accounts.AsReadOnly();
    }
}
