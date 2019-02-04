public class Tower
{
    private float _rangeRadius = 4f;
    private float _reloadTime = 3f;
    private int _upgradeLevel = 0;
    private bool _placingMode = true;

    private float _elapsedTime;

    public float RangeRadius
    {
        get { return _rangeRadius; }
    }

    public float ReloadTime
    {
        get { return _reloadTime; }
    }

    public float ElapsedTime
    {
        get { return _elapsedTime; }
    }

    public bool PlacingMode
    {
        get { return _placingMode; }
        set { _placingMode = value; }
    }

    public int UpgradeLevel
    {
        get { return _upgradeLevel; }
        set { _upgradeLevel = value; }
    }

    public void Attack(bool attack)
    {
        if (_elapsedTime >= _reloadTime && attack)
        {
            _elapsedTime = 0;
        }
    }

    public void AddTime(float dt)
    {
        _elapsedTime += dt;
    }

    public void Upgrade()
    {
        _rangeRadius += 3f;
        _reloadTime -= 0.5f;
        _upgradeLevel++;
    }
}