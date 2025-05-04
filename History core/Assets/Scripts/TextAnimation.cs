using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _TextPromp;
    [SerializeField] private float _betweenHalf = 0.05f;
    [SerializeField] private float _betweenChar = 0.03f;
    [SerializeField] private float _animationTime = 0.1f;
    //[SerializeField] private float _animationStartTime = 0f;
    //[SerializeField] private float _animationEndTime = 2;

    private List<float> _RightAlphas;
    private List<float> _LeftAlphas;
    
    private bool _isAnimating = false;
    
    
    private void Start()
    {
        _RightAlphas = new float[_TextPromp.text.Length].ToList();
        _LeftAlphas = new float[_TextPromp.text.Length].ToList();
    }

    private void Update()
    {
        if (_isAnimating)
        {
            SwitchColor();
        }

        //if ()
        //{
            // TextVisible(false);
            _isAnimating = true;
            StartCoroutine(AnimateText(0));
        //}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isAnimating = false;
            TextVisible(true);
        }
    }
        

    private void TextVisible(bool isVisible) 
    {
        StopAllCoroutines();
        DOTween.Kill(5);

        for (int i = 0; i < _RightAlphas.Count; i++)
        {
            _LeftAlphas[i] = isVisible ? 255 : 0;
            _RightAlphas[i] = isVisible ? 255 : 0;
        }
        SwitchColor();
    }

    private void SwitchColor()
    {
        for (int i = 0; i < _RightAlphas.Count; i++)
        {
            if (_TextPromp.textInfo.characterInfo[i].character != '\n' &&
                _TextPromp.textInfo.characterInfo[i].character != ' ')
            {
                int meshIndex = _TextPromp.textInfo.characterInfo[i].materialReferenceIndex;
                int vertexIndex = _TextPromp.textInfo.characterInfo[i].vertexIndex;

                Color32[] vertexColors = _TextPromp.textInfo.meshInfo[meshIndex].colors32;
                vertexColors[vertexIndex + 0].a = (byte)_LeftAlphas[i];
                vertexColors[vertexIndex + 1].a = (byte)_LeftAlphas[i];
                vertexColors[vertexIndex + 2].a = (byte)_RightAlphas[i];
                vertexColors[vertexIndex + 3].a = (byte)_RightAlphas[i];
            }
        }

        _TextPromp.UpdateVertexData();
    }

    private IEnumerator AnimateText(int i)
    {
        if(i >= _RightAlphas.Count)
            yield break;
        
        DOTween.To(
            () => _LeftAlphas[i],
            x => _LeftAlphas[i] = x,
            255,
            _animationTime)
            .SetEase(Ease.Linear)
            .SetId(5);
        yield return new WaitForSeconds(_betweenHalf);
        
        DOTween.To(
                () => _RightAlphas[i],
                x => _RightAlphas[i] = x,
                255,
                _animationTime)
            .SetEase(Ease.Linear)
            .SetId(5);
        yield return new WaitForSeconds(_betweenChar); 
        StartCoroutine(AnimateText(i + 1));
    }

}