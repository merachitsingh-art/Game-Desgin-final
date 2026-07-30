using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth = 100f; 

    [Header("Heal Settings")]
    public float healAmount = 30f;
    public float healCooldown = 15f;
    private float nextHealTime = 0f;

    [Header("I-Frame Settings (Prevents Instant Death)")]
    public float iframeDuration = 1.0f;
    public float flashInterval = 0.1f;
    private bool isInvulnerable = false;
    private SpriteRenderer spriteRenderer;

    [Header("UI Setup")]
    public Slider healthSlider;
    public GameObject deathScreenUI;

    private Vector3 spawnPoint;

    void Awake()
    {
      
        currentHealth = maxHealth;
        
        if (deathScreenUI != null)
        {
            deathScreenUI.SetActive(false); 
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();

        spawnPoint = transform.position;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        if (deathScreenUI != null)
        {
            deathScreenUI.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (Time.time >= nextHealTime)
            {
                if (currentHealth < maxHealth)
                {
                    Heal(healAmount);
                    nextHealTime = Time.time + healCooldown;
                }
                else
                {
                    Debug.Log("Already at full health!");
                }
            }
            else
            {
                float timeRemaining = Mathf.Ceil(nextHealTime - Time.time);
                Debug.Log("Heal on cooldown! Wait " + timeRemaining + " seconds.");
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (isInvulnerable || currentHealth <= 0) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateUI();

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(IFrameRoutine());
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateUI();
        Debug.Log("Player healed! Current Health: " + currentHealth);
    }

    void UpdateUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }

    private System.Collections.IEnumerator IFrameRoutine()
    {
        isInvulnerable = true;
        float timer = 0f;

        while (timer < iframeDuration)
        {
            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a = (color.a == 1.0f) ? 0.2f : 1.0f;
                spriteRenderer.color = color;
            }
            yield return new WaitForSeconds(flashInterval);
            timer += flashInterval;
        }

        if (spriteRenderer != null)
        {
            Color finalColor = spriteRenderer.color;
            finalColor.a = 1.0f;
            spriteRenderer.color = finalColor;
        }

        isInvulnerable = false;
    }

    void Die()
    {
        Debug.Log("Player died!");
        
        if (deathScreenUI != null)
        {
            deathScreenUI.SetActive(true);
        }
        
        Time.timeScale = 0f;
    }

    public void Respawn()
    {
        Time.timeScale = 1f;
        currentHealth = maxHealth;
        
        transform.position = spawnPoint;

        isInvulnerable = false;

        if (spriteRenderer != null)
        {
            Color c = spriteRenderer.color;
            c.a = 1.0f;
            spriteRenderer.color = c;
        }

        UpdateUI();

        if (deathScreenUI != null)
        {
            deathScreenUI.SetActive(false);
        }
    }
}