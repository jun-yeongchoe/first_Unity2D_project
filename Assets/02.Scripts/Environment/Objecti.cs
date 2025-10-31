using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

public class Objecti : MonoBehaviour
{
    public Rigidbody2D target;
    private Animator anim;
    NavMeshAgent agent;
    private bool isMoving;

    private bool detectTarget;
    private float detectRange = 1.5f;
    [SerializeField] LayerMask player;

    bool isRescued;
    [SerializeField] private ParticleSystem love;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = true;
        agent.updateUpAxis = false;
        anim = GetComponent<Animator>();
        
    }
    private void Update()
    {
        Vector2 center = transform.position + Vector3.up * 0.4f;
        detectTarget = Physics2D.OverlapCircle(center, detectRange, player);

        if(detectTarget)
        {
            if (!isRescued)
            {
                isRescued = true;
                var fx = Instantiate(love, transform.position, Quaternion.identity);
                fx.Play();
            }
            Player.withObject = true;
            agent.isStopped = false;
            isMoving = true;
            anim.SetBool("9_Move", isMoving);
            agent.SetDestination(target.position);
        }
        else
        {
            Player.withObject = false;
            isMoving = false;
            anim.SetBool("9_Move", isMoving);
            agent.isStopped = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position + Vector3.up * 0.4f, detectRange);
    }
}
