using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour {

    public string charName;
    public int maxHealth = 3;
    [HideInInspector] public int currentHealth;

    [Header("Minigames Buff")]
    public int maximumCollectTrashAmt = 2;
}
