using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetQuestionsFromCSV : MonoBehaviour {
    public TextAsset easyQuestions;
    public TextAsset mediumQuestions;
    public TextAsset hardQuestions;
    public List<QuestionCSV> questions = new List<QuestionCSV> ();
    public TextAsset easyQuestionsWithImages;
    public TextAsset mediumQuestionsWithImages;
    public TextAsset hardQuestionsWithImages;
    List<QuestionCSVWithImages> questionsWithImages = new List<QuestionCSVWithImages> ();

    public QuestionCSV[] GetQuestions (int levelChosen) {
        List<string> questionData = new List<string> ();
        switch (levelChosen) {
            case 1:
                questionData = easyQuestions.text.Split (new char[] { '\n' }).ToList<string> ();
                break;
            case 2:
                questionData = mediumQuestions.text.Split (new char[] { '\n' }).ToList<string> ();
                break;
            case 3:
                questionData = hardQuestions.text.Split (new char[] { '\n' }).ToList<string> ();
                break;
            case 4:
                List<string> tempQuestion1 = (easyQuestions.text.Split (new char[] { '\n' }).ToList<string> ());
                List<string> tempQuestion2 = (mediumQuestions.text.Split (new char[] { '\n' }).ToList<string> ());
                List<string> tempQuestion3 = (hardQuestions.text.Split (new char[] { '\n' }).ToList<string> ());
                for (int i = 0; i < tempQuestion1.Count - 1; i++) {
                    questionData.Add (tempQuestion1[i]);
                }
                for (int i = 1; i < tempQuestion2.Count - 1; i++) {
                    questionData.Add (tempQuestion2[i]);
                }
                for (int i = 1; i < tempQuestion3.Count-1; i++) {
                    questionData.Add (tempQuestion3[i]);
                }
                break;
        }
        for (int i = 1; i < questionData.Count - 1; i++) {
            string[] row = questionData[i].Split (new char[] { ';' });
            QuestionCSV q = ScriptableObject.CreateInstance<QuestionCSV> ();
            q.questionName = row[0];
            q.correctAnswer = row[1];
            q.wrongAnswer1 = row[2];
            q.wrongAnswer2 = row[3];
            q.wrongAnswer3 = row[4];
            q.correction = row[5];
            int.TryParse (row[6], out q.correctAnswerValue);
            questions.Add (q);
        }
        return (questions.ToArray<QuestionCSV> ());
    }
    public QuestionCSVWithImages[] GetQuestionsWithImages (int levelChosen) {
        List<string> questionData = new List<string> ();
        switch (levelChosen) {
            case 1:
                questionData = easyQuestionsWithImages.text.Split (new char[] { '\n' }).ToList<string> ();
                break;
            case 2:
                questionData = mediumQuestionsWithImages.text.Split (new char[] { '\n' }).ToList<string> ();
                break;
            case 3:
                questionData = hardQuestionsWithImages.text.Split (new char[] { '\n' }).ToList<string> ();
                break;
        }
        for (int i = 1; i < questionData.Count - 1; i++) {
            string[] row = questionData[i].Split (new char[] { ';' });
            //QuestionCSV q = new QuestionCSV ();
            QuestionCSVWithImages q = ScriptableObject.CreateInstance<QuestionCSVWithImages> ();
            q.questionName = row[0];
            int.TryParse (row[1], out q.correctImageID);
            int.TryParse (row[2], out q.correctionImageID);
            int.TryParse (row[3], out q.correctAnswerValue);
            questionsWithImages.Add (q);
        }
        return (questionsWithImages.ToArray<QuestionCSVWithImages> ());
    }
}