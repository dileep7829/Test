using System.Collections;
using System.Collections.Generic;
using Controllers.Game;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
   public void OnRestartButtonClicked()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }
}
