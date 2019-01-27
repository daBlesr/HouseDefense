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

    private bool isPaused = false;
 
    // Start is called before the first frame update
    void Start()
    {
		pauseMenu.SetActive(false);
		GameOverMenu.SetActive(false);
	}

	private void Update()
	{
		OnPauseGame();
	}

	private void OnPauseGame()
	{
		if(Input.GetButtonDown("Pause"))
		{
            if (!isPaused)
            {
                Pause();   
            } else
            {
                Resume();
            }
		}
	}

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        resumeButton.Select();
        isPaused = true;
    }

    public void Resume() //Jumps when exits pauseScreen;
	{
		pauseMenu.SetActive(false);
		Time.timeScale = 1;
        isPaused = false;
    }

	public void Quit()
	{

	}

	public void GameOver()
	{
		GameOverMenu.SetActive(true);
	}

	public void BackToMenu(string name)
	{
		SceneManager.LoadScene(name);
	}

	public void Restart(string name)
	{
		SceneManager.LoadScene(name);
	}
}