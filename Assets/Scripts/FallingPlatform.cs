using UnityEngine;

public class Falling : MonoBehaviour
{
    // Plataforma irá cair
    public float fallingTime;

    private TargetJoint2D target;
    private BoxCollider2D boxColl;

    void Start()
    {
        target = GetComponent<TargetJoint2D>();
        boxColl = GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Invoke("DoFalling", fallingTime);
        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }
    }

    void DoFalling()
    {
        target.enabled = false;
        boxColl.isTrigger = true;
    }
}
