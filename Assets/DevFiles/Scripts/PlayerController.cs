using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    #region Serialized Variables

    //[SerializeField] InputController _input;
    InputController _input;
    [SerializeField] Transform _target;
    [SerializeField] bool _isMine = false;
    #endregion

    #region Private Variables

    private NavMeshAgent _navmesh;
    private Transform _targetParent;
    private Rigidbody _rb;
    private List<Touchable> _nearestList = new List<Touchable>();
    #endregion

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _navmesh = GetComponent<NavMeshAgent>();
        _targetParent = _target.parent;

        _input = InputController.instance;

    }

    private void Update()
    {
        Movement();

        VelocityControl();
    }


    private void Movement()
    {
        if (_navmesh.enabled)
        {
            _navmesh.destination = _isMine ? _target.position : GetNearest();
        }

        if (_isMine)
        {
            _targetParent.rotation = Quaternion.LookRotation(new Vector3(_input.Horizontal, 0, _input.Vertical));
            transform.rotation = Quaternion.Lerp(transform.rotation, _targetParent.rotation, Time.deltaTime * 10);
        }

        if (_rb.velocity.magnitude <= 0.2f)
        {
            _navmesh.enabled = true;
        }
    }

    private void VelocityControl()
    {
        if (_rb.velocity.magnitude > 15)
        {
            _rb.velocity = _rb.velocity.normalized * 15;
        }
            
    }

    /// <summary>
    /// This function finds the closest object's position to us
    /// </summary>
    private Vector3 GetNearest()
    {
        _nearestList = FindObjectsOfType<Touchable>().ToList();
        _nearestList.Remove(this.GetComponent<Touchable>());

        Vector3 nearest = Vector3.zero;

        float dist = float.MaxValue;

        for (int i = 0; i < _nearestList.Count; i++)
        {
            if (_nearestList[i].Force > this.GetComponent<Touchable>().Force)
                continue;

            float temp = Vector3.SqrMagnitude(_nearestList[i].transform.position - transform.position);
            if (temp < dist)
            {
                dist = temp;

                nearest = _nearestList[i].transform.position;

            }
        }

        return nearest;
    }

}
