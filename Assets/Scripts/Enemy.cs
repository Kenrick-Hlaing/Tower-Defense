using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    public float startHealth = 100f;
    public int worth = 50;
    public GameObject deathEffect;

    private float health;

    [Header("Unity Stuff")]
    public Image healthBar;

    private bool isDead = false;

    [HideInInspector]
    public float speed;

    public void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if(health <= 0 && !isDead){
            Die();
        }
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    void Die()
    {
        isDead = true;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        PlayerStats.Money += worth;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
        Destroy(effect, 5f);
    }
}
