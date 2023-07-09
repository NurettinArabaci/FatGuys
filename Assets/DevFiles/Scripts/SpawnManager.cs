using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Touchable prefab;

    bool start = false;

    float timer;
    IEnumerator Start()
    {
        Debug.Log("start false");
        yield return new WaitForSeconds(2);
        start = true;
        Debug.Log("start true");
        yield return new WaitUntil(()=>start);
        timer = Random.Range(1, 5);
        while (timer > 0)
        {
            timer -= Time.deltaTime;


            yield return null;
        }
        Debug.Log("Naber");
        StartCoroutine(Start());
        

    }
    /*
    IEnumerator RandomTimeInstantiate()
    {
        
        StartCoroutine(RandomTimeInstantiate());
    }*/
}
