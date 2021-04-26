using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
	public bool isGrounded = false;

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject != transform.gameObject && collision.gameObject != transform.parent.gameObject)
		{
			isGrounded = true;
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject != transform.gameObject && collision.gameObject != transform.parent.gameObject)
		{
			isGrounded = false;
		}
	}
}
