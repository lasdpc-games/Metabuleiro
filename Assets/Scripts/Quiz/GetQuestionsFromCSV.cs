using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetQuestionsFromCSV : MonoBehaviour {
    public TextAsset allQuestions;
    public List<QuestionCSV> questions = new List<QuestionCSV> ();

    public QuestionCSV[] GetQuestions(){
        List<string> questionData = new List<string> ();
        questionData = allQuestions.text.Split (new char[] { '\n' }).ToList<string> ();
        for (int i = 1; i < questionData.Count - 1; i++) {
            string[] row = questionData[i].Split (new char[] { ';' });
            QuestionCSV q = ScriptableObject.CreateInstance<QuestionCSV> ();
            q.questionName = row[0];
            q.correctAnswer = row[1];
            q.wrongAnswer1 = row[2];
            q.wrongAnswer2 = row[3];
            q.wrongAnswer3 = row[4];
            int.TryParse (row[5],out q.difficulty);
            questions.Add (q);
        }
        return (questions.ToArray<QuestionCSV> ());
    }
}