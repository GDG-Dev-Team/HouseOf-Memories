using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    public static CollectionManager instance;

    [SerializeField]
    private Image itemIcon;

    [SerializeField]
    private TextMeshProUGUI countText;

    [SerializeField]
    private Sprite iconSprite;

    [SerializeField]
    private int collectedCount = 0;

    [SerializeField]
    private int totalCount = 6;

    void Start()
    {
        itemIcon.sprite = iconSprite;
        UpdateText();
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void CollectItem()
    {
        if (collectedCount < totalCount)
        {
            collectedCount++;
            UpdateText();
        }
    }

    void UpdateText()
    {
        countText.text = collectedCount + " / " + totalCount;
    }
}
