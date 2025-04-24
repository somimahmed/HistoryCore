using UnityEngine;
using UnityEngine.UI;

public class ProgressController : MonoBehaviour
{
    [SerializeField] private Slider progressSlider;

    void Update()
    {
        progressSlider.maxValue = GameState.Instance.MaxScore;
        progressSlider.value = GameState.Instance.QuizScore;
    }
}