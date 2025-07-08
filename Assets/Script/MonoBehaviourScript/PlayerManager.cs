using TMPro;
using UnityEngine.Events;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public UnityEvent onCurrencyUpdate;

    private double _currentCurrency;
    public double current_Currency
    {
        get => _currentCurrency;
        set
        {
            _currentCurrency = value;
            onCurrencyUpdate?.Invoke();
        }
    }



    private void Update()
    {
        current_Currency+=Time.deltaTime*GameManager.Instance.AutoMultiplier;
    }

    public void UpdateCurrencyText(TMP_Text text)
    {
        text.text = current_Currency.ToString("0");
    }
}
