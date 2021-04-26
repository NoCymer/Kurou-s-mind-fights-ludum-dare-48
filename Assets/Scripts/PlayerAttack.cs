using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform AttackPoint;
    public List<AudioClip> AudioClips;
    public GameObject Sword;
    public float AttackRange = 1.5f;
    bool canAttack = true;
    bool canFake = true;
    bool isAttacking = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            Attack();
		}
    }
    void Attack()
	{
        if(canFake)
		{
            canFake = false;
            Sword.GetComponent<SpriteRenderer>().enabled = true;
            Sword.GetComponent<Animation>().Play();
            StartCoroutine(FakeCooldown());
        }
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(AttackPoint.position.x, AttackPoint.position.y), AttackRange);
        foreach (Collider2D collider in colliders)
		{
            if (collider != null && collider.tag == "Ennemy")
			{
                if (canAttack)
                {
                    isAttacking = true;
                    collider.transform.GetComponent<Ennemy>().applyDamage(20);
                    canAttack = false;
                    AudioSource AudioSource = GetComponent<AudioSource>();
                    Sword.GetComponent<Animation>().Play();
                    if (AudioClips.Count > 0)
					{
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
                    StartCoroutine(Cooldown());
                }
                
            }
		}
	}
    IEnumerator FakeCooldown()
	{
        yield return new WaitForSeconds(1.5f);
        if(!isAttacking) 
        {
            Sword.GetComponent<SpriteRenderer>().enabled = false;  
        }
        canFake = true;
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1.5f);
        Sword.GetComponent<SpriteRenderer>().enabled = false;
        isAttacking = false;
        canAttack = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
