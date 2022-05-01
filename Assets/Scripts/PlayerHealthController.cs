using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth, maxHealth;

    public float invicibleLength;
    private float invicibleCounter;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invicibleCounter > 0)
        {
            invicibleCounter -= Time.deltaTime;

            if (invicibleCounter <= 0)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
            }
        }
    }

    public void DealDamage()
    {
        if (invicibleCounter > 0) return;

        --currentHealth;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            
            LevelManager.instance.RespawnPlayer();
        } 
        else
        {
            invicibleCounter = invicibleLength;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);

            PlayerController.instance.KnockBack();
        }

        UIController.instance.UpdateHealthDisplay();
    }
}
