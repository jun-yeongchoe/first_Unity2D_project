using UnityEngine;
using UnityEngine.Pool;

public class Bullet_E : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private int damage = 10;

    private Vector3 direction;
    public Vector3 movePos;
    private Rigidbody2D rb;

    // ÃÑ¾Ë Æ¨±è
    Vector3 lastVelocity;
    int count;
    // ÃÑ¾Ë Æ¨±è


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

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(collision.gameObject.TryGetComponent<Player>(out var p))
            {
                p.TakeDamage(damage);
            }
            Destroy(gameObject);
        } 
    }
}
