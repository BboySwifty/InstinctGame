using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    public LightController[] lights;
    public TutorialChest chest;

    private Player player;
    private int tutorialStage = 0;
    private Inventory _inventory;

    private void Start()
    {
        _inventory = InventoryManager.Instance.getItemInventory();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        lights[0].SetOuterRadius(LightController.Distance.Off);
        lights[1].SetOuterRadius(LightController.Distance.Off);
        lights[2].SetOuterRadius(LightController.Distance.Off);
        lights[3].SetOuterRadius(LightController.Distance.Off);
        lights[4].SetIntensity(LightController.Intensity.Off);

        chest.CollisionExit += ChestExited;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == tutorialStage)
                popUps[i].SetActive(true);
            else
                popUps[i].SetActive(false);
        }

        switch (tutorialStage)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.W) ||
                    Input.GetKeyDown(KeyCode.A) ||
                    Input.GetKeyDown(KeyCode.S) ||
                    Input.GetKeyDown(KeyCode.D))
                {
                    lights[0].TweenOuterRadius(LightController.Distance.Low);
                    tutorialStage++;
                }
                break;
            case 1:
                if (player.GetNearbyObject() is BreakableBox && Input.GetMouseButtonDown(0))
                {
                    lights[0].TweenOuterRadius(LightController.Distance.High);
                    tutorialStage++;
                }
                break;
            case 2:
                if (_inventory.GetItemCount() == 2)
                    tutorialStage++;
                break;
            case 3:
                if (player.GetNearbyObject() is BreakableBox box &&
                    Input.GetMouseButtonDown(0) &&
                    _inventory.GetActiveItem().id == box.requiredItemID)
                    tutorialStage++;
                break;
            case 4:
                if (_inventory.GetItemCount() == 2)
                {
                    lights[1].TweenOuterRadius(LightController.Distance.High);
                    tutorialStage++;
                }
                break;
            case 6:
                if (player.GetNearbyObject() is Door door &&
                    Input.GetMouseButtonDown(0) &&
                    _inventory.GetActiveItem().id == door.requiredItemID)
                {
                    tutorialStage++;
                }
                break;
        }
    }

    private void ChestExited(object state, EventArgs e)
    {
        if (tutorialStage == 5)
        {
            lights[2].TweenOuterRadius(LightController.Distance.High);
            lights[3].TweenOuterRadius(LightController.Distance.High);
            lights[4].TweenIntensity(LightController.Intensity.Low);
            tutorialStage++;
        }
    }
}
