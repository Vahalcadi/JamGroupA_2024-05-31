using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    public int Damage { get; set; }
    private void Update()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if(!player.IsInvincible)
                player.TakeDamage(Damage);
        }
        if (!other.CompareTag("Enemy"))
            Destroy(gameObject);
    }
}
