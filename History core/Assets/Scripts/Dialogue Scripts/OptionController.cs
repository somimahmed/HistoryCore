using UnityEngine;

public class OptionController : MonoBehaviour
{
    
    public Options[] options;
    public void TriggerDiplomatic()
    {
        if(DialogueManager.Instance.optionButtons !=null)
            DialogueManager.Instance.optionButtons.SetActive(false);
        DialogueManager.Instance.dialogueNode = options[DialogueManager.Instance.optionIteration].options[0];
        Debug.Log(options[DialogueManager.Instance.optionIteration].options[0]);
        Debug.Log(options[DialogueManager.Instance.optionIteration]);
        DialogueManager.Instance.reinitialize();
    }
    public void TriggerAggressive()
    {
        if(DialogueManager.Instance.optionButtons !=null)
            DialogueManager.Instance.optionButtons.SetActive(false);
        DialogueManager.Instance.dialogueNode = options[DialogueManager.Instance.optionIteration].options[1];
        DialogueManager.Instance.reinitialize();
    }
    public void TriggerAbsurd()
    {
        if(DialogueManager.Instance.optionButtons !=null)
            DialogueManager.Instance.optionButtons.SetActive(false);
        DialogueManager.Instance.dialogueNode = options[DialogueManager.Instance.optionIteration].options[2];
        DialogueManager.Instance.reinitialize();
    }
}

[System.Serializable]
public class Options
{
    public DialogueNode[] options;
}

