using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class GameMenu : MonoBehaviour 
{
    public static GameMenu instance;

    [Header("New")]
    public TextMeshProUGUI totalStoredTrash;

    public HealthController playerHealthController;

    [Header("GameOverPanel")]
    public GameObject GameOverPanel;

    [Header("Shop UI")]
    public GameObject shopPanel;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI purchaseText;
    public Button purchaseButton;
    public List<ShopItem> shopItems = new List<ShopItem>();
    [HideInInspector] public ShopItem selectedShopItem;
    
    //Pop up confirmation
    public GameObject popUpPanel;
    public TextMeshProUGUI confirmationDescriptionText;
    public TextMeshProUGUI purchaseTextConfirm;
    private void Awake() 
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public void OpenShop()
    {
        if(shopPanel.activeInHierarchy == false)
        {
            GameManager.instance.shopActive = true;
            shopPanel.SetActive(true);
            
            ResetAllSelectedShopItems();

            if(!PlayerPrefs.HasKey("lastShopItemSelected"))
            {
                //shopItems[0].Select();
                PlayerPrefs.SetInt("lastShopItemSelected", 0);
                UpdateShopUI();
                return;
            }else
            {
                //shopItems[PlayerPrefs.GetInt("lastShopItemSelected")].Select();
                UpdateShopUI();
                return;
            }
        }
    }
    public void PurchaseItem()
    {
        if(selectedShopItem.item.isOwned == false)
        {
            selectedShopItem.Purcahse();
        }else
        {
            if(selectedShopItem.item.isUpgradeAble)
            {
                
                selectedShopItem.Upgrade();
            }
        }
        
    }
    public void OpenPopUpPanel()
    {
        if(!popUpPanel.activeInHierarchy)
        {
            UpdateShopUI();
            popUpPanel.SetActive(true);
        }
    }
    public void ClosePopUpPanel()
    {
        if(popUpPanel.activeInHierarchy)
        {
            popUpPanel.SetActive(false);
        }
    }
    public void CloseShop()
    {
        if(shopPanel.activeInHierarchy)
        {
            shopPanel.SetActive(false);
            GameManager.instance.shopActive = false;
        }
    }
    public void ResetAllSelectedShopItems()
    {
        foreach(ShopItem s in shopItems)
        {
            s.Deselect();
        }
    }
    void UpdateShopUI()
    {
        purchaseText.text = "Purchase";
        purchaseButton.interactable = true;
        confirmationDescriptionText.text = "Do you want to buy " + selectedShopItem.item.itemName;
        purchaseTextConfirm.text = "Purchase";

        if(selectedShopItem.item.isOwned && !selectedShopItem.item.isUpgradeAble && !selectedShopItem.item.requireSomething)
        {
            purchaseText.text = "Owned";
            purchaseButton.interactable = false;

            //confirmationDescriptionText.text = "Do you want to upgrade " + selectedShopItem.item.itemName;
            purchaseTextConfirm.text = "Owned";
        }else if(selectedShopItem.item.isOwned && selectedShopItem.item.isUpgradeAble && !selectedShopItem.item.requireSomething)
        {
            purchaseText.text = "Upgrade";
            purchaseButton.interactable = false;

            confirmationDescriptionText.text = "Do you want to upgrade " + selectedShopItem.item.itemName;
            purchaseTextConfirm.text = "Upgrade";
        }else if(selectedShopItem.item.isOwned && selectedShopItem.item.isUpgradeAble && selectedShopItem.item.requireSomething)
        {
            confirmationDescriptionText.text = "Do you want to upgrade " + selectedShopItem.item.itemName;

            if(selectedShopItem.item.itemRequiered.isOwned)
            {
                purchaseText.text = "Upgrade";
                purchaseButton.interactable = true;
                
                purchaseTextConfirm.text = "Upgrade";
            }else
            {
                purchaseText.text = "Upgrade";
                purchaseButton.interactable = false;

                confirmationDescriptionText.text = "Do you want to upgrade " + selectedShopItem.item.itemName;
                purchaseTextConfirm.text = "Upgrade";
            }
        }
    }
}
