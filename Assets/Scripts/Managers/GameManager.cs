using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] characters;

    GameObject player;

    private int index;
    private bool SpeedIsPressed = false;

    private void Awake()
    {
        index = PlayerPrefs.GetInt("SelectPlayer");
        Instantiate(characters[index], transform.position, Quaternion.identity);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (SpeedIsPressed)
        {
            OnClickSpeed();
        }
    }

    public void OnClickSpeed()
    {
        player.GetComponent<Controls>().OnClickSpeed();
    }

    public void OnDownSpeed()
    {
        // если кнопка нажата
        SpeedIsPressed = true;
    }

    public void OnUpSpeed()
    {
        // если кнопка отпущена
        SpeedIsPressed = false;
    }
}
