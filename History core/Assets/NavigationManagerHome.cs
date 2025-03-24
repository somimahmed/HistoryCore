using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManagerHome : MonoBehaviour
{
    
    public GameObject PromptImg;
    public void QuitToHome(){
        SceneManager.LoadScene("Home");
    }

    public void SkipPromptImg(){
        PromptImg.SetActive(false);
    }
    
}
