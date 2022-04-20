using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    GameObject[] enemyCarsPrefabs;

    [SerializeField]
    float timeToAddNewCar; // частота добавления новой машины

    [SerializeField]
    GameObject player;

    [SerializeField]
    float distanceFromPlayer; // на какой дистанции от игрока начинать создавать машины

    [SerializeField]
    float distanseErrorZ = 3f; // минимальное расстояние между вражескими машинами по Z, чтобы они не столкнулись

    [SerializeField]
    float distanseErrorX; // минимальное расстояние между вражескими машинами по X, чтобы они не столкнулись

    float timer;
    public List<GameObject> enemyCars;


    private void Start()
    {
        timer = 0f;
        enemyCars = new List<GameObject>();
    }
    private void FixedUpdate()
    {
        //Добавление вражеских машин
        timer += Time.deltaTime;
        if (timer >= timeToAddNewCar)
        {
            // выбор стороны
            float side = Random.Range(1, 3) == 1 ? 1f : -1f;

            //выбор машины
            int numberOfCarsPrefabs = Random.Range(0, enemyCarsPrefabs.Length);

            GameObject car;

            if(enemyCars.Count > 0)
            {
                // делаем проверку перед созданием машины на пути, есть ли там уже машина. Если есть, то машину не создаем
                if (CanInstantiateCarHere(player.transform.position.z + distanceFromPlayer))
                {
                    car = Instantiate(enemyCarsPrefabs[numberOfCarsPrefabs], new Vector3(2.4f * side, enemyCarsPrefabs[numberOfCarsPrefabs].transform.position.y + 0.2f, player.transform.position.z + distanceFromPlayer), Quaternion.identity);
                    car.transform.SetParent(gameObject.transform); //Добавление машины в объект Road 
                    enemyCars.Add(car);
                    timer = 0f;
                }
            }
            else
            {
                //добавление первой вражеской машины на дорогу
                car = Instantiate(enemyCarsPrefabs[numberOfCarsPrefabs], new Vector3(2.4f * side, enemyCarsPrefabs[numberOfCarsPrefabs].transform.position.y + 0.2f, player.transform.position.z + distanceFromPlayer), Quaternion.identity);
                car.transform.SetParent(gameObject.transform); //Добавление машины в объект Road 
                enemyCars.Add(car);
                timer = 0f;
            }
            
        }

    }

    private bool CanInstantiateCarHere(float z)
    {
        bool result = true;

        for (int i = 0; i < enemyCars.Count; i++)
        {
            if (enemyCars[i].GetComponent<Rigidbody>().position.z < z + distanseErrorZ && enemyCars[i].GetComponent<Rigidbody>().position.z >= z - distanseErrorZ)
            {
                result = false;
            }
        }

        return result;
    }
}
