using UnityEngine;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
	private Health health;
	[SerializeField] private Image homeBarImage;

    // Start is called before the first frame update
    void Start()
    {
        health = new Health(30, homeBarImage);
    }

    // Update is called once per frame
    void takeDamage(int damage)
    {
        health.takeDamage(damage);
	}

	private void OnEnable()
	{
		Goblin.AttackHomeEvent += takeDamage;
	}

	private void OnDisable()
	{
		Goblin.AttackHomeEvent -= takeDamage;
	}
}