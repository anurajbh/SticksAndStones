using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurn : MonoBehaviour
{
    int index = 0;
    int totalOptions = 4;
    public float xOffset = 239.93f;
    public float yOffset = 130.59f;
    Vector2 position;
    Button hurt;
    Button consume;
    Button protect;
    Button doNothing;
    public bool playerTurn = true;
    


    void Awake()
    {
        hurt = GameObject.Find("Hurt").GetComponent<Button>();
        consume = GameObject.Find("Consume").GetComponent<Button>();
        protect = GameObject.Find("Protect").GetComponent<Button>();
        doNothing = GameObject.Find("Run").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (index < totalOptions / 2)
            {
                index = index + 2;
                position = transform.position;
                position.y -= yOffset;
                transform.position = position;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (index > (totalOptions - 1) / 2)
            {
                index = index - 2;
                position = transform.position;
                position.y += yOffset;
                transform.position = position;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (index % 2 == 0)
            {
                index = index + 1;
                position = transform.position;
                position.x += xOffset;
                transform.position = position;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (index % 2 != 0)
            {
                {
                    index = index - 1;
                    position = transform.position;
                    position.x -= xOffset;
                    transform.position = position;
                }
            }
        }

        switch (index)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    hurt.onClick.Invoke();
                }
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    consume.onClick.Invoke();
                }
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    consume.onClick.Invoke();
                }
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    doNothing.onClick.Invoke();
                }
                break;
            default:
                break;
        }
    }
}
