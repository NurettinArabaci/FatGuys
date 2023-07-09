using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : Touchable, IPushable
{
    [SerializeField] private int force = 1000;

    public override int Force { get => force; set => force = value; }


    private void OnCollisionEnter(Collision coll)
    {
        if (coll.transform.TryGetComponent(out ICollectable collectable))
        {
            collectable.Collected(this);
            OnCollected();
        }

        if(coll.transform.TryGetComponent(out IPushable pusher))
        {
            OnPush(coll, pusher.Force);
        }

    }

    void OnCollected()
    {
        Rb.mass += 0.1f;
        transform.localScale += Vector3.one / 5;
        Force += 200;
    }


    Vector3 PushPoint(Collision coll)
    {
        Vector3 pos = coll.transform.position;
        Vector3 _point = coll.GetContact(0).point;
        return new Vector3(_point.x - pos.x, 0, _point.z - pos.z);
    }



    public void OnPush(Collision coll,int force)
    {
        Rb.AddForce(PushPoint(coll) * force);
    }
}
