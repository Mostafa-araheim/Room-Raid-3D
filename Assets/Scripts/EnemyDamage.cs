using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private bool randomDamage = false; // Toggle random damage
    [SerializeField] private float damageSet = 25f; // Fixed damage value
    [SerializeField] private float minDamage = 10f; // Random damage min
    [SerializeField] private float maxDamage = 30f; // Random damage max

    [SerializeField] private AudioClip[] sounds; // Audio clips for attack
    private GameObject player; // Reference to player
    private Animator animator; // Optional: for attack animations

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Consistent player reference
        animator = GetComponent<Animator>(); // Optional: for animations
    }

    // Called by EnemyAiTutorial to deal damage to player
    public void DealDamageToPlayer()
    {
        // Calculate damage
        float damage = randomDamage ? Random.Range(minDamage, maxDamage) : damageSet;

        // Apply damage to player
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.health -= damage;

            // Play random sound
            if (sounds.Length > 0)
            {
                AudioSource.PlayClipAtPoint(sounds[Random.Range(0, sounds.Length)], transform.position);
            }

            // Optional: Play attack animation
            if (animator != null)
                animator.SetTrigger("Attack");
        }
    }
}