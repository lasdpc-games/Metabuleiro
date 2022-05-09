using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    float size;
    public RectTransform myRectTransform;

    // Start is called before the first frame update
    void Start()
    {

        this.transform.SetParent(GameObject.Find("BubbleSpawn").transform);

        myRectTransform.localPosition = new Vector3(Random.Range(-200, 200), -400, 0);

        if (this.tag == "Bubble")
        {
            size = Random.Range(0.5f, 1);
        }else if (this.tag == "BubbleWithBlur")
        {
            size = Random.Range(0.1f, 0.5f);
        }
        transform.localScale = new Vector3(size, size,size);
        StartCoroutine("Destroyer");
    }

    // Update is called once per frame
    void Update()
    {
        myRectTransform.localPosition += Vector3.up;

    }

   public void Clicked()
    {
        Destroy(this.gameObject);
    }

    IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(14f);
        Destroy(this.gameObject);
    }
}
