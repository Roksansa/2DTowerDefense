using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 1f;

    public Vector3 Direction;

    private void Start()
    {
        Direction = Direction.normalized;
        float angle = Mathf.Atan2(-Direction.y, -Direction.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //переделать
        Destroy(gameObject, 10);
    }

    void Update()
    {
        transform.position += Direction*Time.deltaTime*Speed;
    }
}