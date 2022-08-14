using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    //public LightController[] lights;
    public TutorialChest chest;
    public List<TutorialTimelineTrigger> triggers;
    public PlayableDirector timeline1;
    public Spawner spawner;
    public GameObject zombieDoor;

    private Player player;
    private int tutorialStage = 0;
    private Inventory _inventory;
    private Inventory _gunInventory;

    private void Start()
    {
        _inventory = InventoryManager.Instance.getItemInventory();
        _gunInventory = InventoryManager.Instance.getGunInventory();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        //foreach (LightController light in lights)
        //    light.SetIntensity(LightController.Intensity.Off);
        chest.CollisionExit += ChestExited;
        foreach(TutorialTimelineTrigger t in triggers)
        {
            t.TriggerEntered += TutorialTriggerEntered;
        }
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
                //    lights[0].TweenIntensity(LightController.Intensity.High);
                //    lights[0].TweenOuterRadius(LightController.Distance.Low);
                    tutorialStage++;
                }
                break;
            case 1:
                if (player.GetNearbyObject() is BreakableBox && Input.GetMouseButtonDown(0))
                {
                //    lights[0].TweenOuterRadius(LightController.Distance.High);
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
                //    lights[0].SetIntensity(LightController.Intensity.High);
                //    lights[1].TweenIntensity(LightController.Intensity.High);
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
            //foreach (LightController light in lights)
            //    light.TweenIntensity(LightController.Intensity.High);
            tutorialStage++;
        }
    }

    private void TutorialTriggerEntered(object state, EventArgs e)
    {
        TutorialTimelineTrigger ttt = state as TutorialTimelineTrigger;
        Console.WriteLine("Hi bitch - " + ttt.id);
        switch (tutorialStage)
        {
            case 7:
                timeline1.Play();
                spawner.SpawnZombie();
                tutorialStage++;
                break;
            case 8:
                if(_gunInventory.GetItemCount() > 0)
                {
                    timeline1.Stop();
                    timeline1.Play();
                    Destroy(zombieDoor, 1f);
                    tutorialStage++;
                }
                break;
            case 9:
                if(ttt.id == 2)
                {
                    GameManager.Instance.SetGameState(GameState.Waves);
                    tutorialStage++;
                }
                break;
        }
    }
}
