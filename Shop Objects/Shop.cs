namespace Boulder_Dash;

public class Shop
{
    private List<ISkin> ShopSkins;

    public Shop()
    {
        ShopSkins = new List<ISkin>();
        ShopSkins.Add(new FerrariSkin());
    }
    
    public void DisplayMenu()
    {
        int choice = 1;
        Console.WriteLine("Welcome to the shop!");
        foreach (var item in ShopSkins)
        {
            Console.WriteLine($"{choice}. {item.name}");
            choice++;
        }
    }

    public bool BoughtItem(IAccount account)
    {
        Console.WriteLine("Enter the number of the item to buy:");
        string? input = Console.ReadLine();

        if (!int.TryParse(input, out int choice))
        {
            Console.WriteLine("Invalid input.");
            return false;
        }

        if (choice < 1 || choice > ShopSkins.Count)
        {
            Console.WriteLine("Invalid option.");
            return false;
        }
        
        ISkin selectedItem = ShopSkins[choice - 1];
        
        //checks to see if player has enough currency
        //commented out until we have currency associated with account
        /*
        if (selectedItem.price > account.Currency)
        {
            Console.WriteLine("You don't have enough currency to buy this item.");
            return false;
        }
        */

        //add item to player inventory
        account.PlayerSkins.Add(selectedItem);

        Console.WriteLine($"You bought {selectedItem.name}!");
        return true;
    }
}