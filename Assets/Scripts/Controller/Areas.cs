using UnityEngine;

public class Areas : MonoBehaviour
{
    private bool _isAreaAllowed;
    public GameObject[] areasVisual;

    private void Awake()
    {
        foreach (var area in areasVisual)
        {
            area.SetActive(false);
        }
    }

    public bool IsAreaAllowed
    {
        get { return _isAreaAllowed; }
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