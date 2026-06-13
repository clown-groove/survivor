using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Image))]
public class UISingleHP : MonoBehaviour
{
    [SerializeField]
    private Sprite filledHeartSprite;
    [SerializeField]
    private Sprite emptyHeartSprite;

    private Image heartImage;

    private bool isFilled;
    public bool HeartFilled
    {
        get { return isFilled; }
        set 
        { 
            isFilled = value; 
            
            heartImage.sprite = isFilled ? filledHeartSprite : emptyHeartSprite;
        }
    }

    private void Awake()
    {
        heartImage = GetComponent<Image>();
        isFilled = false;
    }
}
