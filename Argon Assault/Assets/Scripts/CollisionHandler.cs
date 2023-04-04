using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem deathFX;
    void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    void StartCrashSequence()
    {
        deathFX.Play();
        GetComponent<PlayerControls>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        Invoke("RestartLevel", loadDelay);
    }

    void RestartLevel()
    {
        // Load the current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
