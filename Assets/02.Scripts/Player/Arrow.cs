using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("¼³Á¤")]
    [SerializeField] Transform player;
    [SerializeField] Transform Object;

    private void Update()
    {
        transform.right = (Object.position - transform.position).normalized;
    }
    private void LateUpdate()
    {
        Vector3 targetPos = new Vector3(player.position.x, player.position.y - 0.5f, this.transform.position.z);
        transform.position = targetPos;
    }
}
