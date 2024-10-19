using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopItem : MonoBehaviour
{
    public int shopItemIndex;
    public Image backgroundHolder;
    public Color DefaultColor;
    public Color SelectedColor;
    public Color HoverColor;
    [HideInInspector] public bool isSelected = false;

    [Header("AboutTheItem")]
    public Image itemIcon;
    public Item item;
    [HideInInspector] public bool alreadyPurchased;
    private void Start() 
    {
        if(item == null)
        {
            backgroundHolder.color = DefaultColor;
            itemIcon.color = DefaultColor;
            itemIcon.enabled = false;
        }else
        {
            itemIcon.sprite = item.itemImage;
        }
        if(PlayerPrefs.HasKey("lastShopItemSelected"))
        {
            Select();
        }else
        {
            if(shopItemIndex == 0)
            {
                Select();
            }
        }
    }
    public void Select()
    {
        if(item != null && !isSelected)
        {
            GameMenu.instance.ResetAllSelectedShopItems();
            
            PlayerPrefs.SetInt("lastShopItemSelected", shopItemIndex);
            
            isSelected = true;
            GameMenu.instance.selectedShopItem = this;
            backgroundHolder.color = SelectedColor;
            itemIcon.color = SelectedColor;

            GameMenu.instance.descriptionText.text = item.description;

            if(alreadyPurchased)
            {
                if(item.isAbility)
                {
                    GameMenu.instance.purchaseText.text = "Upgrade";
                }else if(item.isItem)
                {
                    GameMenu.instance.purchaseText.text = "Owned";
                }
            }else
            {
                GameMenu.instance.purchaseText.text = "Purchase";
            }
        }
    }
    public void Deselect()
    {
        if(item != null && isSelected)
        {
            isSelected = false;
            backgroundHolder.color = DefaultColor;
            itemIcon.color = DefaultColor;
        }
    }
    public void ExitHover()
    {
        if(item != null && !isSelected)
        {
            backgroundHolder.color = DefaultColor;
            itemIcon.color = DefaultColor;
        }
    }
    public void Hover()
    {
        if(item != null)
        {
            backgroundHolder.color = HoverColor;
            itemIcon.color = HoverColor;
        }
    }
    public void Purcahse()
    {
        // if(CurrencyManager.instance.coin > item.itemCost)
        // {

        // }

    }
    public void Upgrade()
    {

    }
}
