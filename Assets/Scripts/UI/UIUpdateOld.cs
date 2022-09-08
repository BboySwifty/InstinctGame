using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdateOld : MonoBehaviour
{

    public Player playerStats;
    public TextMeshProUGUI scoreLabel;
    public TextMeshProUGUI healthLabel;
    public TextMeshProUGUI ammoLabel;
    public TextMeshProUGUI waveLabel;
    public Slider healthBarSlider;
    public Transform inventoryUI;

    //GunInventory inventory;
    InventorySlot[] slots;
    Gun activeGun;
    WaveManager wm;

    void Start()
    {
        wm = WaveManager.Instance;
        //inventory = GunInventory.instance;

        /*inventory.ItemsChanged += UpdateInventory;
        inventory.ActiveItemChanged += ChangeActiveGun;

        activeGun = inventory.defaultGun;*/

        slots = inventoryUI.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScoreLabel();
        UpdateAmmoLabel();
    }

    private void UpdateInventory(object sender, EventArgs args)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            //slots[i].AddGun(inventory.GetActiveGun(i));
        }
    }

    private void UpdateAmmoLabel()
    {
        string currentAmmo = activeGun.CurrentClipAmmo().ToString();
        string extraAmmo = activeGun.CurrentExtraAmmo().ToString();
        if (currentAmmo.Equals("-1"))
            currentAmmo = "-";
        if (extraAmmo.Equals("-1"))
            extraAmmo = "-";
        ammoLabel.text = currentAmmo + " | " + extraAmmo;
    }

    private void UpdateScoreLabel()
    {
        //scoreLabel.SetText(playerStats.GetScore().ToString());
    }

    private void ChangeActiveGun(object sender, EventArgs args)
    {
        //activeGun = inventory.GetActiveGun();
        //int gunIndex = inventory.GetIndexOfGun(args.Gun);
        Color color = new Color(48 / 255, 48 / 255, 48 / 255, 0);
        for (int i = 0; i < slots.Length; i++)
        {
            //if (i == gunIndex)
                color.a = 1;
            //else
                color.a = 0;
            slots[i].GetComponent<Image>().color = color;
        }
    }

}
