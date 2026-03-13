using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class HomeButton : MonoBehaviour
{
    public void OnHomeButtonClick()
    {
        SceneManager.LoadScene(GlobalData.LOBBY_SCENE_INDEX);
    }
}
