using UnityEngine.UI;
using UnityEngine;

public class Health
{
	private int health;
	private int maxHealth;
    private readonly float scale;
    private readonly float vOffset;
	private readonly float hOffset;
	private float healthbarWidth = 1f;
    private float healthbarHeight = 0.2f;
    private GameObject fullHealth;
    private GameObject currentHealth;

    public Health(int maxHealth, float scale = 1, float vOffset = 0, float hOffset = 0, bool renderHealthBars = true)
    {
		this.maxHealth = maxHealth;
        this.scale = scale;
        this.vOffset = vOffset;
		this.hOffset = hOffset;
		health = this.maxHealth;

        if (renderHealthBars)
        {
            fullHealth = GameObject.CreatePrimitive(PrimitiveType.Cube);
            currentHealth = GameObject.CreatePrimitive(PrimitiveType.Cube);
            fullHealth.GetComponent<Renderer>().material.color = Color.red;
            currentHealth.GetComponent<Renderer>().material.color = Color.green;
        }        
    }

    public void updateHealthBar(Vector2 pos)
    {
       
        if (fullHealth)
        {
            fullHealth.transform.position = new Vector3(
                pos.x + hOffset,
                pos.y + 2 + vOffset,
                1
            );

            fullHealth.transform.localScale = new Vector3(
                healthbarWidth * scale,
                healthbarHeight * scale,
                0.1f
            );
        }
        

        if (currentHealth)
        {
            float percentage = ((float)health) / maxHealth;

            currentHealth.transform.position = new Vector3(
                pos.x + hOffset - healthbarWidth * scale / 2 + healthbarWidth * percentage * scale / 2,
                pos.y + 2 + vOffset,
                1
            );

            currentHealth.transform.localScale = new Vector3(
                healthbarWidth * percentage * scale,
                healthbarHeight * scale,
                0.2f
            );
        }
        
    }

    public void takeDamage(int amount)
    {
        if (health > 0)
        {
            health -= amount;
        }

        if (health < 0)
        {
            health = 0;
        }
    }

    public int getHealth()
    {
        return health;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    public bool isDead()
    {
        return health == 0;
    }

    public void destroy()
    {
        Object.Destroy(currentHealth);
        Object.Destroy(fullHealth);
    }
}