using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Feedbacks;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public MMF_Player mmPlayer;

    public CharStats[] playerStats;

    public bool gameMenuOpen = false, dialogActive = false, fadingBetweenAreas = false, shopActive = false;

    public string[] itemsHeld;
    public int[] numberOfItems;
    public Item[] referenceItems;
    public int totalStoredTrash;

	// Use this for initialization
	void Start () 
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(gameMenuOpen || dialogActive || fadingBetweenAreas || shopActive ||playerStats[0].currentHealth <=0)
        {
            PlayerController.instance.canMove = false;
        } else
        {
            PlayerController.instance.canMove = true;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveData();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadData();
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
