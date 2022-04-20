using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    List<GameObject> wheels; //Колёса машины

    GameObject road;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        road = GameObject.FindGameObjectWithTag("Road");
    }

    private void FixedUpdate()
    {
        float sideSpeed = 0f; //Скорость движения вбок
        //К положение по оси Z прибавляется 0.1f, умноженная на боковую скорость 
        transform.position = new Vector3(transform.position.x + 0.1f * sideSpeed, transform.position.y, transform.position.z + speed);

        if (wheels.Count > 0) //Если есть колёса
        {
            foreach (var wheel in wheels)
            {
                wheel.transform.Rotate(-3f, 0f, 0f); //Вращение каждого колеса по оси X
            }
        }

        if (transform.position.y < -50f)
        {
            road.GetComponent<SpawnEnemy2>().enemyCars.Remove(gameObject);
            Destroy(gameObject); //Если это машина NPC, то она будет удаляться со сцены, если упадёт ниже -50f
        }
       
    }
}
