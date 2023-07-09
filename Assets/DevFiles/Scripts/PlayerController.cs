using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputController input;

    [SerializeField] bool isMine = false;

    [SerializeField] Transform target;
    Transform targetParent;

    private Rigidbody rb;
    public Rigidbody Rb => rb;

    NavMeshAgent navmesh;

    [SerializeField] List<Touchable> nearests = new List<Touchable>();

    public bool move = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        navmesh = GetComponent<NavMeshAgent>();
        targetParent = target.parent;

    }

    void Update()
    {
        if (navmesh.enabled)
        {
            navmesh.destination = isMine ? target.position : GetNearest();
        }
            

        if (input.isMove)
        {
            targetParent.rotation = Quaternion.LookRotation(new Vector3(input.Horizontal, 0, input.Vertical));
            transform.rotation = Quaternion.Lerp(transform.rotation, targetParent.rotation, Time.deltaTime * 10);
        }

        if (rb.velocity.magnitude <= 0.25f)
            navmesh.enabled = true;

    }

    


    public Vector3 GetNearest()
    {
        nearests = FindObjectsOfType<Touchable>().ToList();

        Vector3 nearest = Vector3.zero;

        float dist = float.MaxValue;

        for (int i = 0; i < nearests.Count; i++)
        {

            if (nearests[i] == this.GetComponent<Touchable>())
                continue;

            if (nearests[i].Force > this.GetComponent<Touchable>().Force)
                continue;

            float temp = Vector3.SqrMagnitude(nearests[i].transform.position - transform.position);
            if (temp < dist)
            {
                dist = temp;


                nearest = nearests[i].transform.position;

            }
        }


        return nearest;
    }

}
