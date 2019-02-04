using UnityEngine;

public class TowerUI : MonoBehaviour
{
    private Tower _tower;
    private UIController _uiController;

    public Tower Tower
    {
        get { return _tower; }
    }

    public GameObject BulletPrefab;

    private HaloSetting _myChildHalo;
    private bool _isAreaAllowed;

    public bool IsAreaAllowed
    {
        get { return _isAreaAllowed; }
    }

    public HaloSetting MyChildHalo
    {
        get { return _myChildHalo; }
    }


    private void Awake()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        _myChildHalo = GetComponentInChildren<HaloSetting>();
    }

    private void Start()
    {
        _uiController = UIController.Instance;
    }

    private void Update()
    {
        if (_tower.PlacingMode) return;
        var attack = Attack();
        _tower.Attack(attack);
        if (!attack)
            _tower.AddTime(Time.deltaTime);
    }

    public void Init(Tower tower)
    {
        _tower = tower;
    }

    public bool Attack()
    {
        if (!(_tower.ElapsedTime >= _tower.ReloadTime)) return false;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _tower.RangeRadius);
        if (hitColliders.Length == 0) return false;
        float min = int.MaxValue;
        int index = -1;
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].tag == "Enemy")
            {
                float distance = Vector2.Distance(hitColliders[i].transform.position, transform.position);
                if (distance < min)
                {
                    index = i;
                    min = distance;
                }
            }
        }
        if (index == -1)
            return false;
        Transform target = hitColliders[index].transform;
        Vector2 direction = (target.position - transform.position).normalized;
        //Create Bullet
        GameObject bullet = Instantiate(BulletPrefab,
            transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Direction = direction;
        return true;
    }

    private void OnMouseDown()
    {
        _myChildHalo.HaloOn(_tower.RangeRadius);
        _uiController.GameController.EventManager.PostNotification(EventType.TowerChance, this, _tower);
    }
    
    private void OnMouseEnter()
    {
        _isAreaAllowed = true;
    }

    private void OnMouseExit()
    {
        _isAreaAllowed = false;
    }
}