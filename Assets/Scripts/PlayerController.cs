using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    Rigidbody2D rigidBody2D;

    public float pullForce = 100f;
    public float rotateSpeed = 360f;
    private float startRotateSpeed;

    private Tower hookedTower;

    private bool isPulled = false;
    private bool isCrashed = false;

    private UIController uIController;
    private AudioSource myAudio;
    private Vector2 startPosition;
    private Quaternion startRotation;

    // Start is called before the first frame update
    void Start()
    {
        InisializationStart();
        GetGameComponent();
        FindGameObject();
    }
    private void InisializationStart()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        startRotateSpeed = rotateSpeed;
    }
    private void GetGameComponent()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        myAudio = GetComponent<AudioSource>();
    }
    private void FindGameObject()
    {
        uIController = GameObject.Find("Game Canvas").GetComponent<UIController>();
    }
    // Update is called once per frame
    void Update()
    {
        //Move the object for each frame
        rigidBody2D.velocity = -transform.up * moveSpeed;
        //Restart the player when dead
        RestartWhenDead();
    }
    private void RestartWhenDead()
    {
        if (isCrashed)
        {
            if (!myAudio.isPlaying)
            {
                RestartPosition();
            }
        }
    }
    public void ReleaseHooked()
    {
        isPulled = false;
        hookedTower = null;
        rigidBody2D.angularVelocity = 0f;
        rotateSpeed = startRotateSpeed;
    }
    public void HookedTower(Tower tower)
    {
        hookedTower = tower;
        if (hookedTower)
        {
            // Get distance from player to tower when hook;
            float distance = Vector2.Distance(transform.position, hookedTower.transform.position);

            //Gravitation toward tower
            Vector3 pullDirection = (hookedTower.transform.position - transform.position).normalized;
            float newPullForce = Mathf.Clamp(pullForce / distance, 20, 50);
            rigidBody2D.AddForce(pullDirection * newPullForce);

            if (hookedTower.hookClockWise)
            {
                rotateSpeed *= -1;
            }
            //Angular Velocity
            rigidBody2D.angularVelocity = rotateSpeed / distance;

            isPulled = true;
        }
    }
    private void RestartPosition()
    {
        ReleaseHooked();
        //Restart Rotation
        transform.rotation = startRotation;
        //Set to start position
        transform.position = startPosition;
        //Set isCrashed to false
        isCrashed = false;
    }
    private void CrashPlayer()
    {
        //Play SFX
        myAudio.Play();
        //Freeze Player
        rigidBody2D.velocity = new Vector2(0f, 0f);
        rigidBody2D.angularDrag = 0f;
        isCrashed = true;
        /*uIController.failedGame(); //Show panel fail*/
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            CrashPlayer();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            gameObject.SetActive(false);
            uIController.EndGame();
        }
    }
}
