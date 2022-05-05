using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Animator[] animators;

    int activePanel = -1;
    
    public void OpenPanel(int id)
    {
        if (activePanel != id)
        {
            animators[id].SetBool("Open", true);
            if (activePanel != -1)
                animators[activePanel].SetBool("Open", false);
            activePanel = id;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
