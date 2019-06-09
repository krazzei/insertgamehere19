using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FC_Roasted : MonoBehaviour
{
    [SerializeField]
    AudioClip ohYeah;

    private bool activated = false;

    // Start is called before the first frame update
    void Start()
    {
        //RhythmManager.Instance.subToBeat(gameObject); //get "onBeat()" calls from RhythmManager

    }

    /*public void onBeat()
    {
        if (activated)
        { //only on 1st frame of activation
            //print("YEUH");
            if(RhythmManager.Instance.musicPlayer.isPlaying)
        }
    }*/


    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(restoreMusicAfterCoffeeDone());

        //RhythmManager.Instance.musicPlayer.loop = false; //stop when done, onBeat() will restore to original level before picking up coffee
    }

    IEnumerator restoreMusicAfterCoffeeDone()
    {
        print("just called");
        activated = true;
        GetComponent<Collider2D>().enabled = false; //can't touch anymore
        LevelManager.instance.coffee();

        yield return new WaitForSeconds(LevelManager.instance.coffeeLvl.music.length + RhythmManager.Instance.Spb * 4);

        LevelManager.instance.decoffee();
        print("exited");

        StopCoroutine(restoreMusicAfterCoffeeDone());
    }
}
