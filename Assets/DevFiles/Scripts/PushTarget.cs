using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushTarget : MonoBehaviour
{
    IPushable pushable;

    private void Awake()
    {
        pushable = GetComponentInParent<IPushable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IPushable pusher))
        {
            if (pusher == pushable)
                return;

            Vector3 pos = other.transform.position;
            Vector3 _point = other.ClosestPoint(transform.position);

            pushable.Rb.AddForce(new Vector3(_point.x - pos.x, 0, _point.z - pos.z) * pusher.Force * 2);
        }
    }


}
