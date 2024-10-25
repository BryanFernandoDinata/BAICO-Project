using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Lighter : IInteractibles
{
    bool alreadyActivated1 = false, alreadyActivated2 = false;
    bool canActivate1 = false, canActivate2 = false; 
    bool isInteractible = true;
    public PlayableDirector timeline;
    private void Update() 
    {
        if(canActivate1 && isInteractible || canActivate2 && isInteractible)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(alreadyActivated1 == false)
                {
                    anim.SetTrigger("Light");
                    alreadyActivated1 = true;
                    isInteractible = false;
                    timeline.Play();
                }else
                {
                    if(alreadyActivated2 == false)
                    {
                        anim.SetTrigger("Light");
                        alreadyActivated2 = true;
                        isInteractible = false;
                    }
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            if(isInteractible)
            {
                pressEToActivate.gameObject.SetActive(true);

                if(PlayerController.instance.sr.color != playerColorTransparent)
                {
                    PlayerController.instance.sr.color = playerColorTransparent;
                }

                if(alreadyActivated1 == false)
                {
                    canActivate1 = true;
                }else
                {
                    canActivate2 = true;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            pressEToActivate.gameObject.SetActive(false);

            if(PlayerController.instance.sr.color != playerColorTransparent)
            {
                PlayerController.instance.sr.color = playerColorTransparent;
            }

            if(alreadyActivated1 == false)
            {   
                canActivate1 = false;
            }else
            {
                canActivate2 = false;
            }
        }
    }
}
