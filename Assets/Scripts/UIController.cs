using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Image[] healthHearts;

    public Sprite heartFull, heartHalf, heartEmpty;

    private int heartIndex;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        heartIndex = PlayerHealthController.instance.currentHealth / 2;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateHealthDisplay()
    {
        int currentHealth = PlayerHealthController.instance.currentHealth;

        if (currentHealth == 0)
        {
            healthHearts[0].sprite = heartEmpty;
            return;
        }

        if (currentHealth % 2 != 0)
        {
            --heartIndex;
            healthHearts[heartIndex].sprite = heartHalf;
        }
        else
        {
            healthHearts[heartIndex].sprite = heartEmpty;
        }
    }
}
