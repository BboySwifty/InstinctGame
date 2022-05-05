using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerOld : MonoBehaviour
{

    public PauseMenu pm;

    public static InputManagerOld Instance { get; private set; }

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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
}
