using UnityEngine;
using UnityEngine.SceneManagement;
public class scenemanager2 : MonoBehaviour
{
    textlogic l;
    public void scene2(){
        if(l.i==10){
            SceneManager.LoadScene(2);
        }
    }
    
}
