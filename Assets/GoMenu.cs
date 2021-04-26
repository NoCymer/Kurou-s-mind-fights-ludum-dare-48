using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoMenu : MonoBehaviour
{
	public LevelChanger lvlChanger;
	private void Start()
	{
		StartCoroutine(levelChanger());
		PlayerPrefs.SetInt("NumberOfDeath", 0);
	}
	IEnumerator levelChanger()
	{
		yield return new WaitForSeconds(3);
		lvlChanger.TransitionToLevel(0);
	}
}
