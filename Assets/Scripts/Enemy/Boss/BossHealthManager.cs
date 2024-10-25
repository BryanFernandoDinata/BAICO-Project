using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum bossStage
{
    stage1, stage2
}
public enum bossActions
{
    intro, 
    idle, 
    throwTrash
}
public class BossHealthManager : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth;
    int currentHealth;

    [Header("Actions")]
    public bossStage currentBossStage;
    public bossActions currentBossAction;

    // Update is called once per frame
    void Update()
    {
        CheckBossStage();
    }
    void CheckBossStage()
    {
        if(currentHealth <= (maxHealth / 2))
        {
            currentBossStage = bossStage.stage2;
        }
    }
}
