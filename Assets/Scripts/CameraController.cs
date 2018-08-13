using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CameraController : MonoBehaviour {

	public Transform target;	
	public AudioClip click;

	public Vector3 offset;

	private void LateUpdate()
	{
		Vector3 newPos = target.position + offset;
		newPos.z = transform.position.z;

		transform.position = newPos;
	}	

	public void SwitchPlay(int scene)
	{
		Time.timeScale = 1f;
		SoundManager.instance.PlaySingle(click);
		SceneManager.LoadScene(scene);
	}
}
