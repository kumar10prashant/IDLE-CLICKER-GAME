using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapCollectble : MonoBehaviour
{
    [SerializeField] GameObject particle;
    [SerializeField] GameObject text;
    public void OnMouseDown()
    {
        TapToCollect();
    }

    public void TapToCollect()
    {
        text.SetActive(false);
        Destroy(Instantiate(particle, transform), 3f);
        transform.DOScale(Vector3.one * 0.8f, 0.1f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.InBack);

        });
        PlayerManager.Instance.AddCurrency(GameManager.Instance.TapMultiplier);

    }
}
