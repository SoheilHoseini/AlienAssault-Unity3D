using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // according to https://docs.unity3d.com/Manual/ExecutionOrder.html
    // the first execution is "Awake" so we use it to avoid replaying the theme music  each time the game restarts
    private void Awake()
    {
        // if number of instantiated musics if more than 1, we destroy the rest
        int numOfMUsicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        if (numOfMUsicPlayers > 1)
        {
            Destroy(gameObject);
        }

        //if the number of musics goes under 2, should not be destroyed
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
