using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private RectTransform healthDisplayParent;
    [SerializeField]
    private Sprite filledHealthSprite;
    [SerializeField]
    private Sprite emptyHealthSprite;
    private List<GameObject> healthDisplays;

    [SerializeField]
    private TextMeshProUGUI ammoText;
    /*[SerializeField]
    private TextMeshProUGUI timer;*/

    [SerializeField]
    private GameObject pauseMenu;

    private void OnCurrentHealthChanged(int current)
    {

    }

    private void OnMaxHealthChanged(int max, int change)
    {

    }

    private void Start()
    {
        healthDisplays = new List<GameObject>();

    }

    private void OnEnable()
    {
        PlayerHealth.OnCurrentHealthChanged += OnCurrentHealthChanged;
        PlayerStats.OnMaxHealthChanged += OnMaxHealthChanged;
    }

    private void OnDisable()
    {
        PlayerHealth.OnCurrentHealthChanged -= OnCurrentHealthChanged;
        PlayerStats.OnMaxHealthChanged -= OnMaxHealthChanged;
    }
}
