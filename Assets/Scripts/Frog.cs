using UnityEngine;

public class Frog : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;
    private bool colliding;

    [Header("Movimento")]
    public float speed;
    public Transform rightCol;
    public Transform leftCol;

    [Header("Detecção do Player")]
    public Transform headPoint;
    public LayerMask layer;

    [Header("Colisores")]
    public BoxCollider2D boxCollider2D;
    public CircleCollider2D circleCollider2D;

    [Header("Áudio")]
    public AudioSource audioSource;
    public AudioClip monsterFrogSound;

    [Header("Recoil")]
    public float lateralPushForce = 2f;
    public float lateralPushUp = 0.5f; 

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (rig == null) return;

        rig.linearVelocity = new Vector2(speed, rig.linearVelocity.y);

        if (rightCol != null && leftCol != null)
        {
            colliding = Physics2D.Linecast(rightCol.position, leftCol.position, layer);
        }

        if (colliding)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed *= -1f;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = col.gameObject.GetComponent<Rigidbody2D>();
            PlayerLife life = col.gameObject.GetComponent<PlayerLife>();

            float height = col.contacts[0].point.y - headPoint.position.y;

            if(height > 0.1f && playerRb != null && playerRb.linearVelocity.y < -0.1f)
            {
                // Toca som do Frog
                if(audioSource != null && monsterFrogSound != null)
                    audioSource.PlayOneShot(monsterFrogSound);

                // Impulsiona Player para cima
                playerRb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

                // Mata o Frog
                speed = 0;
                if(anim != null) anim.SetTrigger("die");
                if(boxCollider2D != null) boxCollider2D.enabled = false;
                if(circleCollider2D != null) circleCollider2D.enabled = false;
                if(rig != null) rig.bodyType = RigidbodyType2D.Kinematic;

                Destroy(gameObject, 0.33f);
            }
            else
            {
                if(life != null)
                    life.TakeDamage(1);

                if(playerRb != null)
                {
                    float directionX = col.transform.position.x < transform.position.x ? -1 : 1;
                    Vector2 pushDirection = new Vector2(directionX, lateralPushUp);
                    playerRb.AddForce(pushDirection * lateralPushForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}
