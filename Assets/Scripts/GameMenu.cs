using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using MoreMountains.Feel;
public class GameMenu : MonoBehaviour 
{
    public static GameMenu instance;

    [Header("MainGameUI")]
    public TextMeshProUGUI totalStoredTrash;
    public HealthController playerHealthController;
    public TextMeshProUGUI coinText;

    [Header("Boss")]
    public GameObject bossHealth;

    [Header("ButtonColor")]
    public Color defaultColor;
    public Color hoverColor;

    [Header("GameOverPanel")]
    public GameObject GameOverPanel;
    
    [Header("Shop UI")]
    public GameObject shopPanel;
    public GameObject mainPurchaseButtonHolder;
    public GameObject ownedTextHolder;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI purchaseText;
    public TextMeshProUGUI itemCostText;
    public Button purchaseButton;
    public List<ShopItem> shopItems = new List<ShopItem>();
    [HideInInspector] public ShopItem selectedShopItem;
    
    //Pop up confirmation
    public GameObject popUpPanel;
    public TextMeshProUGUI confirmationDescriptionText;
    public TextMeshProUGUI purchaseTextConfirm;

    [Header("Puase UI")]
    public GameObject pauseMenu;
    public GameObject mainTab;
    public Image muteImage;
    public Sprite mutedIcon;
    public Sprite notMutedIcon;
    [HideInInspector] public int isMuted = 1;

    [Header("Quest UI")]
    public GameObject questMenu;
    public GameObject questOngoingHolder;
    public GameObject questDoneHolder;
    public QuestItemUI questItemUI;

    private void Awake() 
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start() 
    {
        if(PlayerPrefs.HasKey("Mute"))
        {
            if(PlayerPrefs.GetInt("Mute") == 1)
            {
                muteImage.sprite = mutedIcon;
                isMuted = 1;
            }else
            {
                muteImage.sprite = notMutedIcon;
                isMuted = 0;
            }
        }else
        {
            muteImage.sprite = notMutedIcon;
            isMuted = 0;
        }
    }

