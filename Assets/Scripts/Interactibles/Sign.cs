using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : IInteractibles
{
    bool canActivate = false;
    private void Update() 
    {
        if(canActivate)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                // Do something here
            }
        }    
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            pressEToActivate.SetActive(true);    
            canActivate = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            pressEToActivate.SetActive(false);    
            canActivate = false;
        }    
    }
}
