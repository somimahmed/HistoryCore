using System.Collections;
using UnityEngine;
using TMPro;

public class TypeWriterEffectTMP : MonoBehaviour
{
    public int a=0;
    public TextMeshProUGUI uiText;  // Assign your TMP text here
    public string fullText;         // Full text you want to display
    public float delay = 0.05f;      // Delay between each letter

    private string currentText = "";

    void Start()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            uiText.text = currentText;
            yield return new WaitForSeconds(delay);
            if(i==fullText.Length){
                a=1;
            }
        }
    }
}
