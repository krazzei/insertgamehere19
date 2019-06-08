using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    private Transform _transform;
    private Vector3 _targetPos;
    
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _targetPos = _target.position;
        _targetPos.z = -10;
        _transform.position = _targetPos;
    }
}