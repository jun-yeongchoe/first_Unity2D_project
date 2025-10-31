using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    [SerializeField] private float hp = 500f;
    private float maxHp = 500f;
    [SerializeField] private RectTransform hpFront;
    public float speed = 3.0f;
    public Rigidbody2D target;

    bool isLive = true;
    
    private Animator anim;

    [SerializeField] private Transform root;

    //전투 관련(순찰)
    NavMeshAgent agent;
    private Vector3 startPos; // 원래 위치
    private Vector3 objectPos; // 목표 위치
    private bool isMoving;
    //전투 관련(순찰)

    //플레이어 발견
    private bool detectTarget;
    private float detectRange = 10f;
    [SerializeField] LayerMask player;
    Vector2 targetPos;
    //플레이어 발견

    //벽감지
    [SerializeField] LayerMask wallMask;
    //벽감지

    //공격
    public Bullet_Boss bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] private float shootCooldown = 0.5f;
    private float lastShotTime;
    bool isFight;
    //공격

    float angle;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        startPos = transform.position;
        isMoving = true;
        anim.SetBool("9_Move", isMoving);
        isFight = false;
    }
    private void Update()
    {
        bool lookLeft = target.transform.position.x < transform.position.x;
        var s = transform.localScale;
        s.x = lookLeft ? Mathf.Abs(s.x) : -Mathf.Abs(s.x);
        transform.localScale = s;
        angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        targetPos = target.position;

        agent.SetDestination(targetPos);
    }

    private void FixedUpdate()
    {
        if (!isLive) return;
        var bAng = firePoint.rotation * Quaternion.Euler(0f, 0f, -90f);
        Vector3 atkDir = target.transform.position - transform.position;
        Vector2 center = transform.position + Vector3.up * 0.4f;
        detectTarget = Physics2D.OverlapCircle(center, detectRange, player);
        bool CanSee = CanIShoot();

        if (detectTarget && Time.time - lastShotTime >= shootCooldown && CanSee)
        {
            isFight = true;
            objectPos = target.position;
            agent.isStopped = true;
            isMoving = false;
            anim.SetBool("9_Move", isMoving);
            lastShotTime = Time.time;                  // 코루틴 시작 전에 쿨 갱신
            StartCoroutine(DelayAtk(bAng, atkDir));
        }
        if (isFight && !CanSee)
        {
            agent.isStopped = false;
            isMoving = true;
            anim.SetBool("9_Move", isMoving);
            agent.SetDestination(targetPos);
        }
    }

    private bool CanIShoot()
    {
        Vector2 dir = target.transform.position - firePoint.position;
        float dist = dir.magnitude;

        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, dir.normalized, dist, wallMask);
        Debug.DrawLine(firePoint.position, target.transform.position, hit.collider ? Color.blue : Color.yellow, 2.5f);
        if (!hit.collider) return true;
        else return false;
    }

    private void OnEnable()
    {
        isLive = true;
    }


    public void TakeDamage(float d)
    {
        if (!isLive) return;
        hp -= d;
        Health.HpDown(hpFront, (int)hp, (int)maxHp);
        Debug.Log($"현재 체력 : {hp}");
        if (hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (!isLive) return;
        isLive = false;


        Vector3 deathPos = transform.position;


        anim.SetTrigger("4_Death");
        StartCoroutine(DeathDelay());
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(0.5f); // 죽는 모션 잠깐 재생 후
        root.gameObject.SetActive(false);
    }

    IEnumerator DelayAtk(Quaternion BA, Vector3 AD)
    {
        yield return new WaitForSecondsRealtime(0.1f);
        var go = Instantiate(bulletPrefab, firePoint.position, BA);
        go.GetDir(AD);
        
    }

    public void Init(Rigidbody2D playerRb)
    {
        target = playerRb;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 0.4f, detectRange);
    }
}

