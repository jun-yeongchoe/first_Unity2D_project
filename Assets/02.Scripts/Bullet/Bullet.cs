using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 10f;

    private Vector3 direction;
    public Vector3 movePos;
    private Rigidbody2D rb;

    // �Ѿ� ƨ��
    Vector3 lastVelocity;
    int count;
    // �Ѿ� ƨ��

    // Ǯ��
    private IObjectPool<Bullet> ManagedPool;
    // Ǯ��

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        count = 0;
        movePos = new Vector2(direction.x, direction.y).normalized * speed;
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
            count++;
            var speed = lastVelocity.magnitude;
            var dir = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            rb.velocity = dir * Mathf.Max(speed, 0f);

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;           
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if(count >= 5)
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if(collision.gameObject.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        } 
    }
}
