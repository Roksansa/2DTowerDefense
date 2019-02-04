using UnityEngine;

public class HaloSetting : MonoBehaviour
{
    private Projector _projectorHalo;

    private void Awake()
    {
        _projectorHalo = GetComponent<Projector>();
    }

    public void HaloOn(float radius)
    {
        _projectorHalo.orthographicSize = radius;
        _projectorHalo.enabled = true;
    }

    public void HaloOff()
    {
        _projectorHalo.enabled = false;
    }
}