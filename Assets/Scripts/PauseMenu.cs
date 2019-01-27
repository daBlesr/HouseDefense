using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject GameOverMenu;

	[SerializeField] private Button resumeButton;
 
    // Start is called before the first frame update
    void Start()
    {
		pauseMenu.SetActive(false);
		GameOverMenu.SetActive(false);
	}

	private void Update()
	{
		PauseGame();
	}

	private void PauseGame()
	{
		if(Input.GetButtonDown("Pause"))
		{
			pauseMenu.SetActive(true);
			Time.timeScale = 0;
			resumeButton.Select();
		}
	}

    public void Resume() //Jumps when exits pauseScreen;
	{
		pauseMenu.SetActive(false);
		Time.timeScale = 1;
	}

	public void Quit()
	{

	}

	public void BackToMenu(string name)
	{
		SceneManager.LoadScene(name);
	}

	public void Restart(string name)
	{
		SceneManager.LoadScene(name);
	}

	private void ActivateGameOver()
	{
		GameOverMenu.SetActive(true);
	}

	private void OnEnable()
	{
		Player.playerDeadEvent += ActivateGameOver;
	}

	private void OnDisable()
	{
		Player.playerDeadEvent += ActivateGameOver;
	}
}