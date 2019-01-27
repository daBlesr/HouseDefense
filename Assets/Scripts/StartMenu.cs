using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
	[SerializeField] private GameObject menu_0;
	[SerializeField] private GameObject menu_1;
	[SerializeField] private GameObject menu_2;
	[SerializeField] private GameObject htw1;

	[SerializeField] private Button startButton;
	[SerializeField] private Button howToPlayButton;
	[SerializeField] private Button creditsButton;


	// Start is called before the first frame update
	void Start()
	{
		startButton.Select();
		menu_0.SetActive(true);
		menu_1.SetActive(false);
		menu_2.SetActive(false);
	}

	public void StartGame(string name)
	{
		SceneManager.LoadScene(name);
	}

	public void HowToPlay()
	{
		howToPlayButton.Select();
		menu_0.SetActive(false);
		menu_1.SetActive(true);
		menu_2.SetActive(false);
		htw1.SetActive(true);
	}

	public void Credits()
	{
		creditsButton.Select();
		menu_0.SetActive(false);
		menu_1.SetActive(false);
		menu_2.SetActive(true);
	}

	public void MainMenu()
	{
		startButton.Select();
		menu_0.SetActive(true);
		menu_1.SetActive(false);
		menu_2.SetActive(false);
	}
}