using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour 
{

    public List<DialogSO> lines;

    private bool canActivate;

    [Header("Shop")]
    public bool shouldActivateShop = false;

    [Header("Quest")]
    public bool shouldActivateQuest;
    public string questToMark;
    public bool markComplete;


	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		if(canActivate && Input.GetKeyDown(KeyCode.E) && !DialogManager.instance.dialogBox.activeInHierarchy && PlayerController.instance.canMove)
        {
            DialogManager.instance.ShowDialog(lines);
            DialogManager.instance.ShouldActivateQuestAtEnd(questToMark, markComplete);
            DialogManager.instance.shouldActivateShop = shouldActivateShop;
        }else if(canActivate && Input.GetKeyDown(KeyCode.E) && DialogManager.instance.dialogBox.activeInHierarchy && DialogManager.instance.dialogText.text == DialogManager.instance.dialogLines[DialogManager.instance.currentLine].dialogLine)
        {
            DialogManager.instance.NextLine();
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canActivate = false;
        }
    }
}
