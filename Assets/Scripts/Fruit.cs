using UnityEngine;

public class Fruit : MonoBehaviour
{
    private SpriteRenderer sr;
    private CircleCollider2D circle;

    public GameObject collected;
    public int Score;
    
    private AudioSource collectSound; 

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
        
        GameObject soundObject = GameObject.Find("CollectedSound");
        if (soundObject != null)
        {
            collectSound = soundObject.GetComponent<AudioSource>();
        }
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {

            if (collectSound != null)
            {

                collectSound.PlayOneShot(collectSound.clip);
            }

            sr.enabled = false;
            circle.enabled = false;
            collected.SetActive(true);

            GameController.instance.totalScore += Score;
            GameController.instance.UpdateScoreText();

            Destroy(gameObject, 0.5f); 
        }
    }
}
