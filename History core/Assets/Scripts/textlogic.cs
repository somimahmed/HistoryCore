using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class textlogic : MonoBehaviour
{
    public GameObject[] sf;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int i=0;
    int j=0;
    
    // Update is called once per frame
    void Update()
    {
    if(Input.GetMouseButtonDown(0)&&i<=sf.Length){
        sf[j].SetActive(false);
        if(i<sf.Length){sf[i].SetActive(true);}
        if(i>0){
            j++;
        }
        i++;
        if(i>10){
            SceneManager.LoadScene(3);
        }
    }
    }
}