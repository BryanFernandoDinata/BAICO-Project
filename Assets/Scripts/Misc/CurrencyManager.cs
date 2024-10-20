using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    public int currentGold = 0;
    private void Awake() 
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start() 
    {
        if(PlayerPrefs.HasKey("Gold"))
        {
            currentGold = PlayerPrefs.GetInt("Gold");
            GameMenu.instance.coinText.text = currentGold.ToString();
        }
    }
    public void GetGold(int amt)
    {
        currentGold += amt;
        GameMenu.instance.coinText.text = currentGold.ToString();
    }
    public void UseGold(int amt)
    {
        currentGold -= amt;
        GameMenu.instance.coinText.text = currentGold.ToString();
    }
}
