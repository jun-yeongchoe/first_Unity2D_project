using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Pool;
using UnityEngine.UIElements;
public class Player : MonoBehaviour 
{ 
    //�ȱ�
    [Header("ĳ���� �̵�����")]
    [SerializeField] private float moveSpeed = 5.0f; 
    private Animator anim; 
    float h; 
    float v;
    Rigidbody2D rb;
    //�ȱ�

    //ȸ��
    float angle;
    //ȸ��


    //����
    [Header("���ݰ���")]
    [SerializeField] Transform firePoint;

    private Vector3 mousePos;
    public Bullet bulletPrefab;
    private IObjectPool<Bullet> Pool;

    //����

    private void Awake() 
    { 
        rb = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();
        //Pool = new ObjectPool<Bullet>(CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet, maxSize: 10);
    }
    
    private void Update() 
    {
        // �ȱ�
        h = Input.GetAxisRaw("Horizontal"); 
        v = Input.GetAxisRaw("Vertical");
        
        // �ȱ�

        // ����
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
        //����

        //ȸ��
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
        //ȸ��

    }
    private void FixedUpdate() 
    {
        // �ȱ�
        Vector2 dir = new Vector2(h, v).normalized;
        rb.velocity = dir * moveSpeed;

        bool isMoving = dir.sqrMagnitude > 0f;
        anim.SetBool("9_Move", isMoving);
        // �ȱ�
    }

    // Bullet Ǯ��
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
    // Bullet Ǯ��
}