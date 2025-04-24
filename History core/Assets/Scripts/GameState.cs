using UnityEngine;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }
    private int quizScore;
    private int maxPossibleScore;

    public int QuizScore => quizScore;
    public int MaxScore => maxPossibleScore;

    private Dictionary<string, bool> flags = new Dictionary<string, bool>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void AddScore(int value) {
        quizScore = Mathf.Clamp(quizScore + value, 0, maxPossibleScore);
    }

    public void SetMaxScore(int value) {
        maxPossibleScore = value;
    }

    public void SetFlag(string key, bool value) => flags[key] = value;

    public bool CheckFlag(string key)
    {
        return flags.TryGetValue(key, out bool value) && value;
    }
}