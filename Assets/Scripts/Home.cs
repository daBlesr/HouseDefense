using UnityEngine;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
	private Health health;
    [SerializeField] private Image HomeHealthBar;
    private float initialHealBarPos;

    // Start is called before the first frame update
    void Start()
    {
        health = new Health(30, 1, 0, 0, false);
        initialHealBarPos = HomeHealthBar.rectTransform.position.x;
    }

    private void Update()
    {
        if (health.isDead())
        {
            gameObject.GetComponent<PauseMenu>().GameOver();
        }
    }

    private void OnGUI()
    {
        // health.updateHealthBar(transform.position);
        float percentage = ((float)health.getHealth()) /  health.getMaxHealth();

        HomeHealthBar.rectTransform.position = new Vector3(
           initialHealBarPos - 3.5f / 2.0f + 3.5f * percentage / 2.0f,
           HomeHealthBar.rectTransform.position.y,
           HomeHealthBar.rectTransform.position.z
       );

        HomeHealthBar.rectTransform.localScale = new Vector3(
            3.5f * percentage,
            0.35f,
            1
        );
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