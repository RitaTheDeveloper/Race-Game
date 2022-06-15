using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public float speed = 0f; //Скорость
    public float sideSpeed = 3f; //Боковая скорость
    public bool isPhone; // управление через телефон

    private float maxSpeed; //Максимальная скорость
    private float maxSideSpeed; // 

    private void Start()
    {
        maxSpeed = GetComponent<PlayerParameters>().MaxSpeed;        
    }

    void Update()
    {

        float moveSide;
        if (isPhone)
        {
            Vector3 acceleration = Input.acceleration;

            moveSide = acceleration.x;
        }
        else
        {
            moveSide = Input.GetAxis("Horizontal"); //Когда игрок будет нажимать на стрелочки влево или вправо, сюда будет добавляться 1f или -1f
        }
        //float moveSide = Input.GetAxis("Horizontal"); //Когда игрок будет нажимать на стрелочки влево или вправо, сюда будет добавляться 1f или -1f
        //float moveForward = Input.GetAxis("Vertical"); //То же самое, но со стрелочками вверх и вниз

        //float moveForward = acceleration.y;

        if (moveSide != 0)
        {
            sideSpeed = moveSide * 2f; //Если игрок нажал на стрелочки влево или вправо, задаём боковую скорость
        }

        //if (moveForward != 0)
        //{
        //    speed += 0.01f * moveForward; //Если игрок нажал вверх или вниз
        //}
        //else //Если игрок не нажал ни вверх, ни вниз, то скорость будет постепенно возвращаться к нулю
        //{
            if (speed > 0)
            {
                speed -= 0.01f;
            }
            else
            {
                speed += 0.01f;
            }
        //}

        if (speed > maxSpeed)
        {
            speed = maxSpeed; //Проверка на превышение максимальной скорости
        }

    }

    public void OnClickSpeed()
    {
        speed += 0.2f;
        Debug.Log(speed);
    }
}
