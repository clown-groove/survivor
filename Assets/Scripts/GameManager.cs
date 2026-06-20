using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static event Action<bool> OnPause = delegate { };
    public static event Action OnVictory = delegate { };
    public static event Action OnDefeat = delegate { };

    private bool gamePaused;
    private bool gameEnded;

    public bool GamePaused
    {
        get { return gamePaused; }
        private set 
        { 
            gamePaused = value;

            if (gamePaused)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }

            if (!gameEnded)
                OnPause?.Invoke(gamePaused);
        }
    }

    public void PauseInput()
    {
        if (!gameEnded)
            GamePaused = !GamePaused;
    }

    public void ResumeButton()
    {
        if (gamePaused)
        {
            GamePaused = false;
        }
    }

    public void CallPlayerDead()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            GamePaused = true;
            OnDefeat?.Invoke();
        }
    }

    public void CallVictory()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            GamePaused = true;
            OnVictory?.Invoke();
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
        gameEnded = false;
    }
}
