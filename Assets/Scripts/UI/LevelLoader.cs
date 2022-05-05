using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public void OpenScene(int index)
    {
        StartCoroutine(LoadScene(index));
    }

    private IEnumerator LoadScene(int index)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(index);
    }
}
