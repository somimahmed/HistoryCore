using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public TypeWriterEffectTMP k;
    public GameObject obj1;
    public GameObject obj2;
    // Update is called once per frame
    void Update()
    {
        if(k.a==1){
            obj1.SetActive(false);
            obj2.SetActive(true);
        }
    }
}
