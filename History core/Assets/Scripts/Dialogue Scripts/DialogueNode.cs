using UnityEngine;
using TMPro;
[CreateAssetMenu(menuName = "Dialogue/Node")]

public class DialogueNode : ScriptableObject
{
    public Characters[] characters;
    public DialogueNodeOption[] options;
}

[System.Serializable]
public class Characters
{
    public string name;
    public line[] lines;
    
}
[System.Serializable]
public class line
{
    public string text;
    public int order;
    public AudioTrack audioTrack;
}


[System.Serializable]
public class DialogueNodeOption
{
    public string[] lines;
    public DialogueNode nextNode;
}