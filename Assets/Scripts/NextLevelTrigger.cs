using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Ship>() != null) {
            LevelManager.instance.nextLevel();
            GetComponent<Collider2D>().enabled = false;
        }
            
        
    }
}
