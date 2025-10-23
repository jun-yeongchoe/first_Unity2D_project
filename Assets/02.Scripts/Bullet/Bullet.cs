using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 10;

    private Vector3 direction;
    public Vector3 movePos;
    private Rigidbody2D rb;

    // ÃÑ¾Ë Æ¨±è
    Vector3 lastVelocity;
    // ÃÑ¾Ë Æ¨±è

    // Ç®¸µ
    private IObjectPool<Bullet> ManagedPool;
    // Ç®¸µ

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        movePos = new Vector2(direction.x, direction.y) * speed;
        rb.velocity = movePos;
        Destroy(gameObject, 5f);
    }


    private void FixedUpdate()
    {
        lastVelocity = rb.velocity;
    }

    public void GetDir(Vector3 dir)
    {
        direction = dir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            var speed = lastVelocity.magnitude;
            var dir = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            rb.velocity = dir * Mathf.Max(speed, 0f);
        }
    }


    // Ç®¸µ
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
    // Ç®¸µ
}
