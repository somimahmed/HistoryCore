using UnityEngine;
using UnityEngine.UI;

public class DialogueStarter : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public DialogueNode startNode;

    void Start()
    {
        DialogueManager.Instance.StartDialogue(startNode);
    }
}