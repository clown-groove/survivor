using System.Collections.Generic;
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
    /*[SerializeField]
    private TextMeshProUGUI timer;*/

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
