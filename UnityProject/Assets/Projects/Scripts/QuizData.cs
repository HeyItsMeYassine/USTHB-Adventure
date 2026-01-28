using UnityEngine;

[CreateAssetMenu(fileName = "NewQuiz", menuName = "Quiz/Quiz Data")]
public class QuizData : ScriptableObject
{
    public string facultyName;
    public Question[] questions;
}

[System.Serializable]
public class Question
{
    public string questionText;
    public string[] answers;
    public int correctAnswerIndex;
}
