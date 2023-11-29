using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class FadingManager : MonoBehaviour
{
    [Header("Main UI Canvas")]
    [SerializeField] private CanvasGroup panelsCanvasGroup;

    private Tween _fadingTextTween;
        
    public void ShowPanel(GameObject panel, float duration)
    {
        panelsCanvasGroup.DOFade(1f, duration).OnComplete(() => ActivatePanel(panel));
    }
       
    public void HidePanel(GameObject panel, float duration)
    {
        panelsCanvasGroup.DOFade(0f, duration).OnComplete(() => DeactivatePanel(panel));
    }

    public void SetFadingText(TextMeshProUGUI text, float duration)
    {
        _fadingTextTween = text.DOFade(0.0f, duration).SetEase(Ease.InSine).SetLoops(-1, LoopType.Yoyo);
    }

    private void DeactivatePanel(GameObject panel)
    {
        panel.SetActive(false);
        _fadingTextTween.Pause();
    }
        
    private void ActivatePanel(GameObject panel)
    {
        panel.SetActive(true);
        _fadingTextTween.Play();
    }
}
