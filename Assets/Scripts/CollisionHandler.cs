using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("Determines how long it takes to reload the current scene")]
    [SerializeField] float sceneLoadDelay = 1f;

    [SerializeField] ParticleSystem explosion;

    //it seems it is better to control hit to othe objects by "Trigger" rather than "Collision"
    //mabe because collision affects the movement of the player but trigger doesn't
    private void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    private void StartCrashSequence()
    {
        MeshRenderer[] rs = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer r in rs)
            r.enabled = false;// disappear the ship and all its children
        explosion.Play();// show explosion VFX (visual FX)
        GetComponent<PlayerControls>().enabled = false;// disable player controls
        GetComponent<BoxCollider>().enabled = false;//so the ship will not crash into another thing after the first crash !
        
        Invoke("ReloadLevel", 1f);// calls the detemined method ater detemined delay
    }

    //reload the current level when the player hits something
    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
