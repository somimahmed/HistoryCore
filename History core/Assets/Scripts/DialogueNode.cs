using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewDialogueNode", menuName = "Dialogue/Node")]
public class DialogueNode : ScriptableObject
{
    [TextArea(3, 5)] public string text;
    public CharacterSpeaker speaker;
    public DialogueResponse[] responses;
    public Sprite background;
    public AudioClip soundEffect;
    public UnityEvent onNodeEnter;
}

// DialogueResponse.cs
[System.Serializable]
public class DialogueResponse
{
    public string text;
    public DialogueNode nextNode;
    public GameCondition[] conditions;
    public GameEffect[] effects;
}

// GameCondition.cs
[System.Serializable]
public class GameCondition
{
    public string flagKey;
    public bool requiredValue;
}

// GameEffect.cs
[System.Serializable]
public class GameEffect
{
    public enum EffectType
    {
        SetFlag,
        AddScore,
        SetMaxScore
    }

    public EffectType effectType;

    public string flagKey;
    public bool value;

    public int scoreValue;
}