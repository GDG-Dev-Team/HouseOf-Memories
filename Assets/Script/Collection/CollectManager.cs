using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    public static CollectionManager instance;

   [SerializeField] private GameObject iconPrefab;

    [SerializeField]
    private TextMeshProUGUI countText;

   [SerializeField] private Transform iconContainer;// Parent object (e.g., a UI panel) where icons will appear

    [SerializeField]
    private int collectedCount = 0;

    [SerializeField]
    private int totalCount = 5;

    void Start()
    {
        
        UpdateText();
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void CollectItem(Sprite itemSprite)
    {
        if (collectedCount < totalCount)
        {
            collectedCount++;
            UpdateText();


             GameObject newIcon = Instantiate(iconPrefab, iconContainer);
            Image img = newIcon.GetComponent<Image>();
            if (img != null)
            {
                img.sprite = itemSprite;
            }
        }
    }

    void UpdateText()
    {
        countText.text = collectedCount + " / " + totalCount;
    }
}
