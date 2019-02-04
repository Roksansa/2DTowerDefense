using UnityEngine;
using UnityEngine.UI;

public class TowerMenuUI : MonoBehaviour
{
    public Image UiImage;
    public Text UpgradePriceText;
    public Text SellPriceText;
    public Button UpgradeButton, SellButton;
    private TowerUI _currentTower;
    private GameController _gameController;

    private void Awake()
    {
        gameObject.SetActive(false);        
        UpgradeButton.onClick.AddListener(UpgradeTower);
        SellButton.onClick.AddListener(SellTower);
        _gameController = UIController.Instance.GameController;
        _gameController.EventManager.AddListener(EventType.TowerChance, OnUpdate);
    }

    private void OnDestroy()
    {
        if (_gameController != null)
            _gameController.EventManager.RemoveListener(EventType.TowerChance, OnUpdate);
    }

    private void OnUpdate(EventType eventType, object sender, object param = null)
    {
        if (_currentTower != null)
        {
            _currentTower.MyChildHalo.HaloOff();
            if (ReferenceEquals(_currentTower, sender))
            {
                _currentTower = null;

                gameObject.SetActive(false);
                return;
            }
        }
        if (!(sender is TowerUI)) return;
        _currentTower = sender as TowerUI;
        _gameController.TowerMenu.SetNewTower(_currentTower.Tower);
        var info = _gameController.TowerMenu.TowerInfos[_currentTower.Tower.UpgradeLevel];
        UiImage.sprite = Resources.Load<Sprite>(info.NameSpriteForMenu);
        UpgradePriceText.text = _currentTower.Tower.UpgradeLevel == _gameController.TowerMenu.TowerInfos.Count - 1
            ? "-"
            : "$" + info.UpgradePrice;
        SellPriceText.text = "$" + info.SellPrice;
        gameObject.SetActive(true);
    }

    private void UpgradeTower()
    {
        bool isUpgrade = _gameController.TowerMenu.OnUpgradeTower();
        if (isUpgrade)
        {
            _currentTower.MyChildHalo.HaloOff();
            _currentTower = null;
            gameObject.SetActive(false);
        }
    }

    private void SellTower()
    {
        _gameController.TowerMenu.OnSellTower();
        UIController.Instance.RemoveTower(_currentTower);
        Destroy(_currentTower.gameObject);
        gameObject.SetActive(false);
    }
}