using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 500;

    public static int Lives;
    public int startLives = 100;

    void Start()
    {
        Money = startMoney;
        Lives = startLives;
       // Debug.Log(Money);
    }

    public void AddMoney(int money)
    {
        Money += money;
       // Debug.Log(money);
    }
}
