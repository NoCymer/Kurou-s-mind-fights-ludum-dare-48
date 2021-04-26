using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    public void TransitionToNextLevel()
	{
		animator.SetTrigger("Transition");
		if (SceneManager.GetActiveScene().buildIndex + 1 > SceneManager.sceneCountInBuildSettings)
		{
			SceneManager.LoadScene(0);
		}
		else
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);	
		}
	}
	public void TransitionToLevel(int buildIndex)
	{
		animator.SetTrigger("Transition");
		SceneManager.LoadScene(buildIndex);
	}
	public void BlankTansition()
	{
		animator.SetTrigger("Transition");
		StartCoroutine(WaitAndTransitionBack());
	}
	IEnumerator WaitAndTransitionBack()
	{
		yield return new WaitForSeconds(1.5f);
		animator.SetTrigger("TransitionBack");
	}

}
