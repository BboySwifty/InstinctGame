using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public PauseMenu pm;
    public Animator inventoryAnimator;
    public Animator gunsAnimator;

    public static InputManager Instance { get; private set; }

    private Player player;

    #region Singleton
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

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        /* Item selection */
        if (checkKeyDown(KeyCode.Alpha1))
        {
            InventoryManager.Instance.SelectItem(1, 0);
        }
        else if (checkKeyDown(KeyCode.Alpha2))
        {
            InventoryManager.Instance.SelectItem(1, 1);
        }
        else if (checkKeyDown(KeyCode.Alpha3))
        {
            InventoryManager.Instance.SelectItem(1, 2);
        }
        else if (checkKeyDown(KeyCode.Alpha4))
        {
            InventoryManager.Instance.SelectItem(1, 3);
        }
        else if (checkKeyDown(KeyCode.Alpha5))
        {
            InventoryManager.Instance.SelectItem(1, 4);
        }
        else if (checkKeyDown(KeyCode.Alpha6))
        {
            InventoryManager.Instance.SelectItem(1, 5);
        }
        else if (checkKeyDown(KeyCode.Q))
        {
            InventoryManager.Instance.SelectItem(0, 0);
        }
        if (Input.GetMouseButton(0))
        {
            if(player.state == 1)
            {
                Gun gun = (Gun) InventoryManager.Instance.GetActiveItem();
                gun.TriggerDown();
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if(player.state == 1)
            {
                Gun gun = (Gun)InventoryManager.Instance.GetActiveItem();
                gun.Trigger();
            }
            else
            {
                player.UseObject();
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            player.PickupItem();
        }
        /*else if (checkKeyDown(KeyCode.E))
        {
            InventoryManager.Instance.SelectItem(0, 1);
        }*/
        /*
        if (Input.GetButtonDown("Gun1"))
        {
            weaponHolder.transform.GetChild(0).gameObject.SetActive(true);
            activeGun = 0;
            if (guns.Count > 1)
            {
                weaponHolder.transform.GetChild(1).gameObject.SetActive(false);
                //ActiveItemChanged(this, new InventoryEventArgs(guns[0]));
            }
        }
        else if (Input.GetButtonDown("Gun2"))
        {
            weaponHolder.transform.GetChild(0).gameObject.SetActive(false);
            activeGun = 1;
            if (guns.Count > 1)
            {
                weaponHolder.transform.GetChild(1).gameObject.SetActive(true);
                //ActiveItemChanged(this, new InventoryEventArgs(guns[1]));
            }
        }
        */

        /* Pause Menu */
        if (checkKeyDown(KeyCode.Escape))
        {
            if (PauseMenu.GameIsPaused)
            {
                pm.Resume();
            }
            else
            {
                pm.Pause();
            }
        }
    }

    private void FixedUpdate()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");
        if(xAxis != 0f || yAxis != 0f)
        {
            Vector2 movement = new Vector3(xAxis, yAxis, 0.0f);
            movement.Normalize();
            player.Move(movement);
            player.isWalking = true;
        }
        else
        {
            player.isWalking = false;
        }
    }

    private bool checkKeyDown(KeyCode key) { return Input.GetKeyDown(key); }
}
