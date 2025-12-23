using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossHP : MonoBehaviour
{
    [Header("HP set")]
    [SerializeField] private float maxHealth = 1000f;
    [SerializeField] private float currentHealth;

    [Header("HP Slider")]
    [SerializeField] private Slider hpSlider;

    [Header("Hit effect")]
    [SerializeField] private SpriteRenderer bossSprite;
    [SerializeField] private Color hitColor = Color.red;

    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
        if (bossSprite == null) bossSprite = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;
        currentHealth -= damage;

        UpdateUI();

        if (bossSprite != null)
        {
            StopAllCoroutines();
            StartCoroutine(HitFlashRoutine());
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateUI()
    {
        if (hpSlider != null)
        {
            hpSlider.value = currentHealth / maxHealth;
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("º¸½º µØÁü");

        Destroy(gameObject);

        // GameManager.Instance.GameClear(); 
    }

    private IEnumerator HitFlashRoutine()
    {
        bossSprite.color = hitColor;
        yield return new WaitForSeconds(0.1f);
        bossSprite.color = Color.white;
    }
}