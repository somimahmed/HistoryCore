using Unity.VisualScripting;
using UnityEngine;

public class textlogic : MonoBehaviour
{
    public GameObject[] sf;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    int i=0;
    int j=0;
    

    // Update is called once per frame
    void Update()
    {
    if(Input.GetMouseButtonDown(0)&&i<sf.Length){
        sf[j].SetActive(false);
        sf[i].SetActive(true);
        if(i>0){
            j++;
        }
        i++;
    }
    }
}