using System.Collections.Generic;

public class GameController
{
    public TowerShop TowerShop;
    public MoneyCounter MoneyCounter;
    public LivesCounter LivesCounter;
    public TowerMenu TowerMenu;
    public EventManager EventManager;

    public void Init()
    {
        EventManager = EventManager.InstancePublic;
        TowerShop = new TowerShop(this) {Price = 400};
        MoneyCounter = new MoneyCounter(this, 1000);
        LivesCounter = new LivesCounter(this, 100);
        //temp, parse 
        var tempList = new List<TowerInfo>()
        {
            new TowerInfo("tower_1_square", 500, 650),
            new TowerInfo("tower_2_square", 900, 1100),
            new TowerInfo("tower_3_square", 0, 1500)
        };
        TowerMenu = new TowerMenu(this, tempList);
    }
}