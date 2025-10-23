using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 10;

    private Vector3 direction;
    private Rigidbody2D rb;

    private IObjectPool<Bullet> ManagedPool;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(direction.x, direction.y) * speed;
    }

    public void GetDir(Vector3 dir)
    {
        direction = dir;
    }
    //public void SetManagedPool(IObjectPool<Bullet> pool)
    //{
    //    ManagedPool = pool;
    //}

    //public void Shot(Vector3 dir)
    //{
    //    direction = dir;
    //    Invoke("DestroyBullet", 5f);
    //}

    //public void DestroyBullet()
    //{
    //    ManagedPool.Release(this);
    //}
}
