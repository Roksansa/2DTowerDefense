using UnityEngine;
using UnityEngine.UI;

public class LivesCounterUI : MonoBehaviour
{
    private Text _uiText;

    private void Awake()
    {
        _uiText = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        //прикрепиться
        var gameController = UIController.Instance.GameController;
        gameController.EventManager.AddListener(EventType.HealthChance, OnUpdateLivesCounter);
        //послать запрос на обновление
        gameController.EventManager.PostNotification(EventType.HealthChance, this,
            gameController.LivesCounter.Lives);
    }

    private void OnUpdateLivesCounter(EventType eventType, object sender, object param = null)
    {
        if (param != null)
            _uiText.text = ((int) param).ToString();
    }
}