using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy2 : MonoBehaviour
{
    [SerializeField]
    GameObject[] enemyCarsPrefabs_1; // машины движущиеся в твою сторону

    [SerializeField]
    GameObject[] enemyCarsPrefabs_2; // машины, движущиеся по встречке

    [SerializeField]
    float timeToAddNewCar; // частота добавления новой машины

    [SerializeField]
    GameObject player;

    [SerializeField]
    float distanceFromPlayer; // на какой дистанции от игрока начинать создавать машины

    [SerializeField]
    float distanseErrorZ = 3f; // минимальное расстояние между вражескими машинами по Z, чтобы они не столкнулись

    [SerializeField]
    float distanseErrorX = 1.5f; // минимальное расстояние между вражескими машинами по X, чтобы они не столкнулись

    [SerializeField]
    float sideOfRoadX_1, sideOfRoadX_2, sideOfRoadX_3, sideOfRoadX_4; // координатЫ по х, по которой будет ехать добавленная вражеская машина

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
            // выбор стороны дороги: встречная или твоя
            int road = Random.Range(1, 3) == 1 ? -1 : 1;

            float side;
            int numberOfCarsPrefabs;
            GameObject car;
            GameObject prefab;

            if (road == 1)
            {
                // выбор стороны
                side = Random.Range(1, 3) == 1 ? sideOfRoadX_1 : sideOfRoadX_2; // выбираем рандомно координату по Х

                //выбор машины
                numberOfCarsPrefabs = Random.Range(0, enemyCarsPrefabs_1.Length);

                //выбор из списка префабов
                prefab = enemyCarsPrefabs_1[numberOfCarsPrefabs];

            }
            else
            {
                side = Random.Range(1, 3) == 1 ? sideOfRoadX_3 : sideOfRoadX_4; // выбираем рандомно координату по Х
                //выбор машины
                numberOfCarsPrefabs = Random.Range(0, enemyCarsPrefabs_2.Length);
                prefab = enemyCarsPrefabs_2[numberOfCarsPrefabs];
            }         

            if(enemyCars.Count > 0)
            {
                // делаем проверку перед созданием машины на пути, есть ли там уже машина. Если есть, то машину не создаем
                if (CanInstantiateCarHere(player.transform.position.z + distanceFromPlayer, side))
                {
                    car = Instantiate(prefab, new Vector3(side, prefab.transform.position.y + 0.2f, player.transform.position.z + distanceFromPlayer), prefab.transform.rotation);
                    car.transform.SetParent(gameObject.transform); //Добавление машины в объект Road 
                    enemyCars.Add(car);
                    timer = 0f;
                }
            }
            else
            {
                //добавление первой вражеской машины на дорогу
                car = Instantiate(prefab, new Vector3(side, prefab.transform.position.y + 0.2f, player.transform.position.z + distanceFromPlayer), prefab.transform.rotation);
                car.transform.SetParent(gameObject.transform); //Добавление машины в объект Road 
                enemyCars.Add(car);
                timer = 0f;
            }
            
        }

    }

    private bool CanInstantiateCarHere(float z, float x)
    {
        bool result = true;

        for (int i = 0; i < enemyCars.Count; i++)
        {
            if (enemyCars[i].GetComponent<Rigidbody>().position.z < z + distanseErrorZ && enemyCars[i].GetComponent<Rigidbody>().position.z >= z - distanseErrorZ)
            {
                if(enemyCars[i].GetComponent<Rigidbody>().position.x < x + distanseErrorX && enemyCars[i].GetComponent<Rigidbody>().position.x >= x - distanseErrorX)
                {
                    result = false;
                }
            }
        }

        return result;
    }

    private void InstantiateEnemyCar()
    {

    }
}
