using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    private int score = 0;
    private void OnCollisionEnter(Collision other)
    {
        IncrementScore(other);
        
    }

    private void IncrementScore(Collision other)
    {
        if (other.gameObject.name == "Ground" || other.gameObject.tag == "Hit") return;
        score++;
        Debug.Log(string.Format("You've bumped into objects {0} time(s).", score));
    }
}
