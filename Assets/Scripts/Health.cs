using UnityEngine.UI;
using UnityEngine;

public class Health
{
	private int health;
	private int maxHealth;
    private readonly float scale;
    private readonly float offset;
    private float healthbarWidth = 1f;
    private float healthbarHeight = 0.2f;
    private GameObject fullHealth = GameObject.CreatePrimitive(PrimitiveType.Cube);
    private GameObject currentHealth = GameObject.CreatePrimitive(PrimitiveType.Cube);

    public Health(int maxHealth, float scale = 1, float offset = 0)
    {
		this.maxHealth = maxHealth;
        this.scale = scale;
        this.offset = offset;
        health = this.maxHealth;

        fullHealth.GetComponent<Renderer>().material.color = Color.red;
        currentHealth.GetComponent<Renderer>().material.color = Color.green;
    }

    public void updateHealthBar(Vector2 pos)
    {
        fullHealth.transform.position = new Vector3(
            pos.x, 
            pos.y + 2 + offset, 
            1
        );

        fullHealth.transform.localScale = new Vector3(
            healthbarWidth * scale, 
            healthbarHeight * scale, 
            0.1f
        );

        float percentage = ((float) health) / maxHealth;

        currentHealth.transform.position = new Vector3(
            pos.x - healthbarWidth * scale / 2 + healthbarWidth * percentage * scale / 2, 
            pos.y + 2 + offset, 
            1
        );

        currentHealth.transform.localScale = new Vector3(
            healthbarWidth * percentage * scale, 
            healthbarHeight * scale, 
            0.2f
        );
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