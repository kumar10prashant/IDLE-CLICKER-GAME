using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

[DefaultExecutionOrder(1000)]
public class UpgradblePanel : MonoBehaviour
{
    public Button btn;
    public List<Button> allBtn;

 
    private void Start()
    {
        allBtn = FindObjectsByType<Button>(FindObjectsSortMode.None).ToList();
        foreach(var btn in allBtn)
        {
            btn.onClick.AddListener(() =>
            {
                btn.transform.DOScale(Vector3.one * 0.8f, 0.1f).SetEase(Ease.OutBack).onComplete += () =>
                {
                    btn.transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.InBack);
                };
            });
        }
    }
    public void SendDown(RectTransform rect)
    {
           rect.DOAnchorPosY(rect.anchoredPosition.y - rect.sizeDelta.y, 0.5f).SetEase(Ease.OutBack).onComplete += () =>
           {
               btn.onClick.RemoveAllListeners();
               btn.onClick.AddListener(() => SendUp(rect));
           }; 
    }

    public void SendUp(RectTransform rect)
    {
        
        rect.DOAnchorPosY(rect.anchoredPosition.y + rect.sizeDelta.y, 0.5f).SetEase(Ease.InBack).onComplete += () =>
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => SendDown(rect));
        };
    }
}
