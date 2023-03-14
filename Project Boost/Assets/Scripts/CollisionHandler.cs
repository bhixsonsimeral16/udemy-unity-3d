using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{   
    [SerializeField] float reloadDelay = 1f;
    [SerializeField] float nextLevelDelay = 1f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip beatLevelSound;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;
    BoxCollider[] playerBoxColliders;

    public bool isTransitioning = false;
    public bool collisionEnabled = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerBoxColliders = GetComponents<BoxCollider>();
    }

    void Update()
    {
        DebugKeyResponse();
    }

    void OnCollisionEnter(Collision other)
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
        crashParticles.Play();
        Invoke("ReloadLevel", reloadDelay);
    }

    void StartFinishLevelSequence()
    {
        // TODO: add particle effect on success
        isTransitioning = true;
        RemovePlayerControl();
        audioSource.PlayOneShot(beatLevelSound);
        successParticles.Play();
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
    
    void DebugKeyResponse()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCollision();
        }
    }

    void ToggleCollision()
    {
        collisionEnabled = !collisionEnabled;

        foreach (BoxCollider collider in playerBoxColliders)
        {
            collider.enabled = collisionEnabled;
        }
    }
}
