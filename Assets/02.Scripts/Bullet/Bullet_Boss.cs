using UnityEngine;

public class Bullet_Boss : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private int damage = 10;

    private Vector3 direction;
    public Vector3 movePos;
    private Rigidbody2D rb;

    public PlayerSfx playerSfx;
    [SerializeField] private ParticleSystem boom;
    


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        movePos = new Vector2(direction.x, direction.y).normalized * speed;
        rb.velocity = movePos;
    }

    public void GetDir(Vector3 dir)
    {
        direction = dir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            FireSfx(transform.position);
            Destroy(gameObject);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3.0f);
            foreach (Collider2D hit in colliders) 
            {
                bool isPlayer = hit.TryGetComponent<Player>(out Player p);
                if (p != null) 
                {
                    p.GetComponent<Player>().TakeDamage(damage);
                }
            }
        }
    }

    private void FireSfx(Vector3 pos)
    {
        
        AudioSource.PlayClipAtPoint(playerSfx.explosion, pos, 0.3f);
        
        var fx = Instantiate(boom, pos, Quaternion.identity);
        fx.Play();

        var main = fx.main;
        float lifetime = main.duration + main.startLifetime.constantMax;
        Destroy(fx.gameObject, lifetime);
        
        
    }
}
