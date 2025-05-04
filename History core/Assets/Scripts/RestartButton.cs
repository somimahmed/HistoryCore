using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    // Метод будет вызываться при нажатии на кнопку
    public void RestartCurrentScene()
    {
        // Получаем индекс текущей активной сцены
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Перезагружаем текущую сцену
        SceneManager.LoadScene(currentSceneIndex);


        // string currentSceneName = SceneManager.GetActiveScene().name;
        // SceneManager.LoadScene(currentSceneName);
    }
}