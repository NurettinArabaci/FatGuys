using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset = new Vector3(0f, 2f, -10f);
    [SerializeField] float distanceDamp = 10f;

    Transform mT;

    void Awake()
    {
        mT = transform;
    }


    void LateUpdate()
    {
        Vector3 toPos = new Vector3(target.position.x,0,target.position.z) + offset;
        Vector3 curPos = Vector3.MoveTowards(mT.position, toPos, distanceDamp * Time.deltaTime);
        mT.position = curPos;
    }
}