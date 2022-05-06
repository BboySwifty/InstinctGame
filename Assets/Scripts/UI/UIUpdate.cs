using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdate : MonoBehaviour
{
    public Transform inventoryUI;
    public Transform gunInventoryUI;
    public Transform waveUI;
    public TextMeshProUGUI messageBox;

    [Header("Examine fields")]
    public Image itemImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    Inventory itemInventory;
    Inventory gunInventory;
    InventorySlot[] itemSlots;
    InventorySlot[] gunSlots;
    TextMeshProUGUI[] waveLabels;
    WaveManager wm;

    #region Singleton
    public static UIUpdate Instance { get; private set; }
    private void CreateInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Awake()
    {
        CreateInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        wm = WaveManager.Instance;

        InventoryManager.Instance.ItemsChanged += UpdateInventory;
        InventoryManager.Instance.ItemsChanged += UpdateExaminePanel;

        itemInventory = InventoryManager.Instance.getItemInventory();
        gunInventory = InventoryManager.Instance.getGunInventory();

        gunSlots = gunInventoryUI.GetComponentsInChildren<InventorySlot>();
        itemSlots = inventoryUI.GetComponentsInChildren<InventorySlot>();

        waveLabels = waveUI.GetComponentsInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (wm.waveOnCooldown)
        {
            waveLabels[0].SetText("Next wave in");
            waveLabels[1].SetText(Mathf.Ceil(wm.WaveRate - wm.cooldownTimer).ToString() + "s");
        }
        else
        {
            waveLabels[0].SetText("Wave");
            waveLabels[1].SetText(WaveManager.Instance.waveNumber.ToString());
        }
    }

    public void BroadcastMessageToUser(String message)
    {
        messageBox.text = message;
    }

    private void UpdateInventory(object sender, EventArgs e)
    {
        Color color = new Color(48 / 255, 48 / 255, 48 / 255, 0);
        Item activeItem = InventoryManager.Instance.GetActiveItem();

        for (int i = 0; i < gunSlots.Length; i++)
        {
            Item currentItem = gunInventory.GetItemAtIndex(i);
            gunSlots[i].AddItem(currentItem);

            if (currentItem == activeItem && currentItem != null)
                color.a = 1;
            else
                color.a = 0;
            gunSlots[i].GetComponent<Image>().color = color;
        }

        for (int i = 0; i < itemSlots.Length; i++)
        {
            Item currentItem = itemInventory.GetItemAtIndex(i);
            itemSlots[i].AddItem(currentItem);

            if (currentItem == activeItem && currentItem != null)
                color.a = 1;
            else
                color.a = 0;
            itemSlots[i].GetComponent<Image>().color = color;
        }
    }

    private void UpdateExaminePanel(object sender, EventArgs e)
    {
        Item activeItem = InventoryManager.Instance.GetActiveItem();
        if (activeItem == null)
            return;
        itemImage.sprite = activeItem.inventoryImage;
        nameText.text = activeItem.itemName;
        descriptionText.text = activeItem.description;
    }
}
