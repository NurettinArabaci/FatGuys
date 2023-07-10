using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Touchable _prefab;

    [SerializeField] float _radius;
    [SerializeField] float _height = 1;

    bool start = false;

    float RandomTimer()
    {
        return Random.Range(2, 8);
    }

    Vector3 RandomSpawnPoint()
    {
        float distance = Random.Range(0, _radius);
        float xAxis = Mathf.Sqrt(_radius*_radius - distance*distance);
        float yAxis = distance;

        return new Vector3(Random.Range(-xAxis, xAxis), _height, Random.Range(-yAxis, yAxis));
    }

    private void Awake()
    {
        GameStateEvent.OnPlayGame += StartGame;
    }

    IEnumerator Start()
    {
        yield return new WaitUntil(()=>start);

        float timer = RandomTimer();
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        Instantiate(_prefab, RandomSpawnPoint(), Quaternion.identity, transform);

        StartCoroutine(Start());
        

    }

    void StartGame()
    {
        start = true;
    }

    private void OnDisable()
    {
        GameStateEvent.OnPlayGame -= StartGame;
    }
}
