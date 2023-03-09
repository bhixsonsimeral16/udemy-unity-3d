using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{   
    [SerializeField] float reloadDelay = 1f;
    [SerializeField] float nextLevelDelay = 1f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip beatLevelSound;

    AudioSource audioSource;

    public bool isTransitioning = false;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) return;
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("That was a friendly little tap.");
                break;
            case "Finish":
                Debug.Log("You won?!?!?");
                StartFinishLevelSequence();
                break;
            default:
                Debug.Log("Death...");
                StartCrashSequence();
                break;
        }
    }

    void RemovePlayerControl()
    {
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
    }

    void StartCrashSequence()
    {   
        // TODO: add particle effect on crash
        isTransitioning = true;
        RemovePlayerControl();
        audioSource.PlayOneShot(crashSound);
        Invoke("ReloadLevel", reloadDelay);
    }

    void StartFinishLevelSequence()
    {
        // TODO: add particle effect on success
        isTransitioning = true;
        RemovePlayerControl();
        audioSource.PlayOneShot(beatLevelSound);
        Invoke("LoadNextLevel", nextLevelDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;
        if (currentSceneIndex == totalScenes - 1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        
    }
}
