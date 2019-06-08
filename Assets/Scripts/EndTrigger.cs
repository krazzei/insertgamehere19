using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EndTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject _postGameScreen;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var ship = other.gameObject.GetComponent<Ship>();
        if (ship != null)
        {
            GameManager.FireWinTriggerHit();
            Instantiate(_postGameScreen);
        }
    }
}
