using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ButtonManagement : MonoBehaviour
{
   public void loadScene(string sceneName)
   {
      SceneManager.LoadScene(sceneName);
   }
}
