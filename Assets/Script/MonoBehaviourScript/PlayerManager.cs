using TMPro;
using UnityEngine.Events;
using UnityEngine;
using System;

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
    public static PlayerManager Instance;

    


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           
        }
        
    }

    private void Start()
    {
        current_Currency = 50;
    }

    private void OnDisable()
    {
        
    }


    private void Update()
    {
        current_Currency+=Time.deltaTime*GameManager.Instance.AutoMultiplier;
    }

    public void UpdateCurrencyText(TMP_Text text)
    {
        text.text = MoneyFormatter.FormatMoney(current_Currency).ToString();
    }

    

    
}
