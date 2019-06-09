using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Level[] lvls = {
        
    };
    private int current = 0;

    void Awake()
    {
        instance = this;
    }

    public static void nextLevel() { instance.nextLevelPrivate(); }
    private void nextLevelPrivate()
    {
        if(current < lvls.Length)
            RhythmManager.transitionLevels(lvls[current++]);
    }
}
