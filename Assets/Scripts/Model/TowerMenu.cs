using System.Collections.Generic;

public class TowerMenu
{
    private Tower _currentTower;
    private readonly GameController _gameController;
    private readonly List<TowerInfo> _towerInfos = new List<TowerInfo>();

    private int _level;
    private int _currentUpgradePrice;
    private int _currentSellPrice;

    public TowerMenu(GameController gameController, List<TowerInfo> list)
    {
        _towerInfos = list;
        _gameController = gameController;
    }

    public List<TowerInfo> TowerInfos
    {
        get { return _towerInfos; }
    }

    public bool OnUpgradeTower()
    {
        if (_level == _towerInfos.Count - 1)
            return false;
        int money = _gameController.MoneyCounter.Money;
        if (money >= _currentUpgradePrice)
        {
            _gameController.MoneyCounter.OnChangeMoney(-_currentUpgradePrice);
            _currentTower.Upgrade();
            _currentTower = null;
            return true;
        }
        return false;
    }

    public void OnSellTower()
    {
        _gameController.MoneyCounter.OnChangeMoney(_currentSellPrice);
        _currentTower = null;
    }

    public void SetNewTower(Tower tower)
    {
        _currentTower = tower;
        _level = tower.UpgradeLevel;
        _currentSellPrice = _towerInfos[_level].SellPrice;
        _currentUpgradePrice = _towerInfos[_level].UpgradePrice;
    }
}

public class TowerInfo
{
    public readonly string NameSpriteForMenu;
    public readonly int UpgradePrice;
    public readonly int SellPrice;

    public TowerInfo(string str, int upd, int sell)
    {
        NameSpriteForMenu = str;
        UpgradePrice = upd;
        SellPrice = sell;
    }
}