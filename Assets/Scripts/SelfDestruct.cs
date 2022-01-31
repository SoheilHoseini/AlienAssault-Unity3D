using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    // gameObject is the the current object this script is attached to
    // GameObject is a class and a type of course

    [SerializeField] float delayBeforeDestruction = 3f; 

    void Start()
    {
        Destroy(gameObject, delayBeforeDestruction);//wait a few sec and the destroyes the enemy VFX 
    }


}
