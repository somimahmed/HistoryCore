// DialogueManager.cs

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")] [SerializeField]
    private TextMeshProUGUI dialogueText;

    [SerializeField] private TextMeshProUGUI speakerName;
    [SerializeField] private Image speakerPortrait;
    [SerializeField] private Transform buttonsParent;
    [SerializeField] private GameObject responseButtonPrefab;
    private Coroutine typingCoroutine;

    [Header("Settings")] [SerializeField] private float textSpeed = 0.05f;

    private DialogueNode currentNode;
    private bool isTyping = false;
    public static DialogueManager Instance { get; private set; }

    private void Awake()
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

    public void StartDialogue(DialogueNode startNode)
    {
        currentNode = startNode;
        gameObject.SetActive(true);
        DisplayNode(currentNode);
    }

    private void DisplayNode(DialogueNode node)
    {
        ClearUI();

        // Остановка предыдущей анимации
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        // Настройка информации о говорящем
        node.onNodeEnter?.Invoke();
        speakerName.text = node.speaker?.characterName ?? "";
        speakerPortrait.sprite = node.speaker?.portrait;

        // Запуск новой анимации текста
        typingCoroutine = StartCoroutine(TypeText(node.text));

        // Создание кнопок ответов
        foreach (var response in node.responses)
        {
            if (CheckConditions(response.conditions))
                CreateResponseButton(response);
        }
    }

    private bool CheckConditions(GameCondition[] conditions)
    {
        return conditions.All(condition =>
            GameState.Instance.CheckFlag(condition.flagKey) == condition.requiredValue);
    }

    private void CreateResponseButton(DialogueResponse response)
    {
        var buttonObj = Instantiate(responseButtonPrefab, buttonsParent);

        // Принудительное обновление макета
        LayoutRebuilder.ForceRebuildLayoutImmediate(buttonsParent.GetComponent<RectTransform>());

        // Настройка текста
        var text = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
        text.text = response.text;

        // Настройка кнопки
        var button = buttonObj.GetComponent<Button>();
        button.onClick.AddListener(() => OnResponseSelected(response));

        // Явное обновление размера
        buttonObj.GetComponent<ContentSizeFitter>().SetLayoutVertical();
    }

    private void OnResponseSelected(DialogueResponse response)
    {
        ApplyEffects(response.effects);

        if (response.nextNode != null)
        {
            currentNode = response.nextNode;
            DisplayNode(currentNode);
        }
        else
        {
            EndDialogue();
        }
    }

    private void ApplyEffects(GameEffect[] effects)
    {
        foreach (var effect in effects)
        {
            switch (effect.effectType)
            {
                case GameEffect.EffectType.SetFlag:
                    GameState.Instance.SetFlag(effect.flagKey, effect.value);
                    break;
                case GameEffect.EffectType.AddScore:
                    GameState.Instance.AddScore(effect.scoreValue);
                    break;
                // В методе ApplyEffects добавьте:
                case GameEffect.EffectType.SetMaxScore:
                    GameState.Instance.SetMaxScore(effect.scoreValue);
                    break;
            }
        }
    }

    private IEnumerator TypeText(string text)
    {
        dialogueText.text = "";
        foreach (char c in text.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void ClearUI()
    {
        foreach (Transform child in buttonsParent)
        {
            Destroy(child.gameObject);
        }

        dialogueText.text = "";
    }

    public void EndDialogue()
    {
        ClearUI();
        gameObject.SetActive(false);
    }
}