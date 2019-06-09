using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulser : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        float size = RhythmManager.Instance.getOutput() * 5;
        transform.localScale = new Vector2(size, size);
        
    }
}
