using UnityEngine;
using UnityEngine.SceneManagement;

public class EndVitoria : MonoBehaviour
{
    [SerializeField] private string Menu;

    public void VoltarMenu()
    {
        SceneManager.LoadScene(Menu);
    }
}
