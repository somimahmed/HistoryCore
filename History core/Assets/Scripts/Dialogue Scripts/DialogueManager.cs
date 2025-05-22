using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    public GameObject optionButtons;
    [Header("Start Node")]
    public DialogueNode dialogueNode;

    [Header("Assign TMPs for each character in order")]
    public TextMeshPro[] TextObjects;
    
    
    private List<DialogueLineData> sortedLines = new List<DialogueLineData>();
    public int optionIteration;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void reinitialize()
    {
        optionIteration++;
        if (dialogueNode != null)
        {
            InitializeAllAudioTracks();
            CollectAndSortLines();
            StartCoroutine(PlayDialogueSequence());
        }
        else
        {
            Debug.LogError("DialogueNode not assigned!");
        }
        if (dialogueNode != null)
        {
            CollectAndSortLines();
            StartCoroutine(PlayDialogueSequence());
        }
        else
        {
            Debug.LogError("DialogueNode not assigned!");
        }
    }
    void Start()
    {
        optionIteration = -1;
        reinitialize();
    }

    void CollectAndSortLines()
    {
        sortedLines.Clear();

        for (int i = 0; i < dialogueNode.characters.Length; i++)
        {
            Characters character = dialogueNode.characters[i];

            if (character.lines != null)
            {
                for (int j = 0; j < character.lines.Length; j++)
                {
                    line currentLine = character.lines[j];

                    TextMeshPro targetTMP = (i < TextObjects.Length) ? TextObjects[i] : null;

                    if (targetTMP == null)
                    {
                        Debug.LogWarning($"TextObject for character index {i} not assigned! Line: \"{currentLine.text}\"");
                    }

                    sortedLines.Add(new DialogueLineData
                    {
                        text = currentLine.text,
                        order = currentLine.order,
                        audioTrack = currentLine.audioTrack,
                        targetTextObject = targetTMP
                    });
                }
            }
        }

        sortedLines = sortedLines.OrderBy(l => l.order).ToList();
    }

    IEnumerator PlayDialogueSequence()
    {
        for (int i = 0; i < sortedLines.Count; i++)
        {
            DialogueLineData lineData = sortedLines[i];
            
            if (lineData.targetTextObject != null)
                lineData.targetTextObject.text = lineData.text;
            
            if (lineData.audioTrack != null && lineData.audioTrack.clip != null)
            {
                lineData.audioTrack.source.Play();
                yield return new WaitForSeconds(lineData.audioTrack.clip.length);
            }
            else
            {
                yield return new WaitForSeconds(1f); // fallback wait
            }
            
            if (lineData.targetTextObject != null)
                lineData.targetTextObject.text = "";
        }

        Debug.Log("Dialogue sequence complete.");
        if(optionButtons !=null)
            optionButtons.SetActive(true);
    }

    
    void InitializeAllAudioTracks()
    {
        foreach (var character in dialogueNode.characters)
        {
            if (character.lines != null)
            {
                foreach (var l in character.lines)
                {
                    if (l.audioTrack != null && l.audioTrack.source == null)
                    {
                        GameObject audioObject = new GameObject("Audio_" + l.audioTrack.id);
                        audioObject.transform.SetParent(transform);

                        AudioSource source = audioObject.AddComponent<AudioSource>();
                        source.clip = l.audioTrack.clip;
                        source.loop = l.audioTrack.loop;
                        source.volume = l.audioTrack.volume;

                        l.audioTrack.source = source;
                        
                    }
                }
            }
        }
    }

    
}

public class DialogueLineData
{
    public string text;
    public int order;
    public AudioTrack audioTrack;
    public TextMeshPro targetTextObject;
}

