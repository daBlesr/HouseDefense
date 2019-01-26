using UnityEngine.UI;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
	public static HealthSystem Instance { get { return GetInstance(); } }

	#region Singleton

	private static HealthSystem instance;
	private static HealthSystem GetInstance()
	{
		if (instance == null)
		{
			instance = FindObjectOfType<HealthSystem>();
		}
		return instance;
	}
	#endregion

	public float Health	{ get { return health; } set { health = value; } }
	private float health;

	[SerializeField] private float maxHealth = 100;
	[SerializeField] private Image healthBarImage;

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

	private void OnEnable()
	{
		Goblin.DamageEvent += UpdateHealthBar;
	}

	private void OnDisable()
	{
		Goblin.DamageEvent -= UpdateHealthBar;
	}
}