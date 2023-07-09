using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CollisionController : Touchable
{
    [SerializeField] private int force = 1000;

    public override int Force { get => force; set => force = value; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Touchable touchable))
        {
            touchable.OnTouch();
            collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(collision.transform.position.x - collision.GetContact(0).point.x, 0, collision.transform.position.z - collision.GetContact(0).point.z) * force);
            touchable.Collected(this);
        }

    }

    public override void OnTouch()
    {
        GetComponent<NavMeshAgent>().enabled = false;

    }
}
