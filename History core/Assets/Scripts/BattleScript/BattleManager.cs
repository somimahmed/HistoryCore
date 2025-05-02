using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("Настройки боя")] public int maxHealth = 5; // Максимум HP у каждого
    public List<Question> questions = new List<Question>(); // Список вопросов

    [HideInInspector] public int playerHealth;
    [HideInInspector] public int enemyHealth;

    private UIManager uiManager;
    private Question currentQuestion;
    private int lastQuestionIndex = -1; // Индекс предыдущего вопроса

    void Start()
    {
        if (questions == null || questions.Count == 0)
        {
            Debug.LogError("BattleManager: не задан ни один вопрос!");
            return;
        }

        playerHealth = maxHealth;
        enemyHealth = maxHealth;
        uiManager = FindObjectOfType<UIManager>();

        NextQuestion();
        uiManager.UpdateHealthBars(playerHealth, enemyHealth, maxHealth);
    }

    // Вызывается из UIManager, когда игрок нажал на ответ
    public void OnPlayerAnswer(int answerIndex)
    {
        bool correct = (answerIndex == currentQuestion.correctAnswerIndex);

        if (correct)
            enemyHealth = Mathf.Max(0, enemyHealth - 1);
        else
            playerHealth = Mathf.Max(0, playerHealth - 1);

        uiManager.UpdateHealthBars(playerHealth, enemyHealth, maxHealth);

        if (playerHealth == 0 || enemyHealth == 0)
        {
            bool playerWon = (enemyHealth == 0 && playerHealth > 0);
            uiManager.ShowBattleResult(playerWon);
        }
        else
        {
            NextQuestion();
        }
    }

    private void NextQuestion()
    {
        int idx;

        if (questions.Count == 1)
        {
            // Если всего один вопрос — всегда он же
            idx = 0;
        }
        else
        {
            // Выбираем случайный индекс, отличный от lastQuestionIndex
            do
            {
                idx = Random.Range(0, questions.Count);
            } while (idx == lastQuestionIndex);
        }

        lastQuestionIndex = idx;
        currentQuestion = questions[idx];
        uiManager.DisplayQuestion(currentQuestion);
    }
}