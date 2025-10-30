using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float hp = 30f;
    public float speed = 3.0f;
    public Rigidbody2D target;

    bool isLive = true;
    public bool hasKey;
    [SerializeField] GameObject key;

    private Animator anim;

    Rigidbody2D rb;
    SpriteRenderer sr;

    private bool KeyDropped = false;
    [SerializeField] private Transform root;

    //���� ����(����)
    NavMeshAgent agent;
    public Vector3 patrolPos; // ������ ��ġ
    private Vector3 startPos; // ���� ��ġ
    private Vector3 objectPos; // ��ǥ ��ġ
    private bool isArrived;
    private float arrivalRadius = 0.7f;
    private bool isMoving;
    bool isLeft;
    //���� ����(����)

    //�÷��̾� �߰�
    private bool detectTarget;
    private float detectRange = 6f;
    [SerializeField] LayerMask player;
    Vector2 targetPos;
    //�÷��̾� �߰�

    //������
    [SerializeField] LayerMask wallMask;
    //������

    //����
    public Bullet_E bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] private float shootCooldown = 3.0f;
    private float lastShotTime;
    bool isFight;
    //����

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = true;
        agent.updateUpAxis = false;
        startPos = transform.position;
        objectPos = patrolPos;
        isMoving = true;
        anim.SetBool("9_Move", isMoving);
        agent.SetDestination(patrolPos);
        isFight = false;
    }

    private void Update()
    {
        StartCoroutine(WalkDir());
        var s = transform.localScale;
        s.x = isLeft ? Mathf.Abs(s.x) : -Mathf.Abs(s.x);
        transform.localScale = s;

        isArrived = Vector2.Distance(transform.position, objectPos) < arrivalRadius;
        targetPos = target.position;


        if (isArrived)
        {
            isMoving = false;
            anim.SetBool("9_Move", isMoving);
            objectPos = (objectPos == patrolPos) ? startPos : patrolPos;
            StartCoroutine(DelayToWalk());
        }

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
            agent.isStopped = true;
            isMoving = false;
            anim.SetBool("9_Move", isMoving);
            lastShotTime = Time.time;                  // �ڷ�ƾ ���� ���� �� ����
            StartCoroutine(DelayAtk(bAng, atkDir));
        }
        if(isFight && !CanSee)
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
        KeyDropped = false;

    }


    public void TakeDamage(float d)
    {
        if (!isLive) return;
        hp -= d;
        Debug.Log($"���� ü�� : {hp}");
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

        if (hasKey && !KeyDropped)
        {
            Instantiate(key, deathPos, Quaternion.identity);
        }

        anim.SetTrigger("4_Death");
        StartCoroutine(DeathDelay());
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(0.5f); // �״� ��� ��� ��� ��
        root.gameObject.SetActive(false);
    }
    IEnumerator DelayToWalk()
    {
        yield return new WaitForSeconds(2);
        agent.SetDestination(objectPos);
        isMoving = true;
        anim.SetBool("9_Move", isMoving);
    }

    IEnumerator WalkDir()
    {
        Vector2 lastPos = transform.position;
        yield return new WaitForSeconds(0.1f);
        Vector2 nowPos = transform.position;
        float dir = lastPos.x - nowPos.x;
        if (dir < 0) isLeft = false; 
        else if (dir > 0) isLeft = true;
        else { yield break; }
    }
    IEnumerator DelayAtk(Quaternion BA, Vector3 AD)
    {
        yield return new WaitForSecondsRealtime(0.3f);
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

