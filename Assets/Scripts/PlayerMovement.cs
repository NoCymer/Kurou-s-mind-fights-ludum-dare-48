using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public List<AudioClip> AudioClips;
    [HideInInspector]
    public float InputX;
    public float SpeedMultiplicator = 1;
    [HideInInspector]
    public float SpeedVal = 4.5f;
    public float JumpMultiplicator = 1;
    public float JumpVal = 400f;
    [HideInInspector]
    public bool CanMove = true;
    [HideInInspector]
    public bool IsControled = false;
    public SpriteRenderer swordSprite;
    public GroundChecker groundChecker;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        if (groundChecker == null) { groundChecker = GetComponentInChildren<GroundChecker>(); }
        if (groundChecker == null) { Debug.LogError("No Ground Checker Has been fond on the player error could occur"); }
    }

    private void Update()
    {
        if (CanMove && !IsControled)
        {
            InputX = Input.GetAxis("Horizontal");
            if (Input.GetKeyDown(KeyCode.Space) && groundChecker.isGrounded)
            {
                Jump();
            }
        } 
        else if (!groundChecker.isGrounded && !CanMove && !IsControled)
        {
            InputX = Input.GetAxis("Horizontal");
        }
        else if (groundChecker.isGrounded && !CanMove && !IsControled)
        {
            InputX = 0f;
        }
    }
    void FixedUpdate()
    {
        animator.SetFloat("InputX", InputX);
        float MovementDirection = InputX * SpeedMultiplicator * Time.fixedDeltaTime * SpeedVal;
        GetComponent<Transform>().position += new Vector3(MovementDirection, 0, 0);
        if (InputX < 0)
		{
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else
		{
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
    void Jump()
    {
        AudioSource AudioSource = GetComponent<AudioSource>();
        int tmp = Random.Range(0, 3);
        switch (tmp)
		{
            case 0:
                AudioSource.PlayOneShot(AudioClips[tmp]);
                break;
            case 1:
                AudioSource.PlayOneShot(AudioClips[tmp]);
                break;
            case 2:
                AudioSource.PlayOneShot(AudioClips[tmp]);
                break;
            default:
                break;
        }
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpVal * JumpMultiplicator));
        animator.SetTrigger("Jump");
    }
}