using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    #region Serialized Variables

    [SerializeField] float _speed = 1;
    [SerializeField] Transform _target;
    [SerializeField] bool _isMine = false;
    #endregion

    #region Private Variables

    private InputController _input;
    private NavMeshAgent _navmesh;
    private Transform _targetParent;
    private Rigidbody _rb;
    private List<Touchable> _nearestList = new List<Touchable>();
    #endregion

    #region Properties

    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }
    #endregion

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _navmesh = GetComponent<NavMeshAgent>();
        _targetParent = _target.parent;
    }

    private void Start()
    {
        _input = InputController.instance;

        _target.gameObject.SetActive(_isMine);
    }


    private void FixedUpdate()
    {
        if (_isMine)
        {
            _rb.position = Vector3.MoveTowards(_rb.position, _target.position, Time.fixedDeltaTime * _speed);
            _targetParent.rotation = Quaternion.LookRotation(new Vector3(_input.Horizontal, 0, _input.Vertical));
            _rb.rotation = Quaternion.Lerp(transform.rotation, _targetParent.rotation, Time.fixedDeltaTime * 10);
        }

        else
        {
            _rb.position = Vector3.MoveTowards(_rb.position, GetNearest(), Time.fixedDeltaTime * _speed);

            _rb.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(GetNearest() - transform.position), Time.fixedDeltaTime * 4);

        }

        VelocityControl();
    }

    private void MovementAI()
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
            if (_nearestList[i].Force >= this.GetComponent<Touchable>().Force)
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
