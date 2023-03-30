using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(this.name + " triggered " + other.gameObject.name);
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(this.name + " collided with " + other.gameObject.name);
    }
}
