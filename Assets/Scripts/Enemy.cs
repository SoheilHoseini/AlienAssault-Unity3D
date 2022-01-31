using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFXandSFX;//this has to "Play On Awake" when we bring it into exiestance
    [SerializeField] GameObject hitVFX; // little sparks to assure the enemy is hit

    [SerializeField] int scorePerHit = 15;
    [SerializeField] int hitPoints = 4;// original poinst of each enemy

    ScoreBoard scoreBoard;
    GameObject parentOfEnemyVFXs;

    private void Start()
    {
        //instead of serilizing "scoreBoard",we can use this method to access the very first object it finds with this name which is "ScoreBoard" script
        //note that this method should not be used in "Update" cause it creates a lot of overhead and slows the game
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentOfEnemyVFXs = GameObject.FindWithTag("SpawnAtRuntime");//reference to the parent (instead of serializefields and manuallay drag & drop
        AddRigidbody();
    }

    private void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>(); // add a rigidbidy to the current game object at runtime
        rb.useGravity = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints < 1)
        {
            KillEnemy();
        }
    }

    private void ProcessHit()
    {
        GameObject hitVfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        hitVfx.transform.parent = parentOfEnemyVFXs.transform;
        hitPoints --;
        scoreBoard.IncreaseScore(scorePerHit);//increase the score of player

    }

    private void KillEnemy()
    {
        //Instantiate the deathVFX at position of this gameobject (the enemy) with its original rotation
        //with "Instantiate" we can have explosion for all of our enemies without childing the deathVFX for each one :)
        GameObject vfx_sfx = Instantiate(deathVFXandSFX, transform.position, Quaternion.identity);
        vfx_sfx.transform.parent = parentOfEnemyVFXs.transform;// this places the enemy explosions into "Spawn At Runtime"


        Destroy(gameObject);//destroy the entire gameobjact the the bullets hit
    }
}
