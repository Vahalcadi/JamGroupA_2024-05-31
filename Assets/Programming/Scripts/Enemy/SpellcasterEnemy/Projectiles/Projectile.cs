using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float despawnTime;
    private float timer;
    public int Damage { get; set; }
    private void Start()
    {
        timer = despawnTime;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        rb.velocity = transform.forward * speed;

        if (timer < 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (!player.IsInvincible)
            {
                player.TakeDamage(Damage);
                Destroy(gameObject);
            }
        }
    }
}
