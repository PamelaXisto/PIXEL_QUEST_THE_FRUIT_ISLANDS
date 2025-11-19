using UnityEngine;

public class Saw : MonoBehaviour
{
    public float speed;
    public float moveTime;

    private bool dirRight = true;
    private float timer;

    void Update()
    {
        if(dirRight)
        {
            // verdadeiro = serra p/ direita
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            // falso = serra p/ esquerda
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        timer += Time.deltaTime;
        if(timer >= moveTime)
        {
            dirRight = !dirRight;
            timer = 0f;
        }
    }
}
