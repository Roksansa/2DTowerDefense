public class LivesCounter
{
    private int _maxLives;
    private int _lives;
    private readonly GameController _gameController;

    public LivesCounter(GameController gameController, int max)
    {
        _lives = _maxLives = max;
        _gameController = gameController;

        gameController.EventManager.AddListener(EventType.EnemyDamage, LoseLife);
    }

    private void LoseLife(EventType eventtype, object sender, object param)
    {
        var damage = param as int? ?? 0;
        _lives -= damage;
        if (_lives > 0)
        {
            _gameController.EventManager.PostNotification(EventType.HealthChance, this, _lives);
            return;
        }
        _lives = 0;
        _gameController.EventManager.PostNotification(EventType.HealthChance, this, _lives);
        _gameController.EventManager.PostNotification(EventType.Dead, this, _lives);
    }

    public int Lives
    {
        get { return _lives; }
    }
}