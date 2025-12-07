namespace  Boulder_Dash;

public interface IAccount {
    // Username for an account
    string Username { get; set; }

    // ID for an account
    int Id { get; set; }
    
    List<ISkin> PlayerSkins { get; set; }
}
