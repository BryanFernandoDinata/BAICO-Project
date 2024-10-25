using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController instance;
    public BossHealthManager bossHealthManager;
    public Animator anim;

    [Header("Trash Throwing")]
    public Collectible[] trashesToThrow; 
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ThrowTrash()
    {
        
    }
}
