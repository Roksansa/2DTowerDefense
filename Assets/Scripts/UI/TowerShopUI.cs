using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TowerShopUI : MonoBehaviour
{
    public Button BuyButton;
    public Text CountText;

    private UIController _uiController;

    private void Awake()
    {
        BuyButton.onClick.AddListener(BuyTower);
    }

    private void Start()
    {
        _uiController = UIController.Instance;
        CountText.text = _uiController.GameController.TowerShop.Price.ToString();
    }

    private void BuyTower()
    {
        if (_uiController.Towers.All(val => !val.Tower.PlacingMode))
            _uiController.GameController.EventManager.PostNotification(EventType.BuyTower, this);
    }
}