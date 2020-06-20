using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyBlackout : MonoBehaviour
{
	public static DontDestroyBlackout Instance { get; private set; } // this class is a singleton

	void Awake() 
	{
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}

}
