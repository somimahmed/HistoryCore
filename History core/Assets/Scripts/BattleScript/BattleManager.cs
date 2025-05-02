using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("Пехота на поле")]
    public UnitController[] playerUnits; 

    public UnitController[] enemyUnits; 

    [Header("Настройки битвы")] public List<Question> questions = new List<Question>();

    private int playerHealth; 
    private int enemyHealth; 

    private int nextPlayerIndex = 0; 
    private int nextEnemyIndex = 0;

    private UIManager uiManager;
    private Question currentQuestion;
    private int lastQuestionIndex = -1;

    void Start()
    {
        if (questions == null || questions.Count == 0)
        {
            Debug.LogError("BattleManager: не задан ни один вопрос!");
            enabled = false;
            return;
        }

       
        playerHealth = playerUnits.Length;
        enemyHealth = enemyUnits.Length;

        uiManager = FindObjectOfType<UIManager>();
        uiManager.UpdateHealthBars(playerHealth, enemyHealth, playerUnits.Length);

        NextQuestion();
    }


    public void OnPlayerAnswer(int answerIndex)
    {
        bool correct = answerIndex == currentQuestion.correctAnswerIndex;

        if (correct && enemyHealth > 0)
        {
            // Игрок бьёт: его следующий свободный юнит атакует
            playerUnits[nextPlayerIndex].Attack(enemyUnits[nextEnemyIndex]);
            enemyHealth--;
            nextEnemyIndex = Mathf.Min(nextEnemyIndex + 1, enemyUnits.Length - 1);
        }
        else if (!correct && playerHealth > 0)
        {
            // Враг «контратакует»
            enemyUnits[nextEnemyIndex].Attack(playerUnits[nextPlayerIndex]);
            playerHealth--;
            nextPlayerIndex = Mathf.Min(nextPlayerIndex + 1, playerUnits.Length - 1);
        }

        uiManager.UpdateHealthBars(playerHealth, enemyHealth, playerUnits.Length);

        // Если кто-то проиграл — конец битвы
        if (playerHealth == 0 || enemyHealth == 0)
        {
            bool playerWon = enemyHealth == 0 && playerHealth > 0;
            uiManager.ShowBattleResult(playerWon);
            return;
        }

        // Иначе — следующий вопрос
        NextQuestion();
    }

    private void NextQuestion()
    {
        int idx;
        if (questions.Count == 1)
        {
            idx = 0;
        }
        else
        {
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