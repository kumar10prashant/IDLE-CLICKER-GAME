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
            if(_currentCurrency >= double.MaxValue - 1)
            {
                _currentCurrency = double.MaxValue;
            }
            else
            {
                _currentCurrency = value;
            }
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
       
    }

  

    private void Update()
    {
        AddCurrency(Time.deltaTime * GameManager.Instance.AutoMultiplier);
    }


    public void AddCurrency(double amount)
    {
        current_Currency += amount;
    }
    public void UpdateCurrencyText(TMP_Text text)
    {
        text.text = MoneyFormatter.FormatMoney(current_Currency).ToString();
    }

    

    
}
