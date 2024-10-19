using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrashType
{
    Organic,
    Anorganic
}
public class TrashCan : IInteractibles
{
    public TrashType TrashCanType;
    public List<Collectible> storedCollectibles = new List<Collectible>();
    bool canStore = false;

    private void Update() 
    {
        if(canStore)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                StoreItem();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            if(PlayerController.instance.sr.color != playerColorTransparent)
            {
                PlayerController.instance.sr.color = playerColorTransparent;
            }
            pressEToActivate.SetActive(true);
        }
        canStore = true;
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            if(PlayerController.instance.sr.color != playerColorTransparent)
            {
                PlayerController.instance.sr.color = playerColorTransparent;
            }
            pressEToActivate.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            canStore = false;
            if(PlayerController.instance.sr.color != playerColorNormal)
            {
                PlayerController.instance.sr.color = playerColorNormal;
            }
            pressEToActivate.SetActive(false);
        }   
    }
    public void StoreItem()
    {
        for(int i = 0; i < PlayerController.instance.collectedCollectibles.Count; i++)
        {
            if(PlayerController.instance.collectedCollectibles[i].trashType == TrashCanType)
            {
                storedCollectibles.Add(PlayerController.instance.collectedCollectibles[i]);
                GameManager.instance.totalStoredTrash ++;
                GameMenu.instance.totalStoredTrash.text = "Total Stored Trash " + GameManager.instance.totalStoredTrash.ToString();
            }else
            {
                GameMenu.instance.playerHealthController.TakeDamagePlayer(1);
                return;
            }    
        }

        PlayerController.instance.collectedCollectibles.Clear();
    }
}
