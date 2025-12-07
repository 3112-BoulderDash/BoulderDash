namespace  Boulder_Dash;

public interface IAccount {
    // Username for an account
    string Username { get; set; }

    // ID for an account
    int Id { get; set; }
    
    //Points in account
    
    ISkin SelectedSkin { get; set; }
    List<ISkin> PlayerSkins { get; set; }
    Boolean IsAdmin { get; }
    int TotalPoints { get; set; }
}
