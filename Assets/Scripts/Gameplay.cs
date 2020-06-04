using UnityEngine;
using System.Collections;

public class Gameplay : MonoBehaviour {

	public GameObject playerPrefab;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < 3; i++){
			GameObject newDude = Instantiate(playerPrefab, Vector3.right * i * 2, Quaternion.identity) as GameObject;
			if(i==0){
				newDude.name  = Game.current.Elly.name;
			}
			else if(i==1){
				newDude.name  = Game.current.James.name;
			}
			else if(i==2){
				newDude.name  = Game.current.Charlotte.name;
			}
		}
	}

}
