namespace  Boulder_Dash;
/*should totalAccount be all accounts the factory creates or all accounts in the program?
If we do store accounts in a txt file or smth, I don't think totalAccount should be in here 
I'll come back to this and update AccountFactory based on what you think.*/
public class AccountFactory
{
    /*initialize id counter at 1 so when a user
     creates an account in CreateAccount, its assigned 1 and 
     incremented for new users.*/
    private int _nextId = 1;
    

    //private object class that implements IAccount validating factory 
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

        int id = _nextId++;
        return new Account(username, id); // returns as IAccount
    }
}

