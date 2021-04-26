using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    GameManager gameManager;
    GameObject target;
    public List<AudioClip> AudioClips;
    public Transform AttackPoint;
    public float AttackRange = 1.3f;
    public Transform boundaryL;
    public Transform boundaryR;
    Animator animator;
    public int Health = 100;
    public int Damage = 20;
    public float speed = 2f;
    public int currentHealth;
    public float AttackCooldown = 1.5f;
    float Offset;
    bool canAttack = true;
    Rigidbody2D rb;
    float targetX;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        AttackPoint = transform.GetChild(0).transform;
		currentHealth = Health;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
	private void Update()
	{
        target = gameManager.Player;
        if(target !=null)
		{
            targetX = target.transform.position.x;
        
            Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(AttackPoint.position.x, AttackPoint.position.y), AttackRange);
            foreach (Collider2D collider in colliders)
            {
                if (collider != null && collider.tag == "Player")
                {
                    if (canAttack)
                    {
                        canAttack = false;
                        StartCoroutine(Attack());
                    }
                }
            }
		}
    }
	private void FixedUpdate()
	{
        if (isTargetWithinBoundaries())
		{
            Offset = transform.position.x - targetX;
            float Movement = Mathf.Lerp(transform.position.x, targetX, speed * Time.fixedDeltaTime);
            transform.position = new Vector3(Movement, transform.position.y, 0);
            if (Offset > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
        }
		else
		{
            Offset = 0;
		}
        animator.SetFloat("Movement", Offset);

    }

	bool isTargetWithinBoundaries()
	{
        if (targetX < boundaryR.position.x && targetX > boundaryL.position.x)
		{
            return true;
		}
        else { return false; }
	}
	private void OnDrawGizmosSelected()
	{
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
	}
    public void applyDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }
    void Die()
    {
        animator.SetTrigger("IsDead");
        GetComponent<Rigidbody2D>().simulated = false;
        Destroy(gameObject, 1.1f);
        this.enabled = false;
    }
	IEnumerator Attack()
	{
        yield return new WaitForSeconds(0.5f);
        Collider2D[] colliders2 = Physics2D.OverlapCircleAll(new Vector2(AttackPoint.position.x, AttackPoint.position.y), AttackRange);
        foreach (Collider2D collider2 in colliders2)
        {
            if (collider2 != null && collider2.tag == "Player")
            {
                collider2.gameObject.GetComponent<PlayerManager>().applyDamage(Damage);
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
                
            }
        }
        yield return new WaitForSeconds(1.5f);
        canAttack = true;
    }
}
