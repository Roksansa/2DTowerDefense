using System.Linq;
using UnityEngine;

public class PlacingTowerController : MonoBehaviour
{
    private Areas _areasController;
    private static TowerUI curTower;
    private UIController _uiController;

    public GameObject TowerPrefab;
    

    private void Awake()
    {
        _areasController = GameObject.Find("AreasForTower").GetComponent<Areas>();
        Debug.Log(_areasController);
    }

    private void Start()
    {
        _uiController = UIController.Instance;
        _uiController.GameController.EventManager.AddListener(EventType.CreateTower, CreateTower);
    }

    private void CreateTower(EventType eventType, object sender, object param)
    {
        GameObject gameObject = Instantiate(TowerPrefab, Input.mousePosition, Quaternion.identity);
        var towerUI = gameObject.GetComponent<TowerUI>();
        var tower = new Tower();
        towerUI.Init(tower);
        SetTower(towerUI);
    }

    //вызвали установку 
    private void SetTower(TowerUI tower)
    {
        UIController.Instance.AddTower(tower);
        curTower = tower;
        foreach (var area in _areasController.areasVisual)
        {
            area.SetActive(true);
        }
    }

    private void Update()
    {
        if (curTower == null) return;        
        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;
        curTower.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 10));
        if (Input.GetMouseButtonDown(0) && _areasController.IsAreaAllowed && _uiController.Towers.All(tower => !tower.IsAreaAllowed)) {
            curTower.GetComponent<BoxCollider2D>().enabled = true;
            Debug.Log(curTower.GetComponent<BoxCollider2D>().enabled);
            curTower.Tower.PlacingMode = false;
            curTower = null;
            foreach (var area in _areasController.areasVisual)
            {
                area.SetActive(false);
            }
        }
    }
}