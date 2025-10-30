using System.Collections;
using System.ComponentModel;
using System.Net.NetworkInformation;
using UnityEngine;

[System.Serializable]
public struct PlayerSfx
{
    public AudioClip[] fire;
    public AudioClip explosion;
    
}

public enum atkType { hit, shoot }
public class Player : MonoBehaviour 
{
    //캐릭터 상태
    public int hp;
    private int maxHp=100;
    [SerializeField] private RectTransform hpFront;
    public bool isLive = true;
    //캐릭터 상태

    //걷기
    [Header("캐릭터 이동관련")]
    [SerializeField] private float moveSpeed = 5.0f;
    public float defaultSpeed;
    private Animator anim; 
    float h; 
    float v;
    Rigidbody2D rb;
    //걷기

    //회전
    float angle;
    //회전

    //대쉬
    bool isDash;
    bool canDash=true;
    float dashCooldown = 3.0f;
    float dashSpeed = 15.0f;
    float dashTime;
    public float defaultTime;
    //대쉬

    //공격
    [Header("공격관련")]
    [SerializeField] Transform firePoint;
    [SerializeField] float atkDamage = 5f;
    

    private Vector3 mousePos;
    public Bullet bulletPrefab;
    atkType attackType;
    float atkRange = 0.7f;
    bool isEnemy;
    [SerializeField] private LayerMask enemyLayer;


    // 사운드 및 이펙트
    private AudioSource audioFx;
    public PlayerSfx playerSfx;
    public ParticleSystem muzzleFlash;
    public ParticleSystem hitFX;
    //공격

    //기타

    public static bool hasKey { get; private set; }
    public static bool withObject { get; set; }

    //기타
    private void Awake() 
    { 
        rb = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();
        defaultSpeed = moveSpeed;
        audioFx = GetComponent<AudioSource>();
        isLive = true;
    }
   

    private void Update() 
    {
        Debug.Log($"현재 상태 : {isLive}");
        Debug.Log($"플레이어 HP : {hp}");
        Debug.Log($"인질여부 : {withObject}");
        // 걷기
        h = Input.GetAxisRaw("Horizontal"); 
        v = Input.GetAxisRaw("Vertical");
        // 걷기

        // 공격
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        angle = Mathf.Atan2(mousePos.y - firePoint.position.y, mousePos.x - firePoint.position.x)*Mathf.Rad2Deg;
        var bAng = firePoint.rotation * Quaternion.Euler(0f, 0f, -90f);
        Vector3 atkDir = mousePos - transform.position;

        if (Input.GetMouseButtonDown(0))
        {
            FireSfx();
            switch (attackType)
                {
                    case atkType.shoot:
                    anim.SetTrigger("7_Shoot");
                    var go = Instantiate(bulletPrefab, firePoint.position, bAng);
                    go.GetDir(atkDir);
                    break;

                    //근접 공격
                    case atkType.hit:
                    anim.SetTrigger("8_Hit");
                    //적 공격 당하는 메서드
                    Collider2D hit = Physics2D.OverlapCircle(firePoint.position, atkRange, enemyLayer);
                    if(hit && hit.TryGetComponent<Enemy>(out var enemy))
                    {
                        enemy.TakeDamage(atkDamage);
                    }
                    if (hit && hit.TryGetComponent<Boss>(out var boss))
                    {
                        boss.TakeDamage(atkDamage);
                    }
                    break;
                    //근접 공격
                }
        }
        //공격

        //회전

        bool lookLeft = mousePos.x < transform.position.x;
        var s = transform.localScale;
        s.x = lookLeft ? Mathf.Abs(s.x) : -Mathf.Abs(s.x);
        transform.localScale = s;

        angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        //회전
        //대쉬
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(CoolTimeCo());
            isDash = true;
            anim.SetTrigger("10_Dash");
        }

        if (dashTime <= 0)
        {
            defaultSpeed = moveSpeed;
            if (isDash)
            {
                dashTime = defaultTime;
            }
        }
        else
        {
            dashTime -= Time.deltaTime;
            defaultSpeed = dashSpeed;
        }
        isDash = false;

        //대쉬

        //키 보유
        if (hasKey)
        {
            Debug.Log("키 있음");
        }
        else
        {
            Debug.Log("키 없음");
        }
        //키 보유
    }
    private void FixedUpdate() 
    {
        // 걷기
        Vector2 dir = new Vector2(h, v).normalized;
        rb.velocity = dir * defaultSpeed;

        bool isMoving = dir.sqrMagnitude > 0f;
        anim.SetBool("9_Move", isMoving);
        // 걷기
        CheckEnemy();

        if (hp <= 0)
        {
            isLive = false;
        }

        if(!isLive) StartCoroutine(Die());
    }

    // 대시 쿨타임 구현 코루틴
    IEnumerator CoolTimeCo()
    {
        if(!canDash) yield break;
        canDash = false;
        Debug.Log("대쉬 사용 불가능");
        float time = dashCooldown;
        while(time > 0)
        {
            Debug.Log($"대쉬쿨 {time}초 남음");
            yield return new WaitForSeconds(1f);
            time -= 1f;
        }
        //yield return new WaitForSeconds(dashCooldown);
        Debug.Log("대쉬 사용 가능");
        canDash = true;
    }

    // 공격타입 판단
    private void CheckEnemy()
    {
        isEnemy = Physics2D.OverlapCircle(transform.position, atkRange, enemyLayer);
        if (isEnemy) attackType = atkType.hit;
        else attackType = atkType.shoot;
    }
    // 공격 소리
    private void FireSfx()
    {
        var sfx = playerSfx.fire[(int)attackType];
        audioFx.PlayOneShot(sfx, 0.1f);

        if((int)attackType == 1)
        {
            muzzleFlash.Play();
        }
        else 
        { 
            hitFX.Play();
        }
    }

    public void GainKey()
    {
        hasKey = true;
    }

    public void TakeDamage(int d)
    {
        if (!isLive) return;
        hp -= d;
        Health.HpDown(hpFront, hp, maxHp);
        Debug.Log($"현재 체력 : {hp}");
    }

    IEnumerator Die() //죽음 
    {
        if (!isLive) yield break;
        isLive = false;

        var animC = anim;
        var gm = GameManager.instance;

        if (anim != null) anim.SetTrigger("4_Death");
        yield return new WaitForSeconds(0.5f);

        enabled = false;
        StopAllCoroutines();

        if (gm != null) gm.OnPlayerDead();

        Destroy(gameObject);
        yield break;
    }

    private void OnDestroy()
    {
        var gm = GameManager.instance;
        if (gm != null && gm.player == this) gm.player = null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + Vector3.up *0.4f, atkRange);
    }
}