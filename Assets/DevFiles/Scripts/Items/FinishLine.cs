using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FinishLine : MonoBehaviour
{
    List<PlayerController> _touchables = new List<PlayerController>();

    int _playerCount = 0;

    PlayerController _playerController;

    private void Awake()
    {
        _touchables = FindObjectsOfType<PlayerController>().ToList();
        _playerCount = _touchables.Count;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out Touchable touchable))
        {
            touchable.OnDie();

            if (!touchable.GetComponent<PlayerController>()) return;

            _playerController = touchable.GetComponent<PlayerController>();
            _touchables.Remove(_playerController);
            _playerCount--;

            Debug.Log(_playerCount);

            if (_playerCount > 1) return;

            if (_touchables[0].IsMine)
            {
                GameStateEvent.Fire_OnChangeGameState(GameState.Win);
            }

            
        }
    }
}
