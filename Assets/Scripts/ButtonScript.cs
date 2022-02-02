using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
   public void onNextLevelButtonPressed()
    {
        SceneManager.LoadScene(2);
    }
    public void onPrvsButtonPressed()
    {
        SceneManager.LoadScene(0);
    }
}
