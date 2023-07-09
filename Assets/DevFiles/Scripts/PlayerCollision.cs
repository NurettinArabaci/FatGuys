using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCollision : Touchable
{
    [SerializeField] private int force = 1000;

    public override int Force { get => force; set => force = value; }


    private void OnCollisionEnter(Collision coll)
    {
        if (coll.transform.TryGetComponent(out Touchable touchable))
        {
            touchable.OnTouch();
            OnPushed(coll);
            touchable.Collected(this);
        }

    }

    public override void OnTouch()
    {
        GetComponent<NavMeshAgent>().enabled = false;


    }

    public void OnPushed(Collision coll)
    {
        coll.transform.GetComponent<Rigidbody>().AddForce(PushPoint(coll) * force);
    }

    Vector3 PushPoint(Collision coll)
    {
        Vector3 pos = coll.transform.position;
        return new Vector3(pos.x - coll.GetContact(0).point.x, 0, pos.z - coll.GetContact(0).point.z);
    }
}
