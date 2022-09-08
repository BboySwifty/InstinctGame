using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public PauseMenu pm;
    public Animator inventoryAnimator;
    public Animator gunsAnimator;
    public ExaminePanel examinePanel;

    public static InputManager Instance { get; private set; }

    private InventoryManager invManager;
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
        invManager = InventoryManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        /* Item selection */
        if (checkKeyDown(KeyCode.Alpha1))
        {
            invManager.SelectItem(InventoryType.Item, 0);
        }
        else if (checkKeyDown(KeyCode.Alpha2))
        {
            invManager.SelectItem(InventoryType.Item, 1);
        }
        else if (checkKeyDown(KeyCode.Alpha3))
        {
            invManager.SelectItem(InventoryType.Item, 2);
        }
        else if (checkKeyDown(KeyCode.Alpha4))
        {
            invManager.SelectItem(InventoryType.Item, 3);
        }
        else if (checkKeyDown(KeyCode.Alpha5))
        {
            invManager.SelectItem(InventoryType.Item, 4);
        }
        else if (checkKeyDown(KeyCode.Alpha6))
        {
            invManager.SelectItem(InventoryType.Item, 5);
        }
        else if (checkKeyDown(KeyCode.Q))
        {
            invManager.SelectItem(InventoryType.Gun, 0);
        }

        /* Interaction */
        if (Input.GetMouseButton(0))
        {
            invManager.UseItem();
        }
        if (Input.GetMouseButtonDown(0))
        {
            invManager.UseItemDown();
        }
        if (checkKeyDown(KeyCode.E))
        {
            player.PickupItem();
        }
        if (checkKeyDown(KeyCode.R))
        {
            if (invManager.GetActiveItem() is Gun)
                StartCoroutine((invManager.GetActiveItem() as Gun).Reload());
        }
        if (checkKeyDown(KeyCode.G))
        {
            invManager.DropCurrentItem();
        }
        if (checkKeyDown(KeyCode.Space))
        {
            examinePanel.Open();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            StartCoroutine(examinePanel.Close());
        }

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
