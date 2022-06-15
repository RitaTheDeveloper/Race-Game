using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelect : MonoBehaviour
{
    private GameObject[] characters;
    private int index;

    private void Start()
    {
        index = PlayerPrefs.GetInt("SelectPlayer");
        // массив будет состоять из дочернних объектов
        characters = new GameObject[transform.childCount];

        for(int i = 0; i < transform.childCount; i++)
        {
            characters[i] = transform.GetChild(i).gameObject;
        }

        // выключаем видимость невыбранных объектов
        foreach(GameObject go in characters)
        {
            go.SetActive(false);
        }

        //включаем нужный индекс
        if (characters[index])
        {
            characters[index].SetActive(true);
        }

    }

    private void Update()
    {
        transform.Rotate(10 * Vector3.up * Time.deltaTime); // вращение детали
    }

    public void SelectLeft()
    {
        characters[index].SetActive(false);
        index--;
        
        if(index < 0)
        {
            index = characters.Length - 1;
        }

        characters[index].SetActive(true);
    }

    public void SelectRight()
    {
        characters[index].SetActive(false);
        index++;

        if (index == characters.Length)
        {
            index = 0;
        }

        characters[index].SetActive(true);
    }

    public void StartScene()
    {
        PlayerPrefs.SetInt("SelectPlayer", index);
        SceneManager.LoadScene("TestScene 1");
    }

}
