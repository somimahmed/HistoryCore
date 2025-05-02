using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimpleAuth : MonoBehaviour
{
    // Ссылки на UI элементы (перетяни их в инспекторе)
    public TMP_InputField nameInputField;
    public TMP_InputField passwordInputField;
    public Button closeButton;
    public GameObject panelToClose;

    private void Start()
    {
        // Подписываем метод на нажатие кнопки
        closeButton.onClick.AddListener(OnCloseButtonClicked);
    }

    private void OnCloseButtonClicked()
    {
        // Сохраняем имя в PlayerPrefs
        string playerName = nameInputField.text;
        PlayerPrefs.SetString("PlayerName", playerName);


        if (panelToClose != null)
        {
            panelToClose.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Панель для закрытия не назначена!");
        }

        // Выводим имя в консоль
        Debug.Log("Имя сохранено: " + playerName);
    }
}