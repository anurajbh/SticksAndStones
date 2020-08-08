using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMovePoint : MonoBehaviour
{
	void Awake()
	{
		GameObject[] objs = GameObject.FindGameObjectsWithTag("MovePoint");

		if (objs.Length > 1)
		{
			Destroy(this.gameObject);
		} 

		DontDestroyOnLoad(this.gameObject);
	}
}

