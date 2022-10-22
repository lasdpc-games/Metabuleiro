using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject bubblePrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Loop");  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Loop()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(bubblePrefab);
        StartCoroutine("Loop");
    }
}
