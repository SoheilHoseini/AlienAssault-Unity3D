using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    //[SerializeField] InputAction movement;

    [Header("General Setup Settings")]// help to remember what the heck is giong on in the future
    [Tooltip("How fast ship moves up and down based upon player input")] [SerializeField] float controlSpeed = 10f;// just hold the mouse cursur to see the explanation
    [Tooltip("How far player moves horizontaly")][SerializeField] float windowXRange = 20f;
    [Tooltip("How far player moves vartically")] [SerializeField] float windowYRange = 15f;

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;

    [Header("Player position based tuning")]
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float controlRollFactor = -45f;

    [Header("Laser gun array")]
    [Tooltip("Add all player lasers here")]
    [SerializeField] GameObject[] lasers;
    readonly AudioSource audioSource;
    [SerializeField] AudioClip shootingSFX;

    float xThrow, yThrow;

    //enable the movement 
    private void OnEnable()
    {
        //movement.Enable();
    }

    //disable the movement 
    private void OnDisable()
    {
        //movement.Disable();
    }


    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessTranslation()
    {
        ////the new input system (Input System) ***********************************

        //float horizontalThrow = movement.ReadValue<Vector2>().x;
        //Debug.Log(horizontalThrow);
        //float verticalalThrow = movement.ReadValue<Vector2>().y;
        //Debug.Log(verticalalThrow);




        //the old input system (Input Manager) *************************************

        //returns a decimal number in range (-1, 1)
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        //I think local position is relative to the Play Rig game object
        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -windowXRange, +windowXRange);//returns the given value if it is in the range to keep the player in the framework

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -windowYRange, +windowYRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float pitchDueToPosistion = transform.localPosition.y * positionPitchFactor;//creats a realation between y of the plane and the rotation around y that should have
        float pitchDueToControlThrow = yThrow * controlPitchFactor;//Think this is just to increase the rotation somehow!

        //it matters if we rotate in the y axis first and then x, or vice versa. so we use the "Quaternion" struct
        //these are the name of x , y , z axes i guess
        float pitch = pitchDueToPosistion + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;//going left and right doesn't need the nose of the plane to go that way
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessFiring()
    {
        //if(Input.GetButton("Fire1"))
        if(Input.GetKey(KeyCode.Mouse0))
        {
            //if (!audioSource.isPlaying)
            //{
            //    audioSource.PlayOneShot(shootingSFX);
            //}
            SetLaserActive(true);//Ctrl + R + M => extract method
                                 //Ctrl + . => generate a method of the red underlined code
        }
        else
        {
            //audioSource.Stop();
            SetLaserActive(false);
        }
    }

    private void SetLaserActive(bool info)
    {

        foreach(GameObject laser in lasers)
        {
            
            //if we deactive the hole laser GameObject, the bullets will disappear at the moment we dont push the button but they should live after shooting
            var emmissionModule = laser.GetComponent<ParticleSystem>().emission;
            emmissionModule.enabled = info;
            

        }
    }

}
