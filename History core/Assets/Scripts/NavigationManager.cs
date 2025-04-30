using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class NavigationManager : MonoBehaviour
{ 
    public GameObject settingPanel;
    public GameObject HomeUis;
    public GameObject GlobeUi;



    public void OpenSettingPanel(){
    settingPanel.gameObject.SetActive(true);
    settingPanel.GetComponent<RectTransform>().DOAnchorPos( Vector2.zero,.3f);   
  } 

  public void DeactivatingSettingPanel(){
     Invoke("falsingSettingPanel",.3f); 
    settingPanel.GetComponent<RectTransform>().DOAnchorPos(new Vector2(3000,0),.3f);
  }
  void falsingSettingPanel(){
   settingPanel.gameObject.SetActive(true);
  }

  public void QuitGame(){
    Application.Quit();
  }

  public void LoadGame(){
    SceneManager.LoadScene(1);
  }

  // Bac to globe panel from home
  public void BackToGlobePanel(){
    HomeUis.SetActive(false);
    GlobeUi.SetActive(true);
  }


}