    #region Shop
    public void OpenShop()
    {
        if(shopPanel.activeInHierarchy == false)
        {
            GameManager.instance.shopActive = true;
            shopPanel.SetActive(true);
            AudioManager.instance.PlaySFX(9);
            
            //ResetAllSelectedShopItems();
            //CheckSelectedItem();
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

        if(selectedShopItem.item.itemName == "Carry Capacity")
        {
            GameManager.instance.carryCapacityBuffValue += selectedShopItem.item.itemBuffAmt;
        }else if(selectedShopItem.item.itemName == "Trash Bag")
        {
            GameManager.instance.carryCapacityBuffValue += selectedShopItem.item.itemBuffAmt;
        }else if(selectedShopItem.item.itemName == "Speed Boots")
        {
            GameManager.instance.speedBuffValue += selectedShopItem.item.itemBuffAmt;
        }

        AudioManager.instance.PlaySFX(9);
        ClosePopUpPanel();
    }
    public void OpenPopUpPanel()
    {
        if(!popUpPanel.activeInHierarchy)
        {
            AudioManager.instance.PlaySFX(9);
            UpdateShopUI();
            popUpPanel.SetActive(true);
        }
    }
    public void ClosePopUpPanel()
    {
        if(popUpPanel.activeInHierarchy)
        {
            AudioManager.instance.PlaySFX(9);
            popUpPanel.SetActive(false);
        }
    }
    public void CloseShop()
    {
        if(shopPanel.activeInHierarchy)
        {
            AudioManager.instance.PlaySFX(9);
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
    public void UpdateShopUI()
    {
        purchaseText.text = "Purchase";

        if(selectedShopItem != null)
        {   
            if(!selectedShopItem.notEnoughGold)
            {
                purchaseButton.interactable = true;
            }

            confirmationDescriptionText.text = "Do you want to purchase " + selectedShopItem.item.itemName;
        }

        purchaseTextConfirm.text = "Purchase";
        CheckSelectedItem();
        
    }

    public void CheckSelectedItem()
    {
        if(selectedShopItem != null)
        {
            mainPurchaseButtonHolder.SetActive(true);
            ownedTextHolder.SetActive(false);
            //item is owned but is not upgrade able and does not require something
            if(selectedShopItem.item.isItem)
            {
                if(selectedShopItem.item.isOwned && !selectedShopItem.item.isUpgradeAble && !selectedShopItem.item.requireSomething)
                {
                    purchaseText.text = "Owned";
                    purchaseButton.interactable = false;

                    //confirmationDescriptionText.text = "Do you want to upgrade " + selectedShopItem.item.itemName;
                    purchaseTextConfirm.text = "Owned";

                    mainPurchaseButtonHolder.SetActive(false);
                    ownedTextHolder.SetActive(true);
                }
                //item is owned but is  upgrade able and does not require something
                else if(selectedShopItem.item.isOwned && selectedShopItem.item.isUpgradeAble && !selectedShopItem.item.requireSomething)
                {
                    ChangeTextToUpgrade();
                    if(selectedShopItem.notEnoughGold)
                    {
                        purchaseButton.interactable = false;
                    }
                }
                //item is owned but is upgrade able and require something
                else if(selectedShopItem.item.isOwned && selectedShopItem.item.isUpgradeAble && selectedShopItem.item.requireSomething)
                {
                    ChangeTextToUpgrade();
                    if(selectedShopItem.item.itemRequiered.isOwned)
                    {
                        purchaseButton.interactable = true;
                    }else
                    {
                        if(selectedShopItem.notEnoughGold)
                        {
                            purchaseButton.interactable = false;
                        }
                    }
                }
                //item is not owned but is not upgrade able and does not require something
                else if(!selectedShopItem.item.isOwned && selectedShopItem.item.isUpgradeAble && selectedShopItem.item.requireSomething)
                {
                    ChangeTextToPurchase();
                    if(selectedShopItem.item.itemRequiered.isOwned)
                    {
                        purchaseButton.interactable = true;
                    }else
                    {
                        purchaseButton.interactable = false;
                    }
                }
            }else
            {
                if(selectedShopItem.item.isOwned)
                {
                    if(selectedShopItem.item.isUpgradeAble)
                    {
                        if(selectedShopItem.item.requireSomething)
                        {
                            ChangeTextToUpgrade();
                            if(selectedShopItem.item.itemRequiered.isOwned)
                            {
                                purchaseButton.interactable = true;
                                
                            }else
                            {
                               purchaseButton.interactable = false;
                            }
                        }
                    }
                }else
                {
                    ChangeTextToPurchase();
                    if(selectedShopItem.item.requireSomething)
                    {
                        if(selectedShopItem.item.itemRequiered.isOwned)
                        {
                            if(!selectedShopItem.notEnoughGold)
                            {
                               purchaseButton.interactable = true;  
                            }
                        }else
                        {
                            if(selectedShopItem.notEnoughGold)
                            {
                                purchaseButton.interactable = false;
                            }
                        }
                    }else
                    {
                        if(!selectedShopItem.notEnoughGold)
                        {
                           purchaseButton.interactable = true;  
                        }
                    }
                }
            }
        }   
    }

    void ChangeTextToUpgrade()
    {
        itemCostText.text = selectedShopItem.item.itemCost.ToString();
        confirmationDescriptionText.text = "Do you want to upgrade " + selectedShopItem.item.itemName;
        purchaseText.text = "Upgrade";                 
        purchaseTextConfirm.text = "Upgrade";
    }
    void ChangeTextToPurchase()
    {
        itemCostText.text = selectedShopItem.item.itemCost.ToString();
        confirmationDescriptionText.text = "Do you want to purchase " + selectedShopItem.item.itemName;
        purchaseText.text = "Purchase";             
        purchaseTextConfirm.text = "Purchase";
    }
    #endregion
    #region Pause
    public void OpenPausePanel()
    {
        if(!pauseMenu.activeInHierarchy)
        {
            AudioManager.instance.PlaySFX(9);
            GameManager.instance.pauseMenuOpen = true;
            pauseMenu.SetActive(true);
        }
    }
    public void Mute()
    {   
        //AudioManager.instance.mute
        AudioManager.instance.PlaySFX(9);

        if(isMuted == 0)
        {   
            muteImage.sprite = mutedIcon;
            PlayerPrefs.SetInt("Mute", 1);
            isMuted = 1;

            AudioManager.instance.Mute();
        }else
        {
            muteImage.sprite = notMutedIcon;
            PlayerPrefs.SetInt("Mute", 0);
            isMuted = 0;
            
            AudioManager.instance.UnMute();
        }
        
    }
    public void OpenQuestPanel()
    {
        if(!questMenu.activeInHierarchy)
        {
            AudioManager.instance.PlaySFX(9);
            CloseMainTab();
            GameManager.instance.pauseMenuOpen = true;
            questMenu.SetActive(true);
        }
    }
    public void SaveGame()
    {
        GameManager.instance.SaveData();
        AudioManager.instance.PlaySFX(9);
    }
    public void CloseMainTab()
    {
        if(mainTab.activeInHierarchy)
        { 
            AudioManager.instance.PlaySFX(9);
            //GameManager.instance.pauseMenuOpen = true;
            mainTab.SetActive(false);
            questMenu.SetActive(true);
        } 
    }
    public void CloseQuestPanel()
    {
        if(questMenu.activeInHierarchy)
        {
            AudioManager.instance.PlaySFX(9);
            CloseMainTab();
            //GameManager.instance.pauseMenuOpen = true;
            questMenu.SetActive(false);
            mainTab.SetActive(true);
        }
    }
    public void ClosePausePanel()
    {        
        if(pauseMenu.activeInHierarchy)
        {
            AudioManager.instance.PlaySFX(9);
            GameManager.instance.pauseMenuOpen = false;
            pauseMenu.SetActive(false);
        }
    }
    #endregion
    #region Button
    public void Hover(Image image)
    {
        image.color = hoverColor;
        AudioManager.instance.PlaySFX(8);
    }
    public void ExitHover(Image image)
    {
        image.color = defaultColor;
    }
    #endregion
}
