using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        StopAllCoroutines();
        StartCoroutine(audioManager.PlayBGM("Shining Paper Plane"));
    }

    public void PlayGame()
    {
        Debug.Log("Game start!");
        StopAllCoroutines();
        StartCoroutine(audioManager.StopBGM());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        // Todo Add "Are you sure you want to quit?"
        Debug.Log("Successfully quit!");
        Application.Quit();
    }
}
