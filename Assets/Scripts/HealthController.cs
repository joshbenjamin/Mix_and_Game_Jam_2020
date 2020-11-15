using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    private float currentHealth = 110f;
    private float maxHealth = 110f;

    private float defenseMultiplier = 1f;

    private float theirScore = 0f;

    public HealthBarController healthBarController;
    public AudioSource hitSound;

    public TextController textController;

    public void TakeDamage(float dmg)
    {
        hitSound.Play();

        currentHealth -= dmg * defenseMultiplier;

        theirScore += 1;

        if (currentHealth <= 0f || theirScore == 11)
        {
            Die();
        }

        healthBarController.ScaleBar(currentHealth);

        textController.TheyScore();
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
