using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static event Action<bool> OnPause = delegate { };

    private bool gamePaused;
    public bool GamePaused
    {
        get { return gamePaused; }
        private set 
        { 
            gamePaused = value;

            OnPause?.Invoke(gamePaused);

            if (gamePaused)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }

    public void PauseInput()
    {
        GamePaused = !GamePaused;
    }

    public void ResumeButton()
    {
        if (gamePaused)
        {
            GamePaused = false;
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        GamePaused = false;
    }
}
