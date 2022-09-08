using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Player player;
    public Transform inventoryUI;
    public Transform gunInventoryUI;
    public Transform waveUI;
    public TextMeshProUGUI messageBox;

    [Header("Player Stats fields")]
    public Slider healthSlider;

    [Header("Gun Ammo fields")]
    public TextMeshProUGUI ammoText;

    [Header("Examine fields")]
    public Image itemImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    Inventory inventory;
    InventorySlot[] itemSlots;
    InventorySlot[] gunSlots;
    TextMeshProUGUI[] waveLabels;
    WaveManager wm;
    InventoryManager im;

    #region Singleton
    public static UIManager Instance { get; private set; }
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
        im = InventoryManager.Instance;

        im.ItemsChanged += UpdateInventory;
        im.ItemsChanged += UpdateExaminePanel;
        im.ItemsChanged += EnableAmmoPanel;

        player.HealthChanged += UpdateHealthSlider;

        inventory = InventoryManager.Instance.GetInventory();

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

    private void UpdateInventory(object sender, ItemsChangedEventArgs e)
    {
        Color color = new Color(48 / 255, 48 / 255, 48 / 255, 0);
        Item activeItem = InventoryManager.Instance.GetActiveItem();

        for (int i = 0; i < gunSlots.Length; i++)
        {
            Item currentItem = inventory.GetItemAtIndex(InventoryType.Gun, i);
            gunSlots[i].AddItem(currentItem);
            int alpha = currentItem == activeItem && currentItem != null ? 1 : 0;
            color.a = alpha;
            gunSlots[i].GetComponent<Image>().color = color;
        }

        for (int i = 0; i < itemSlots.Length; i++)
        {
            Item currentItem = inventory.GetItemAtIndex(InventoryType.Item, i);
            itemSlots[i].AddItem(currentItem);
            int alpha = currentItem == activeItem && currentItem != null ? 1 : 0;
            color.a = alpha;
            itemSlots[i].GetComponent<Image>().color = color;
        }
    }

    private void UpdateExaminePanel(object sender, ItemsChangedEventArgs e)
    {
        Item activeItem = InventoryManager.Instance.GetActiveItem();
        if (activeItem == null)
            return;
        itemImage.sprite = activeItem.itemData.inventoryImage;
        nameText.text = activeItem.itemData.itemName;
        descriptionText.text = activeItem.itemData.description;
    }

    private void EnableAmmoPanel(object sender, ItemsChangedEventArgs e)
    {
        ammoText.enabled = e.GunIsEquipped;
        if (e.GunIsEquipped)
            (im.GetActiveItem() as Gun).AmmoChanged += UpdateAmmoText;
    }

    private void UpdateAmmoText(object sender, AmmoChangedEventArgs e)
    {
        ammoText.text = $"{e.CurrentAmmo} | {e.ExtraAmmo}";
    }

    private void UpdateHealthSlider(object sender, HealthChangedEventArgs e)
    {
        healthSlider.value = e.CurrentHealth;
    }
}
