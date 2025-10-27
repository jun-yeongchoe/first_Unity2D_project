using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

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

    //전투 관련(순찰)
    NavMeshAgent agent;
    public Vector3 patrolPos; // 순찰할 위치
    private Vector3 startPos; // 원래 위치
    private Vector3 objectPos; // 목표 위치
    private bool isArrived;
    private float arrivalRadius = 0.7f;
    private bool isMoving;
    //전투 관련(순찰)

    //플레이어 발견
    private bool detectTarget;
    private float detectRange = 6f;
    [SerializeField] LayerMask player;
    //플레이어 발견

    //공격
    public Bullet bulletPrefab;
    [SerializeField] Transform firePoint;
    //공격

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        var go = GameObject.FindWithTag("Player"); // 태그 "Player" 지정 가정
        target = go.GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        startPos = transform.position;
        objectPos = patrolPos;
        isMoving = true;
        anim.SetBool("9_Move", isMoving);
        agent.SetDestination(patrolPos);
    }

    private void Update()
    {
        var bAng = firePoint.rotation * Quaternion.Euler(0f, 0f, -90f);
        Vector3 atkDir = target.transform.position - transform.position;

        detectTarget = Physics2D.OverlapCircle(transform.position, detectRange, player);
        isArrived = Vector2.Distance(transform.position, objectPos) < arrivalRadius;
        if (detectTarget)
        {
            var go = Instantiate(bulletPrefab, firePoint.position, bAng);
            go.GetDir(atkDir);
        }
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
    }

    private void OnEnable()
    {
        isLive = true;
        KeyDropped = false;
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();

    }


    public void TakeDamage(float d)
    {
        if (!isLive) return;
        hp -= d;
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

            if (hasKey && !KeyDropped)
            {
                Instantiate(key, deathPos, Quaternion.identity);
            }

        anim.SetTrigger("4_Death");
        StartCoroutine(DeathDelay());
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(0.5f); // 죽는 모션 잠깐 재생 후
        root.gameObject.SetActive(false);
    }
    IEnumerator DelayToWalk()
    {
        yield return new WaitForSeconds(2);
        agent.SetDestination(objectPos);
        isMoving = true;
        anim.SetBool("9_Move", isMoving);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 0.4f, detectRange);
    }
}
