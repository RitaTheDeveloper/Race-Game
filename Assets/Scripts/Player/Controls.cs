using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public float speed = 0f; //Скорость
    public float maxSpeed = 3f; //Максимальная скорость
    public float sideSpeed = 3f; //Боковая скорость

    void Update()
    {
        //float moveSide = Input.GetAxis("Horizontal"); //Когда игрок будет нажимать на стрелочки влево или вправо, сюда будет добавляться 1f или -1f
        //float moveForward = Input.GetAxis("Vertical"); //То же самое, но со стрелочками вверх и вниз

        Vector3 acceleration = Input.acceleration;

        float moveSide = acceleration.x;
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
    }
}
