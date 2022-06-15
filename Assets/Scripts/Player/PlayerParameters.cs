using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParameters : MonoBehaviour
{
    [SerializeField]
    [Header("Ускорение")]
    private float acceleration; //При зажатой кнопке ускорения скорость авто каждый кадр увеличивается на Acceleration * time.deltaTime
    public float Acceleration
    {
        get { return acceleration; }
    }

    [SerializeField]
    [Header("Скорость торможения")]
    private float brakeDeceleration; //При зажатой кнопке тормоза скорость авто каждый кадр падает на BrakeDeceleration* time.deltaTime
    public float BrakeDeceleration
    {
        get { return brakeDeceleration; }
    }

    [SerializeField]
    [Header("Максимальная скорость")]
    private float maxSpeed; //Скорость авто не может превышать это значений.
    public float MaxSpeed
    {
        get { return maxSpeed; }
    }

    [SerializeField]
    [Header("Максимальная скорость смещения")]
    private float maxSideSpeed; //Скорость смещения между полосами не может превышать это значение.
    public float MaxSideSpeed
    {
        get { return maxSideSpeed; }
    }

    [SerializeField]
    [Header("Скорость падения скорости")]
    private float standartDeceleration; //При отпущенных обоих кнопках скорость авто каждый кадр падает на StandartDeceleration* time.deltaTime
    public float StandartDeceleration
    {
        get { return standartDeceleration; }
    }

    [SerializeField]
    [Header("Кол-во здоровья")]
    private float maxHealth;
    public float MaxHealth
    {
        get { return maxHealth; }
    }
}
