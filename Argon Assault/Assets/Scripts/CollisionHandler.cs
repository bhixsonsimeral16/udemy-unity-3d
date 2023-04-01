using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    // void OnCollisionEnter(Collision other)
    // {
    //     Debug.Log(this.name + " collided with " + other.gameObject.name);
    //     StartCrashSequence();
    // }

    void StartCrashSequence()
    {
        GetComponent<PlayerControls>().enabled = false;
        // GetComponent<ParticleSystem>().Play();
        Invoke("RestartLevel", loadDelay);
    }

    void RestartLevel()
    {
        // Load the current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
