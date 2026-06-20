using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform healthDisplayParent;
    [SerializeField]
    private GameObject singleHealth;
    private List<GameObject> healthDisplays;

    [SerializeField]
    private TextMeshProUGUI ammoText;
    [SerializeField]
    private TextMeshProUGUI timerText;
    private DateTime timerEndTime;

    [SerializeField]
    private GameObject pauseMenu;

    public void Resume()
    {
        GameManager.Instance.ResumeButton();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    private void OnCurrentHealthChanged(int current)
    {
        for (int i = 0; i < current; i++)
        {
            healthDisplays[i].GetComponent<UISingleHP>().HeartFilled = true;
        }
        for (int i = current; i < healthDisplays.Count; i++)
        {
            healthDisplays[i].GetComponent<UISingleHP>().HeartFilled = false;
        }
    }

    private void OnMaxHealthChanged(int max, int change)
    {
        if (change > 0)
        {
            while (healthDisplays.Count < max)
            {
                healthDisplays.Add(Instantiate(singleHealth, healthDisplayParent));
            }
        }
        else if (change < 0)
        {
            while (healthDisplays.Count > max)
            {
                healthDisplays.RemoveAt(healthDisplays.Count - 1);
            }
        }
    }

    private void OnPause(bool gamePaused)
    {
        pauseMenu.SetActive(gamePaused);
    }

    private void OnAmmoAmmountChange(int ammount, int max)
    {
        ammoText.text = $"{ammount}/{max}";
    }

    private void Awake()
    {
        healthDisplays = new List<GameObject>();
    }

    private void Start()
    {
        pauseMenu.SetActive(false);
        timerEndTime = DateTime.Now.AddSeconds(WaveManager.Instance.GetTimeFromStartToBoss());
    }

    private void Update()
    {
        TimeSpan remaining = timerEndTime - DateTime.Now;
        if (remaining.TotalSeconds < 0)
            remaining = TimeSpan.Zero;
        timerText.text = $"{remaining.Minutes:00}:{remaining.Seconds:00}";
    }

    private void OnEnable()
    {
        PlayerHealth.OnCurrentHealthChanged += OnCurrentHealthChanged;
        PlayerHealth.OnMaxHealthChanged += OnMaxHealthChanged;
        GameManager.OnPause += OnPause;
        PlayerWeapon.OnAmmoAmmountChange += OnAmmoAmmountChange;
    }

    private void OnDisable()
    {
        PlayerHealth.OnCurrentHealthChanged -= OnCurrentHealthChanged;
        PlayerHealth.OnMaxHealthChanged -= OnMaxHealthChanged;
        GameManager.OnPause -= OnPause;
        PlayerWeapon.OnAmmoAmmountChange -= OnAmmoAmmountChange;
    }
}
