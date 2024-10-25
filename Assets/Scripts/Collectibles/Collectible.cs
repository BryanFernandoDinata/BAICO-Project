using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : IInteractibles
{   
    public TrashType trashType;
    bool canCollet = false;

    private void Update() 
    {
        if(canCollet)
        {
            if(Input.GetKeyDown(KeyCode.E))
            CollectItem();
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            sr.sprite = WhiteItemSprite;
            if(PlayerController.instance.sr.color != playerColorTransparent)
            {
                PlayerController.instance.sr.color = playerColorTransparent;
            }
            pressEToActivate.SetActive(true);
            canCollet = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            sr.sprite = WhiteItemSprite;
            if(PlayerController.instance.sr.color != playerColorTransparent)
            {
                PlayerController.instance.sr.color = playerColorTransparent;
            }
            pressEToActivate.SetActive(true);
            canCollet = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            canCollet = false;
            sr.sprite = ItemSprite;
            if(PlayerController.instance.sr.color != playerColorNormal)
            {
                PlayerController.instance.sr.color = playerColorNormal;
            }

            pressEToActivate.SetActive(false);
        }   
    }
    void CollectItem()
    {
        if(PlayerController.instance.collectedCollectibles.Count < 1)
        {
            PlayerController.instance.collectedCollectibles.Add(this);
            anim.SetTrigger("Collect");
            AudioManager.instance.PlaySFX(9);
        }else
        {
            if(PlayerController.instance.collectedCollectibles.Count < GameManager.instance.playerStats[0].maximumCollectTrashAmt)
            {
                if(PlayerController.instance.collectedCollectibles[PlayerController.instance.collectedCollectibles.Count - 1].trashType == trashType)
                {
                    PlayerController.instance.collectedCollectibles.Add(this);
                    anim.SetTrigger("Collect");
                    AudioManager.instance.PlaySFX(9);
                }
            }
        }
    }
}
