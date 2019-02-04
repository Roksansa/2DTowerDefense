using System;
using System.Collections.Generic;
using System.Linq;

public enum EventType
{
    HealthChance,
    Dead,
    MoneyChance,
    TowerChance,
    BuyTower,
    CreateTower,
    DeadEnemy,
    EnemyDamage
}

/**реализован как singleton*/
public class EventManager
{
    private static readonly EventManager instance = new EventManager();

    public static EventManager InstancePublic
    {
        get { return instance; }
    }

    static EventManager() { }
    private EventManager() { }

    private static EventManager Instance
    {
        get { return instance; }
    }

    //Тип делегата
    public delegate void OnEvent(EventType eventType, object sender, object param = null);

    //Массив получателей
    private Dictionary<EventType, List<OnEvent>> _listeners = new Dictionary<EventType, List<OnEvent>>();

    //Функция подписки слушателей на события
    public void AddListener(EventType eventType, OnEvent listener)
    {
        List<OnEvent> curListenerList = null;
        if (_listeners.TryGetValue(eventType, out curListenerList))
        {
            curListenerList.Add(listener);
            return;
        }

        curListenerList = new List<OnEvent> {listener};
        _listeners.Add(eventType, curListenerList);
    }

    //функция оповещения о событие
    public void PostNotification(EventType eventType, object sender, object param = null)
    {
        List<OnEvent> curListenerList = null;
        //если подписчиков нет
        if (!_listeners.TryGetValue(eventType, out curListenerList))
        {
            return;
        }

        for (var i = 0; i < curListenerList.Count; i++)
        {
            curListenerList[i](eventType, sender, param);
        }
    }

    //удалить событие из словаря и всех подписчиков
    public void RemoveEvent(EventType eventType)
    {
        _listeners.Remove(eventType);
    }

    //отписаться от события
    public void RemoveListener(EventType eventType, OnEvent listener)
    {
        List<OnEvent> curListenerList = null;
        if (_listeners.TryGetValue(eventType, out curListenerList))
        {
            curListenerList.Remove(listener);
        }
    }

    void RemoveAllEvent()
    {
        _listeners = new Dictionary<EventType, List<OnEvent>>();
    }
}