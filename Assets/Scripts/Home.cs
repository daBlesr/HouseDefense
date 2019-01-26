using UnityEngine;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
	private HealthSystem health = new HealthSystem();
	[SerializeField] private Image homeBarImage;

    // Start is called before the first frame update
    void Start()
    {
		health.MaxHealth = 100;
		health.Health = health.MaxHealth;
		homeBarImage.fillAmount = (health.Health/100);
	}

    // Update is called once per frame
    void WhenHit(int damage)
    {
		if(health.Health > 0)
		{
			health.Health -= damage;
			Debug.Log(health.Health);
			homeBarImage.fillAmount = (health.Health/100);
		}
	}

	private void OnEnable()
	{
		Goblin.AttackHomeEvent += WhenHit;
	}

	private void OnDisable()
	{
		Goblin.AttackHomeEvent -= WhenHit;
	}
}