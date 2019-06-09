using System.Collections;
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

    [SerializeField]
    private Image _flash;

    private Color _color;
   
    // Start is called before the first frame update
    void Start()
    {
        RhythmManager.Instance.OnBeat += OnBeat;
        GameManager.OnShipDeath += GameManagerOnOnShipDeath;
    }

    private void GameManagerOnOnShipDeath()
    {
        Destroy(this);
    }

    private void OnDestroy()
    {
        RhythmManager.Instance.OnBeat -= OnBeat;
        GameManager.OnShipDeath -= GameManagerOnOnShipDeath;
    }

    private void OnBeat()
    {
        //StartCoroutine(BeatRoutine());
         _color = _flash.color;
        _color.a = _color.a > 0.5f ? 0 : 1f;
        _flash.color = _color;
    }

    private IEnumerator BeatRoutine()
    {
        const float duration = 0.1f;
        var time = duration;
        var color = _flash.color;
        color.a = 1;
        _flash.color = color;
        
//        while (time > 0)
//        {
//            color.a = Mathf.Lerp(1, 0, time / duration);
//            _flash.color = color;
//
//            yield return null;
//            time -= Time.deltaTime;
//        }

        yield return null;
        yield return null;
        yield return null;

        color.a = 0;
        _flash.color = color;
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

    void SetMarkerPos()
    {
        progress = GetShipPos() / GetLevelLength();
        marker.position = Vector2.Lerp(barStart.position, barEnd.position, progress);
        //Debug.Log(progress);
    }
}
