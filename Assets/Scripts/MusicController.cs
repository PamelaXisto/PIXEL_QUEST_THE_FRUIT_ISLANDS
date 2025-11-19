using UnityEngine;

public class MusicController : MonoBehaviour
{
    private static MusicController musicControl;

    private void Awake()
    {
        if(musicControl == null)
        {
            musicControl = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(musicControl != this)
        {
            Destroy(gameObject);
        }
    }
}
