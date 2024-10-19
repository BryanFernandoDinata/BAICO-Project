using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleAnimationTrigger : MonoBehaviour
{
    [SerializeField] GameObject parentObject;
    public void SetGameObjectNotActive()
    {
        parentObject.SetActive(false);
    }
}
