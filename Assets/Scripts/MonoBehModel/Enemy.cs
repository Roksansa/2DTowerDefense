using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private Vector3[] _waypoints;
    private int _counter = 0;
    private const float ChangeDist = 0.001f;

    [SerializeField] private int _damage;

    [SerializeField] private int _gold;

    void Update()
    {
        if (_counter == _waypoints.Length)
        {
            HasReachedTheFortress();
            ObjectController.CurEnemy -= 1;
            Destroy(gameObject);
        }
        else
        {
            float dist = Vector3.Distance(transform.position, _waypoints[_counter]);
            if (dist < ChangeDist)
            {
                _counter++;
            }
            else
            {
                float step = _speed*Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position,
                    _waypoints[_counter], step);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            UIController.Instance.GameController.EventManager.PostNotification(EventType.DeadEnemy, this, _gold);
            Destroy(other.gameObject);
            ObjectController.CurEnemy -= 1;
            Destroy(gameObject);
        }
    }

    public void HasReachedTheFortress()
    {
        UIController.Instance.GameController.EventManager.PostNotification(EventType.EnemyDamage, this, _damage);
        //GAMEOVER CONDITIONS
    }
}