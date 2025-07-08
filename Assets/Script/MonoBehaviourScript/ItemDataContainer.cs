using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemDataContainer : MonoBehaviour
{
    public Image icon;
    public TMP_Text itemName,itemDescription,buttonText;
    public Button button;
    

    public void init(string name, string description, Sprite iconSprite, double buttonTextValue)
    {
        itemName.text = name;
        itemDescription.text = description;
        icon.sprite = iconSprite;
        buttonText.text = "Amount:\n" + MoneyFormatter.FormatMoney(buttonTextValue);
    }
}
