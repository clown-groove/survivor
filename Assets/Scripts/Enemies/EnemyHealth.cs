using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
[RequireComponent(typeof(EnemyDrops))]
public class EnemyHealth : MonoBehaviour
{
    private Rigidbody2D rb;

    private float currentHealth;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Color damagedColor = Color.rebeccaPurple;
    private float colorFadeDuration = .2f;
    private Coroutine fadeCoroutineReference;

    public void ApplyDamage(float damage, Vector2 dmgSourcePosition, float knockback)
    {
        currentHealth -= damage;

        if(fadeCoroutineReference != null)
        {
            StopCoroutine(fadeCoroutineReference);
        }
        fadeCoroutineReference = StartCoroutine(ColorFadeCoroutine());

        rb.AddForce(((Vector2)transform.position - dmgSourcePosition).normalized * knockback, ForceMode2D.Impulse);

        CheckDeath();
    }

    private IEnumerator ColorFadeCoroutine()
    {
        float timer = 0;
        while (timer < colorFadeDuration)
        {
            spriteRenderer.color = Color.Lerp(damagedColor, Color.white, timer / colorFadeDuration);
            yield return new WaitForNextFrameUnit();
            timer += Time.deltaTime;
        }
        spriteRenderer.color = Color.white;
        fadeCoroutineReference = null;
    }

    private void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            GetComponent<EnemyDrops>().SpawnDrops();
            Destroy(gameObject);
        }
    }

    public void SetHealth(float health)
    {
        currentHealth = health;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
