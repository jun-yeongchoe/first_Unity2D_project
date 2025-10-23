using UnityEngine;
public class Player : MonoBehaviour 
{ 
    [Header("Ä³¸¯ÅÍ ÀÌµ¿°ü·Ã")]
    [SerializeField] private float moveSpeed = 5.0f; 
    private Animator anim; 
    float h; 
    float v;
    Rigidbody2D rb;
    private void Awake() 
    { 
        rb = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();
    }
    private void Update() 
    {
        // °È±â
        h = Input.GetAxisRaw("Horizontal"); 
        v = Input.GetAxisRaw("Vertical");
        if (h < 0) 
        { 
            transform.rotation = Quaternion.Euler(0, 0, 0); 
        } 
        if (h > 0) 
        { 
            transform.rotation = Quaternion.Euler(0, 180, 0); 
        }
        // °È±â
    }
    private void FixedUpdate() 
    {
        // °È±â
        Vector2 dir = new Vector2(h, v).normalized;
        rb.velocity = dir * moveSpeed;

        bool isMoving = dir.sqrMagnitude > 0f;
        Debug.Log(isMoving);
        anim.SetBool("9_Move", isMoving);
        // °È±â
    }
}