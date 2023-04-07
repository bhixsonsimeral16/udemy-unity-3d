using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] int scorePerHit = 10;
    [SerializeField] float maxHitPoints = 100f;
    [SerializeField] float currentHitPoints = 100f;
    ScoreBoard scoreBoard;
    Renderer renderer;
    GameObject parentGameObject;
    void Start()
    {
        renderer = GetComponent<Renderer>();
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidBody();
    }

    void AddRigidBody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (gameObject.tag == "BlankEnemy")
        {
            ChangeColor();
        }
        if (currentHitPoints <= 0)
        {
            KillEnemy();
        }
    }

    void ProcessHit()
    {
        currentHitPoints -= 10;
        scoreBoard.IncreaseScore(scorePerHit);
    }

    void ChangeColor()
    {
        float healthPercentage = currentHitPoints / maxHitPoints;
        Color newColor = Color.Lerp(Color.white, Color.red, 1 - healthPercentage);
        renderer.material.color = newColor;
    }

    void KillEnemy()
    {
        GameObject vfx = Instantiate(deathFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }
}
