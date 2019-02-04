using UnityEngine;
using UnityEngine.UI;

public class MoneyCounterUI : MonoBehaviour
{
    private Text _uiText;

    private void Awake()
    {
        _uiText = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        var gameController = UIController.Instance.GameController;
        gameController.EventManager.AddListener(EventType.MoneyChance, OnUpdateMoneyCounter);
        gameController.EventManager.PostNotification(EventType.MoneyChance, this, gameController.MoneyCounter.Money);
    }

    private void OnUpdateMoneyCounter(EventType eventType, object sender, object param = null)
    {
        if (param != null)
            _uiText.text = ((int) param).ToString();
    }
}