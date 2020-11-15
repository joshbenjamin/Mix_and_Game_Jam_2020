using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    private float currentHealth = 100f;
    private float maxHealth = 100f;

    private float defenseMultiplier = 0.8f;

    public HealthBarController healthBarController;
    public AudioSource hitSound;

    public void TakeDamage(float dmg)
    {
        hitSound.Play();

        currentHealth -= dmg * defenseMultiplier;

        if (currentHealth <= 0f)
        {
            Die();
        }

        healthBarController.ScaleBar(currentHealth);
    }

    void Die()
    {
        int score = GetComponent<ShootController>().GetScore();

        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            Debug.Log("New high score: " + score);
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
        Destroy(this.gameObject);
    }
}
