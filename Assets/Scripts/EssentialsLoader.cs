﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour 
{
    public GameObject UIScreen;
    public GameObject player;
    public GameObject gameMan;
    public GameObject audioMan;
    public GameObject currencyManager;
    public Transform playerSpawnPosition;
    //public Transform playerTransform;

	// Use this for initialization
	void Start () {
		if(UIFade.instance == null)
        {
            UIFade.instance = Instantiate(UIScreen).GetComponent<UIFade>();
        }

        if(PlayerController.instance == null)
        {
            PlayerController clone = Instantiate(player, playerSpawnPosition).GetComponent<PlayerController>();
            PlayerController.instance = clone;
            //playerTransform = clone.transform;
        }

        if (GameManager.instance == null)
        {
            GameManager.instance = Instantiate(gameMan).GetComponent<GameManager>();
        }

        if (CurrencyManager.instance == null)
        {
            CurrencyManager.instance = Instantiate(currencyManager).GetComponent<CurrencyManager>();
        }

        if(AudioManager.instance == null)
        {
            AudioManager.instance = Instantiate(audioMan).GetComponent<AudioManager>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
