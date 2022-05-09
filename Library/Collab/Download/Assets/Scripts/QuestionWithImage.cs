using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Question", menuName = "QuestionWithImage")]
public class QuestionWithImage : ScriptableObject
{
    public string questionName, correction;
    public Sprite[] wrongAnswers = new Sprite[3];
    public Sprite correctAnswer;

    [HideInInspector]
    public int correctAnswerValue;

}
