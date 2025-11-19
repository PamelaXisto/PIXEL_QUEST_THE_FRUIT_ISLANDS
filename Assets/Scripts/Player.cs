using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;

    public bool isJumping;
    public bool doubleJump;

    private Animator anim;
    private Rigidbody2D rb;

    private AudioSource jumpSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        jumpSound = GameObject.Find("JumpSound").GetComponent<AudioSource>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;

        if(Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        if(Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        if(Input.GetAxis("Horizontal") == 0f)
        {
            anim.SetBool("walk", false);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if(!isJumping)
            {
                rb.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                jumpSound.Play();
            }
            else // Já está no ar
            {
                if(doubleJump)
                {
                    rb.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                    doubleJump = false;
                    jumpSound.Play();
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8) // Layer do Chão
        {
            isJumping = false;
            anim.SetBool("jump", false);
        }

        if(collision.gameObject.tag == "Spike" || collision.gameObject.tag == "Fire" || collision.gameObject.tag == "Saw")
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8) // Saiu do Chão
        {
            isJumping = true;
        }
    }

    // ---------------- FIM DE FASE -----------------
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("End"))
        {
            SceneManager.LoadScene("TelaEnd");
        }
    }
}