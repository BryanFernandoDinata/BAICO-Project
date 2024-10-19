using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using UnityEngine.SceneManagement;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
public class HealthController : MonoBehaviour
{
    public List<Health> healths = new List<Health>();

    private void Start() 
    {
        GameManager.instance.playerStats[0].currentHealth = GameManager.instance.playerStats[0].maxHealth;
    }
    private void Update()
    {
        if(GameManager.instance.playerStats[0].currentHealth <= 0 && GameMenu.instance.GameOverPanel.activeInHierarchy)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                ResetHearts();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                GameMenu.instance.GameOverPanel.SetActive(false);
                //PlayerController.instance.canMove = true;
                 PlayerController.instance.sr.enabled = true;
            }   
        }
    }
    public void TakeDamagePlayer(int damage)
    {
        if(GameManager.instance.playerStats[0].currentHealth > 0)
        {
            GameManager.instance.playerStats[0].currentHealth -= damage;
            if(healths[GameManager.instance.playerStats[0].currentHealth].sr.sprite != healths[GameManager.instance.playerStats[0].currentHealth].emptyHeart)
            {
                healths[GameManager.instance.playerStats[0].currentHealth].sr.sprite = healths[GameManager.instance.playerStats[0].currentHealth].emptyHeart;
            }
            // for(int i = healths.Count -1; i >= 0; i--)
            // {
            //     if(healths[i].sr.sprite != healths[i].emptyHeart)
            //     {
            //         // if(healths[i].sr.sprite == healths[i].fullHeart)
            //         // {
            //         //     healths[i].sr.sprite = healths[i].halfHeart;
            //         //     break;
            //         // }else
            //         // {
            //             healths[i].sr.sprite = healths[i].emptyHeart;
            //             //break;
            //         //}
            //     }
            // }
            if(GameManager.instance.playerStats[0].currentHealth <= 0)
            {
                GameMenu.instance.GameOverPanel.SetActive(true);
                //PlayerController.instance.canMove = false;
                PlayerController.instance.sr.enabled = false;
            }
        }else
        {
            if(!GameMenu.instance.GameOverPanel.activeInHierarchy)
            GameMenu.instance.GameOverPanel.SetActive(true);
        }
        
        GameManager.instance.mmPlayer.PlayFeedbacks();
    }

    public void Heal(int healAmt)
    {
        for(int i = 0; i <= healths.Count; i++)
        {
            if(healths[i].sr.sprite != healths[i].emptyHeart)
            {
                if(healths[i].sr.sprite == healths[i].fullHeart)
                {
                    healths[i].sr.sprite = healths[i].halfHeart;
                    break;
                }else
                {
                    healths[i].sr.sprite = healths[i].emptyHeart;
                    break;
                }
            }
        }
        GameManager.instance.playerStats[0].currentHealth -= healAmt;
        
        GameManager.instance.mmPlayer.PlayFeedbacks();
    }
    public void ResetHearts()
    {
        GameManager.instance.playerStats[0].currentHealth = GameManager.instance.playerStats[0].maxHealth;
        foreach(Health h in healths)
        {
            h.sr.sprite = h.fullHeart;
        }
    }
}
