using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Serialized Variables

    [SerializeField] bool _isMine = false;
    [SerializeField] float _speed = 1;
    [SerializeField] Transform _target;
    [SerializeField] GameObject _goldCrown;
    #endregion

    #region Private Variables

    private InputController _input;
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
        _targetParent = _target.parent;
    }

    private void Start()
    {
        _input = InputController.Instance;

        _target.gameObject.SetActive(_isMine);
    }


    private void FixedUpdate()
    {
        VelocityControl();

        Movement();

        
    }

    private void Movement()
    {
        if (_isMine)
        {
            _rb.position = Vector3.MoveTowards(_rb.position, _target.position, Time.fixedDeltaTime * _speed);
            _targetParent.rotation = Quaternion.LookRotation(new Vector3(_input.Horizontal, 0, _input.Vertical));
            _rb.rotation = Quaternion.Lerp(transform.rotation, _targetParent.rotation, Time.fixedDeltaTime * 10);
            return;
        }

        _rb.position = Vector3.MoveTowards(_rb.position, GetNearest(), Time.fixedDeltaTime * _speed);
        _rb.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(GetNearest() - transform.position), Time.fixedDeltaTime * 4);
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
