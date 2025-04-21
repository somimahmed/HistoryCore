using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Dialogue/Character")]
public class CharacterSpeaker : ScriptableObject
{
    public string characterName;
    public Sprite portrait;
    public Color nameColor = Color.white;
}