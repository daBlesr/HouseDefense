using UnityEngine.UI;
using UnityEngine;

public class HealthSystem
{
	public float Health	{ get { return health; } set { health = value; } }
	private float health;

	public float MaxHealth { get {return maxHealth; } set { maxHealth = value; } }
	private float maxHealth;

	public Image healthBarImage;

	// Start is called before the first frame update
	void Start()
    {
		health = maxHealth;
		healthBarImage.fillAmount = (health) / 100;
    }

    private void UpdateHealthBar(int damageAmount)
	{
		if(health > 0)
		{
			health -= damageAmount;
			healthBarImage.fillAmount = (health) / 100;
		}
	}
}