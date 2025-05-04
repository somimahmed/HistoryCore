using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI-Элементы")] public TextMeshProUGUI questionText;
    public Button[] answerButtons; // Кнопки остаются обычными UI Button
    public Slider playerHpBar;
    public Slider enemyHpBar;

    [Header("Панели результатов")] public GameObject winPanel; // Новая панель победы
    public GameObject losePanel; // Новая панель поражения
    public TextMeshProUGUI winText;
    public TextMeshProUGUI loseText;

    private BattleManager battleManager;

    void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
        winPanel.SetActive(false); // Скрываем обе панели при старте
        losePanel.SetActive(false);
    }

    public void DisplayQuestion(Question q)
    {
        questionText.text = q.questionText;
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < q.answers.Length)
            {
                answerButtons[i].gameObject.SetActive(true);

                // Обновляем TMP текст в кнопке
                TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                    buttonText.text = q.answers[i];

                int index = i;
                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() => OnAnswerClicked(index));
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnAnswerClicked(int index)
    {
        battleManager.OnPlayerAnswer(index);
    }

    public void UpdateHealthBars(int playerHp, int enemyHp, int maxHp)
    {
        playerHpBar.maxValue = maxHp;
        enemyHpBar.maxValue = maxHp;
        playerHpBar.value = playerHp;
        enemyHpBar.value = enemyHp;
    }

    public void ShowBattleResult(bool playerWon)
    {
        if (playerWon)
        {
            winPanel.SetActive(true);
            losePanel.SetActive(false);
            winText.text = "Вы победили!";
        }
        else
        {
            losePanel.SetActive(true);
            winPanel.SetActive(false);
            loseText.text = "Вы проиграли…";
        }
    }
}