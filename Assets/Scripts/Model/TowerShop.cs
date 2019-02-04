public class TowerShop
{
    private int _price = 1200;

    private readonly GameController _gameController;

    public TowerShop(GameController gameController)
    {
        _gameController = gameController;
        gameController.EventManager.AddListener(EventType.BuyTower, OnByeTower);
    }

    public int Price
    {
        get { return _price; }
        set { _price = value; }
    }

    private void OnByeTower(EventType eventType, object sender, object param)
    {
        int money = _gameController.MoneyCounter.Money;
        if (money >= _price)
        {
            _gameController.MoneyCounter.OnChangeMoney(-_price);
            _gameController.EventManager.PostNotification(EventType.CreateTower, this);
        }
    }
}