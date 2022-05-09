using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizWithImageHelper : MonoBehaviour
{
    public Sprite[] newSprites;
    public Image[] answerImages;

    void Start()
    {
            for (int i = 0; i <= 3; i++)
            {
                answerImages[i].sprite = newSprites[i];
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
