using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	public static SoundManager instance;
	
	public AudioSource engene;
	public AudioSource simple;
	// Use this for initialization
	private void Awake()
	{
		if(instance == null) instance = this;
		else if (instance != this) Destroy(gameObject);
		DontDestroyOnLoad(gameObject);		
	}

	public void PlaySingle(AudioClip clip)
	{
		simple.clip = clip;
		simple.Play();
	}



}
