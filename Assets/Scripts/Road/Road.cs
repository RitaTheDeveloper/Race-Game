using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public List<GameObject> blocks; //Коллекция всех дорожных блоков
    public GameObject roadPrefab; //Префаб дорожного блока
    public float lengthBlock = 66.5f; //длина блока

    GameObject player;

    private System.Random rand = new System.Random(); //Генератор случайных чисел

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void FixedUpdate()
    {
        float z = player.GetComponent<PlayerMoving>().rb.position.z; //Получение положения игрока
        Debug.Log("player.z = " + z);

        var last = blocks[blocks.Count - 1]; //Номер дорожного блока, который дальше всех от игрока

        if (z > last.transform.position.z - lengthBlock * 10f) //Если игрок подъехал к последнему блоку ближе, чем на 10 блоков
        {
            //Инстанцирование нового блока
            var block = Instantiate(roadPrefab, new Vector3(last.transform.position.x, last.transform.position.y, last.transform.position.z + lengthBlock), Quaternion.identity);
            block.transform.SetParent(gameObject.transform); //Перемещение блока в объект Road
            blocks.Add(block); //Добавление блока в коллекцию

        }       
                

        foreach (GameObject block in blocks)
        {
            bool fetched = block.GetComponent<RoadBlock>().Fetch(z); //Проверка, проехал ли игрок этот блок

            if (fetched) //Если проехал
            {
                blocks.Remove(block); //Удаление блока из коллекции
                block.GetComponent<RoadBlock>().Delete(); //Удаление блока со сцены
            }
        }
    }
}
