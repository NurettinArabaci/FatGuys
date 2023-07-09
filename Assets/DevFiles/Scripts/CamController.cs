using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] Vector3 _offset = new Vector3(0f, 2f, -10f);
    [SerializeField] float _distanceDamp = 10f;

    private Transform _mT;

    private void Awake()
    {
        _mT = transform;
    }

    private void LateUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        Vector3 toPos = new Vector3(_target.position.x, 0, _target.position.z) + _offset;
        Vector3 curPos = Vector3.MoveTowards(_mT.position, toPos, _distanceDamp * Time.deltaTime);
        _mT.position = curPos;
    }

    
}