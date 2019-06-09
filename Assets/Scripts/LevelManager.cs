using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public Level[] lvls = {};
    public Level coffeeLvl;
    private int current = 0;

    void Awake()
    {
        instance = this;
    }

    public void nextLevel()
    {
        if(current < lvls.Length)
            RhythmManager.Instance.TransitionLevels(lvls[current++]);
    }

    public Level getCurrentLevel() { return lvls[current]; }

    //switch to coffee level
    public void coffee() { RhythmManager.Instance.transitionLevels(coffeeLvl); }
    //switch back to original
    public void decoffee() { RhythmManager.Instance.transitionLevels(lvls[current]); }
}
