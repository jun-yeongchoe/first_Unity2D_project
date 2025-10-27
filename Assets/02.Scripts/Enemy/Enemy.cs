using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    //전투 관련
        // 순찰 시 벽이 앞에있는 지 감지
    Vector2 dir = Vector2.right;
    public float detectDistance = 1.0f;
    [SerializeField] private LayerMask obstacle;
    
        // 순찰 시 벽이 앞에있는 지 감지

    private bool findPlayer; // 플레이어 감지
    public Vector3 patrolPos; // 순찰할 위치
    //전투 관련

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 dir = -(Vector2)transform.right ;
        bool isBlocked = Physics2D.Raycast(transform.position, dir, detectDistance, obstacle);

        if (!isBlocked)
        {
            bool isMoving = true;
            anim.SetBool("9_Move", isMoving);
            transform.position += (Vector3)(dir * speed * Time.deltaTime);
        }
        Debug.DrawRay(transform.position, dir * detectDistance, Color.red);
    }

    private void FixedUpdate()
    {
        if (!isLive) return;
        //Move();
    }

    private void OnEnable()
    {
        isLive = true;
        KeyDropped = false;
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        dir = Random.insideUnitCircle.normalized;


    }

    private void Move()
    {
        
        Vector3 next = Vector3.MoveTowards(transform.position, patrolPos, speed * Time.deltaTime);
        transform.position = next;
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


}
