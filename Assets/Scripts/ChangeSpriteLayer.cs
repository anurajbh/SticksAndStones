using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteLayer : MonoBehaviour
{
	private const string INITIAL_LAYER_NAME = "Player";
	private const string CHANGED_LAYER_NAME = "Default";
	private int sortingOrder = 2;
	private SpriteRenderer sprite;
	private float yvalue;
	public GameObject player;

    void Awake()
    {
		sprite = GetComponent<SpriteRenderer>();
		sprite.sortingOrder = sortingOrder;
		sprite.sortingLayerName = INITIAL_LAYER_NAME;
		yvalue = GetComponent<Transform>().position.y;
		player = GameObject.Find("Elle");
    }
		
    void Update()
    {
		if (player.GetComponent<Transform>().position.y > yvalue)
		{
			sprite.sortingLayerName = INITIAL_LAYER_NAME;
		} else 
		{
			sprite.sortingLayerName = CHANGED_LAYER_NAME;
		}
    }
}
