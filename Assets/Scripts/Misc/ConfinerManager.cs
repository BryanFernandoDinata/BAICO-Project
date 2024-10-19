using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfinerManager : MonoBehaviour
{
    public static ConfinerManager instance;
    public Collider2D confiner;

    private void Awake() 
    {
        if(instance == null)
        instance = this;
        else 
        Destroy(gameObject);
    }
}
