using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float hp = 30f;
    public float speed;
    public Rigidbody2D target;

    bool isLive = true;
    public bool hasKey;
    [SerializeField] GameObject key;

    private Animator anim;

    Rigidbody2D rb;
    SpriteRenderer sr;

    private bool KeyDropped = false;
    private Transform dropLoc;
    [SerializeField] private Transform root;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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


        Vector3 deathPos = root.position;
            

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
