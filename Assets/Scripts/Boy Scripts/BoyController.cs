using System;
using System.Collections;
using System.Collections.Generic;
using Thalmic.Myo;
using UnityEngine;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using Quaternion = UnityEngine.Quaternion;
using UnlockType = Thalmic.Myo.UnlockType;
using Vector3 = UnityEngine.Vector3;
using VibrationType = Thalmic.Myo.VibrationType;

public class BoyController : MonoBehaviour {

    [Header("Movement")]
    private Rigidbody2D boy;
    public float moveSpeed;
    public float jumpForce;
    private float jumpHeight = .4f;
    private float moveInput;

    private bool facingRight = true;
    private bool grounded;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public Vector3 range;
    private Animator anim;

    [Header("Sounds")]
    public AudioClip jumpClip;
    public AudioClip runClip;
    private GameObject player;

    [Header("Input Controls")] 
    public KeyCode right = KeyCode.D;
    public KeyCode left  = KeyCode.A;
    public KeyCode up  = KeyCode.W;
    public KeyCode down  = KeyCode.S;
    public KeyCode jump  = KeyCode.Space;
    
    
    [Header("Myo")]
    public GameObject myo = null;
    private Pose _lastPose = Pose.Unknown;
    private int magnitude;
    private ThalmicMyo thalmicMyo;


    public static bool canMove = false;
    private void Awake()
    {
        boy = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Boy");
        // Access the ThalmicMyo component attached to the Myo object.
        thalmicMyo = myo.GetComponent<ThalmicMyo>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        BoyMovement();
        CheckCollisionForJump();
    }
    // Extend the unlock if ThalmcHub's locking policy is standard, and notifies the given myo that a user action was
    // recognized.
    void ExtendUnlockAndNotifyUserAction(ThalmicMyo myo)
    {
        ThalmicHub hub = ThalmicHub.instance;

        if (hub.lockingPolicy == LockingPolicy.Standard)
        {
            myo.Unlock(UnlockType.Timed);
        }

        myo.NotifyUserAction();
    }
    void RunningSound()
    {
        SoundManager.instance.PlaySoundFx(runClip, 0.1f);
    }
    private void BoyMyoMovement()
    {
        //TODO: do this
        if (thalmicMyo.pose != _lastPose)
        {
            _lastPose = thalmicMyo.pose;

            if (thalmicMyo.pose == Pose.FingersSpread)
            {
                ExtendUnlockAndNotifyUserAction(thalmicMyo);
                print("Pose ----   Fingers spread");
            }
            else if (thalmicMyo.pose == Pose.Fist)
            {
                print("Pose ----   Fist");
                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
            else if ( thalmicMyo.pose == Pose.DoubleTap)
            {
                print("Pose ----   Double tap");
                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
            else if ( thalmicMyo.pose == Pose.WaveIn)
            {
                print("Pose ----   Wave in");
                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
            else if ( thalmicMyo.pose == Pose.WaveOut)
            {
                print("Pose ----   Wave out");
                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
        }
        if (thalmicMyo.xDirection == Thalmic.Myo.XDirection.TowardWrist)
        {
            // Mirror the rotation around the XZ plane in Unity's coordinate system (XY plane in Myo's coordinate
            // system). This makes the rotation reflect the arm's orientation, rather than that of the Myo armband.
            transform.rotation = new Quaternion(transform.localRotation.x,
                                                -transform.localRotation.y,
                                                transform.localRotation.z,
                                                -transform.localRotation.w);
        }
        if (thalmicMyo.armSynced)
        {
            if (thalmicMyo.gyroscope.y < 0.33f)
            {
                moveInput = -1 * moveSpeed;
            }
            else
            {
                moveInput = 1 * moveSpeed;
            }
        }
        else
        {
            Debug.Log("Arm not synced!");
        }
        if (!GameplayManager.doorOpen)
        {
            anim.SetFloat("Speed", Mathf.Abs(moveInput));

        }
        else
        {
            anim.SetFloat("Speed", 0);
            boy.velocity = new Vector2(0, 0);
            boy.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        //Moving
        boy.velocity = new Vector2(moveInput, boy.velocity.y);

        jumpHeight = (thalmicMyo.gyroscope.magnitude/100);
        //Jumping
        if (Input.GetKeyUp(KeyCode.Space)  && !GameplayManager.doorOpen)
        {
            if (boy.velocity.y > 0)
            {
                boy.velocity = new Vector2(boy.velocity.x, boy.velocity.y * jumpHeight);
            }
        }
        //Flipping horizontally
        if (moveInput > 0 && !facingRight || moveInput < 0 && facingRight)
        {
            if (!GameplayManager.doorOpen)
            {
                Flip();
            }
        }
        //Falling
        if (boy.velocity.y < -1)
        {
            anim.SetBool("Fall", true);
        }
        else
        {
            anim.SetBool("Fall", false);
        }
    }

    private void BoyMovement()
    {
        if (canMove)
        {
            // Flip player according to foot rotation
            // moveInput = thalmicMyo.transform.rotation.z < 0 ? -1 * moveSpeed : 1 * moveSpeed;
            // boy.velocity = new Vector2(boy.vel, boy.velocity.y);
            moveInput = 0;
            if (Input.GetKey(right))
            {
                Debug.Log("moving right!");
                //Moving
                moveInput = 1 * moveSpeed;
                boy.velocity = new Vector2( moveInput, boy.velocity.y);

            }
            else if (Input.GetKey(left))
            {
                Debug.Log("moving left!");
                //Moving
                moveInput = -1 * moveSpeed;
                boy.velocity = new Vector2( moveInput, boy.velocity.y);
            }
            if (!GameplayManager.doorOpen)
            {
                anim.SetFloat("Speed", Mathf.Abs(moveInput));
            }
            else
            {
                anim.SetFloat("Speed", 0);
                boy.velocity = new Vector2(0, 0);
                boy.constraints = RigidbodyConstraints2D.FreezeAll;
            }

        
            //Jumping
            // if (Input.GetKeyUp(jump)  && !GameplayManager.doorOpen)
            // {
            //     if (boy.velocity.y > 0)
            //     {
            //         boy.velocity = new Vector2(boy.velocity.x, boy.velocity.y * jumpHeight);
            //     }
            // }
            //Flipping horizontally
            if (moveInput > 0 && !facingRight || moveInput < 0 && facingRight)
            {
                if (!GameplayManager.doorOpen)
                {
                    Flip();
                }
            }
            //Falling
            if (boy.velocity.y < -1)
            {
                anim.SetBool("Fall", true);
            }
            else
            {
                anim.SetBool("Fall", false);
            }
        }
        
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 transformScale = transform.localScale;
        transformScale.x *= -1;
        transform.localScale = transformScale;
    }

    private void CheckCollisionForJump()
    {
        Collider2D bottomHit = Physics2D.OverlapBox(groundCheck.position, range, 0, groundLayer);
        if (bottomHit != null)
        {
            if (bottomHit.gameObject.CompareTag("Ground") && Input.GetKeyDown(jump) && !anim.GetBool("Fall"))
            {
                // moveInput = facingRight ? 1*moveSpeed : -1 * moveSpeed;
                boy.velocity = new Vector2( facingRight ?2.3f : -2.3f, jumpForce);
                // boy.AddForce(new Vector2(boy.velocity.x, jumpForce));
                // boy.AddTorque(boy.transform.right.y);
                SoundManager.instance.PlaySoundFx(jumpClip, 0.1f);
                anim.SetBool("Jump", true);
            }
            else
            {
                anim.SetBool("Jump", false);
            }
        }
    }
}
