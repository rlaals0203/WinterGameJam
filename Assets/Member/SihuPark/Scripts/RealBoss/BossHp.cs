using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossHP : MonoBehaviour
{
    [Header("체력 설정")]
    [SerializeField] private float maxHealth = 1000f;
    [SerializeField] private float currentHealth;

    [Header("UI 연결")]
    [SerializeField] private Slider hpSlider;

    [Header("피격 효과")]
    [SerializeField] private SpriteRenderer bossSprite;
    [SerializeField] private Color hitColor = Color.red;

    private bool isDead = false;
    private void OnEnable()
    {
        if (hpSlider != null)
        {
            hpSlider.gameObject.SetActive(true);
            currentHealth = maxHealth;
            UpdateUI();
        }
    }

    private void OnDisable()
    {
        if (hpSlider != null)
        {
            hpSlider.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        if (bossSprite == null) bossSprite = GetComponent<SpriteRenderer>();
        UpdateUI();
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
        Debug.Log("보스 뒤짐");

        Destroy(gameObject);
    }

    private IEnumerator HitFlashRoutine()
    {
        bossSprite.color = hitColor;
        yield return new WaitForSeconds(0.1f);
        bossSprite.color = Color.white;
    }
}