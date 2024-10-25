using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Feedbacks;
using System;

public class GameManager : MonoBehaviour 
{

    public static GameManager instance;
    public bool pauseMenuOpen = false, dialogActive = false, fadingBetweenAreas = false, shopActive = false;
    public int totalStoredTrash;

    [Header("Player")]
    public CharStats[] playerStats;

    [Header("Items")]
    public List<Item> itemsOwned = new List<Item>();
    public List<Item> allInGameItems = new List<Item>();

    [Header("Quest")]
    public List<QuestSO> allQuestInTheGame = new List<QuestSO>();
    [HideInInspector] public int speedBuffValue;
    [HideInInspector] public int carryCapacityBuffValue;

    [Header("Feel")]
    public MMF_Player mmPlayer;

	// Use this for initialization
	void Start () 
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
        
        for(int i = 0; i < allInGameItems.Count; i ++)
        {
            if(PlayerPrefs.HasKey(allInGameItems[i].itemName))
            {
                itemsOwned.Add(allInGameItems[i]);
                allInGameItems[i].isOwned = true;

                if(allInGameItems[i].isOwned)
                {
                    if(allInGameItems[i].itemName == "Carry Capacity")
                    {
                        GameManager.instance.carryCapacityBuffValue += allInGameItems[i].itemBuffAmt;
                    }else if(allInGameItems[i].itemName == "Trash Bag")
                    {
                        GameManager.instance.carryCapacityBuffValue += allInGameItems[i].itemBuffAmt;
                    }else if(allInGameItems[i].itemName == "Speed Boots")
                    {
                        GameManager.instance.speedBuffValue += allInGameItems[i].itemBuffAmt;
                    }
                }
            }else
            {
                allInGameItems[i].isOwned = false;
            }
        }
        for(int i = 0; i < allQuestInTheGame.Count; i ++)
        {
            if(PlayerPrefs.HasKey(allQuestInTheGame[i].questName))
            {
                GameObject questUI = Instantiate(GameMenu.instance.questItemUI.gameObject);
                
                if(allQuestInTheGame[i].questIsDone)
                {
                    questUI.transform.parent = GameMenu.instance.questDoneHolder.transform;
                }else
                {
                    questUI.transform.parent = GameMenu.instance.questOngoingHolder.transform;
                }
                
                if(allQuestInTheGame[i].questIsDone || PlayerPrefs.GetString(allQuestInTheGame[i].questName) == allQuestInTheGame[i].questName + "Done")
                {
                    //DialogManager.instance.UpdateQuest(allQuestInTheGame[i], GameMenu.instance.questItemUI);
                }
                DialogManager.instance.UpdateTextQuest(allQuestInTheGame[i],GameMenu.instance.questItemUI);
            }
        }
        
	}
	
	// Update is called once per frame
	void Update () 
    {
		if(pauseMenuOpen || dialogActive || fadingBetweenAreas || shopActive ||playerStats[0].currentHealth <=0)
        {
            PlayerController.instance.canMove = false;
        } else
        {
            PlayerController.instance.canMove = true;
        }

        // if (Input.GetKeyDown(KeyCode.O))
        // {
        //     SaveData();
        // }

        // if (Input.GetKeyDown(KeyCode.P))
        // {
        //     LoadData();
        // }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!pauseMenuOpen)
            {
                GameMenu.instance.OpenPausePanel();
            }else
            {
                if(!GameMenu.instance.questMenu.activeInHierarchy)
                {
                    GameMenu.instance.ClosePausePanel();
                }else
                {
                    GameMenu.instance.CloseQuestPanel();
                }
            }
        }
        
    }

    public void SaveData()
    {
        PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("Player_Position_x", PlayerController.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_y", PlayerController.instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_z", PlayerController.instance.transform.position.z);
    }

    public void LoadData()
    {
        PlayerController.instance.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_Position_x"), PlayerPrefs.GetFloat("Player_Position_y"), PlayerPrefs.GetFloat("Player_Position_z"));
    }
}
