using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FC_UI : MonoBehaviour
{
    [SerializeField]
    Text timer;

    [SerializeField]
    Transform levelStart;
    [SerializeField]
    Transform levelEnd;
    [SerializeField]
    Transform barStart;
    [SerializeField]
    Transform barEnd;
    [SerializeField]
    Transform ship;
    [SerializeField]
    Transform marker;
    float levelLength;
    float barLength;
    float shipToEnd;
    float progress;
    float markerToEnd;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
        SetMarkerPos();
    }

    void UpdateTime()
    {
        timer.text = GameManager.GetTime().ToString("F0");
    }

    float GetLevelLength()
    {
        levelLength = Vector2.Distance(levelStart.position, levelEnd.position);
        return levelLength;
    }

    float GetBarLength()
    {
        barLength = Vector2.Distance(barStart.position, barEnd.position);
        return barLength;
    }

    float GetShipPos()
    {
        shipToEnd = Vector2.Distance(ship.position, levelStart.position);
        return shipToEnd;
    }

    float GetMarkerPos()
    {
        markerToEnd = Vector2.Distance(marker.position, barStart.position);
        return markerToEnd;
    }

    void SetMarkerPos()
    {
        progress = GetShipPos() / GetLevelLength();
        marker.position = Vector2.Lerp(barStart.position, barEnd.position, progress);
        Debug.Log(progress);
    }
}
