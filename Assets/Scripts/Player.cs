using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Health health;

    void Start()
    {
        health = new Health(10);
    }

    private void OnGUI()
    {
        health.updateHealthBar(transform.position);
    }

    void takeDamage(int damage)
    {
        health.takeDamage(damage);
    }

    private void OnEnable()
    {
        Goblin.AttackPlayerEvent += takeDamage;
    }

    private void OnDisable()
    {
        Goblin.AttackPlayerEvent -= takeDamage;
    }
}
