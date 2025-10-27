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

    //���� ����
        // ���� �� ���� �տ��ִ� �� ����
    Vector2 dir = Vector2.right;
    public float detectDistance = 1.0f;
    [SerializeField] private LayerMask obstacle;

        // ���� �� ���� �տ��ִ� �� ����

    NavMeshAgent agent;
    public Vector3 patrolPos; // ������ ��ġ
    //���� ����

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        var go = GameObject.FindWithTag("Player"); // �±� "Player" ���� ����
        target = go.GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {

        agent.SetDestination(target.position);
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
        dir = Random.insideUnitCircle.normalized;

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


}
