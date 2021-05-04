using System;
using System.Collections;
using System.Collections.Generic;
using Thalmic.Myo;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
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
    
    
    // [Header("Myo")]
    // public GameObject myo = null;
    private int magnitude;
    // private ThalmicMyo thalmicMyo;

    public Text lastKey;
    private KeyCode currentKey;

    public static bool canMove = false;
    private bool isFalling;

    private void Awake()
    {
        boy = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Boy");
        // Access the ThalmicMyo component attached to the Myo object.
        // thalmicMyo = myo.GetComponent<ThalmicMyo>();
        lastKey = GameObject.FindGameObjectWithTag("CurrentInput").GetComponent<Text>();
        isFalling = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        BoyMovement();
        CheckCollisionForJump();

    }

    void RunningSound()
    {
        SoundManager.instance.PlaySoundFx(runClip, 0.1f);
    }

    
    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            currentKey = e.keyCode;
        }
    }
    
    private void BoyMovement()
    {
        if (Input.GetKey(currentKey))
        {
            lastKey.text = currentKey + "held";
        }
        else if (Input.GetKeyDown(currentKey))
        {
            lastKey.text = currentKey.ToString();
        }
        else if (Input.GetKeyUp(currentKey))
        {
            lastKey.text = currentKey + "released";
        }
        
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
                isFalling = true;
            }
            else
            {
                isFalling = false;
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

    public void ResetFall()
    {
        anim.SetBool("Fall", false);
    }
    public void ResetJump()
    {
        anim.SetBool("Jump", false);
    } 
    private void CheckCollisionForJump()
    {
        Collider2D bottomHit = Physics2D.OverlapBox(groundCheck.position, range, 0, groundLayer);
        if (bottomHit != null)
        {
            if (Input.GetKeyDown(jump))
            {
                if (bottomHit.gameObject.CompareTag("Ground") && !anim.GetBool("Fall"))
                {
                    Debug.Log("JUmpiing!");
                    // moveInput = facingRight ? 1*moveSpeed : -1 * moveSpeed;
                    boy.velocity = new Vector2( facingRight ?2.3f : -2.3f, jumpForce);
                    // boy.AddForce(new Vector2(boy.velocity.x, jumpForce));
                    // boy.AddTorque(boy.transform.right.y);
                    SoundManager.instance.PlaySoundFx(jumpClip, 0.1f);
                    anim.SetBool("Jump", true);
                }
                Debug.Log(bottomHit.gameObject.CompareTag("Ground") + " and " + anim.GetBool("Fall"));
            }    
            

        }
        else
        {
            if (isFalling)
            {
                ResetJump();
            }
        }
    }
}
