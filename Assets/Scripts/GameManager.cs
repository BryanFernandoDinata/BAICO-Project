using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Feedbacks;
using System;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public bool pauseMenuOpen = false, dialogActive = false, fadingBetweenAreas = false, shopActive = false;
    public int totalStoredTrash;

    [Header("Player")]
    public CharStats[] playerStats;
    public List<Item> itemsOwned = new List<Item>();
    public List<Item> allInGameItems = new List<Item>();

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
            }else
            {
                allInGameItems[i].isOwned = false;
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
