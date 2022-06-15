using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoving : MonoBehaviour
{
    public GameObject Menu;
    //[SerializeField]
    public Rigidbody rb;
    [SerializeField]
    GameObject car;

    [SerializeField]
    float speed = 0.2f; //Скорость на старте

    [SerializeField]
    private float minSpeed = 1f; //Минимальная скорость

    float sideSpeed = 0f; //Скорость движения вбок

    [SerializeField]
    GameObject road;

    public GameObject brokenPrefab; //Префаб сломанной машины
    public GameObject modelHolder; //Объект, в который помещается модель

    public Controls control; //Скрипт управления

    private PlayerParameters parameters ; // Параметры игрока

    private bool isAlive = true; //Жива ли машина. Если да, то она будет двигаться
    private bool isKilled = false; //Эта переменная нужна, чтобы триггер сработал только один раз
    private float currentSpeed; // текущая скорость

    public List<GameObject> wheels; //Колёса машины

    void Start()
    {
        parameters = GetComponent<PlayerParameters>();
        rb = GetComponent<Rigidbody>();
        road = GameObject.FindGameObjectWithTag("Road");
    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            currentSpeed = speed; //Скорость движения вперёд


            if (control != null) //Если подключён скрипт управления
            {
                currentSpeed += control.speed; //Изменение скорости
                sideSpeed = control.sideSpeed; //Изменение направления
            }

            //if (newSpeed > maxSpeed)
            //{
            //    newSpeed = maxSpeed; //Проверка на превышение максимальной скорости
            //}

            if (currentSpeed < minSpeed)
            {
                currentSpeed = minSpeed; //Проверка на слишком низкую скорость
            }

            //Изменение положения машины - она двигается вперёд
            //Для этого к её положению по оси X прибавляется новая скорость, положение по Y остаётся прежним
            //К положение по оси Z прибавляется 0.1f, умноженная на боковую скорость 
            transform.position = new Vector3(transform.position.x + 0.1f * sideSpeed, transform.position.y, transform.position.z + currentSpeed);

            if (control != null)
            {
                control.sideSpeed = 0f; //Сброс боковой скорости
            }

            if (wheels.Count > 0) //Если есть колёса
            {
                foreach (var wheel in wheels)
                {
                    wheel.transform.Rotate(-3f, 0f, 0f); //Вращение каждого колеса по оси X
                }
            }

            if (tag == "Car")
            {
                if (transform.position.y < -50f)
                {
                    road.GetComponent<SpawnEnemy2>().enemyCars.Remove(gameObject);
                    Destroy(gameObject); //Если это машина NPC, то она будет удаляться со сцены, если упадёт ниже -50f
                }
            }

        }

    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Car" || other.tag == "Wall") //Если машина игрока столкнулась со стеной или другой машиной
        {
            Debug.Log("машина или стена");
            isAlive = false; //Игрок больше не жив

            if (car != null) //Если есть модель
            {
                if (!isKilled) //Если триггер ещё не сработал
                {
                   // Destroy(car); //Удалить старую модель

                    ////Добавить новую модель
                    //var broken = Instantiate(brokenPrefab, transform.position, Quaternion.Euler(new Vector3(0f, -270f, 0f)));
                    //broken.transform.SetParent(modelHolder.transform);

                    isKilled = true; //Указать, что триггер сработал
                    Menu.SetActive(true);

                }


            }
        }
    }

    public void Accelerate()
    {
        currentSpeed += parameters.Acceleration * Time.deltaTime;

        if (currentSpeed > parameters.MaxSpeed)
        {
            currentSpeed = parameters.MaxSpeed;
        }
    }
}