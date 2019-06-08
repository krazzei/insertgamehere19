using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DestroyOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}
