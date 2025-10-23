using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Pool;
using UnityEngine.UIElements;
public class Player : MonoBehaviour 
{ 
    //걷기
    [Header("캐릭터 이동관련")]
    [SerializeField] private float moveSpeed = 5.0f; 
    private Animator anim; 
    float h; 
    float v;
    Rigidbody2D rb;
    //걷기

    //회전
    float angle;
    //회전


    //공격
    [Header("공격관련")]
    [SerializeField] Transform firePoint;

    private Vector3 mousePos;
    public Bullet bulletPrefab;
    private IObjectPool<Bullet> Pool;

    //공격

    private void Awake() 
    { 
        rb = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();
        //Pool = new ObjectPool<Bullet>(CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet, maxSize: 10);
    }
    
    private void Update() 
    {
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
           
            var go = Instantiate(bulletPrefab, firePoint.position, bAng);
            go.GetDir(atkDir);
            //var bullet = Pool.Get();
            //bulletPrefab.Shot(atkDir);
            Debug.Log(mousePos.ToString());
        }
        //공격

        //회전
        if (mousePos.x > transform.position.x) 
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (mousePos.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        //회전

    }
    private void FixedUpdate() 
    {
        // 걷기
        Vector2 dir = new Vector2(h, v).normalized;
        rb.velocity = dir * moveSpeed;

        bool isMoving = dir.sqrMagnitude > 0f;
        anim.SetBool("9_Move", isMoving);
        // 걷기
    }

    // Bullet 풀링
    //private Bullet CreateBullet()
    //{
    //    Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
    //    bullet.SetManagedPool(Pool);
    //    return bullet;
    //}

    //private void OnGetBullet(Bullet bullet)
    //{
    //    bullet.gameObject.SetActive(true);
    //}

    //private void OnReleaseBullet(Bullet bullet)
    //{
    //    bullet.gameObject.SetActive(false);
    //}

    //private void OnDestroyBullet(Bullet bullet)
    //{
    //    Destroy(bullet.gameObject);
    //}
    // Bullet 풀링
}