using System;

public class MoneyCounter
{
    private int _money = 3000;

    private readonly GameController _gameController;

    public int Money
    {
        get { return _money; }
    }

    public MoneyCounter(GameController gameController, int cash)
    {
        _money = cash;
        this._gameController = gameController;

        gameController.EventManager.AddListener(EventType.DeadEnemy, OnChangeMoney);
    }

    private void OnChangeMoney(EventType eventType, object sender, object param)
    {
        var amount = param as int? ?? 0;
        _money += amount;
        if (_money < 0)
        {
            _money = 0;
        }
        _gameController.EventManager.PostNotification(EventType.MoneyChance, this, _money);
    }

    public void OnChangeMoney(int amount)
    {
        _money += amount;
        if (_money < 0)
        {
            _money = 0;
        }
        _gameController.EventManager.PostNotification(EventType.MoneyChance, this, _money);
    }
}