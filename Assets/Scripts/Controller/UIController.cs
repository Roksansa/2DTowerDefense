using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public List<TowerUI> Towers = new List<TowerUI>();
    public GameController GameController = new GameController();

    public static UIController Instance
    {
        get { return instance; }
    }

    private static UIController instance = null;

    private void Awake()
    {
        instance = this;
        GameController.Init();
    }

    public void AddTower(TowerUI tower)
    {
        Towers.Add(tower);
    }

    public bool RemoveTower(TowerUI tower)
    {
        return Towers.Remove(tower);
    }
}