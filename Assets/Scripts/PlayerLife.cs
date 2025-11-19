using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public int maxLife = 3;
    public int currentLife;

    public Animator anim;
    public Rigidbody2D rb;
    public Collider2D playerCollider;

    private bool isDead = false;
    private bool isInvulnerable = false;

    void Start()
    {
        currentLife = maxLife;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Damager"))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead || isInvulnerable) return;

        StartCoroutine(InvulnerabilityTime());

        currentLife -= amount;

        GameController.instance.LoseLife();

        anim.SetTrigger("hit");

        if (currentLife <= 0)
        {
            Die();
        }
    }

    System.Collections.IEnumerator InvulnerabilityTime()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(0.5f);
        isInvulnerable = false;
    }

    void Die()
    {
        isDead = true;

        anim.SetTrigger("die");
        rb.bodyType = RigidbodyType2D.Kinematic;
        playerCollider.enabled = false;

        Destroy(gameObject, 0.5f);
    }
}
