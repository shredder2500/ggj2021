using UnityEngine;
using UnityEngine.SceneManagement;

namespace src
{
  public class LoadScene : MonoBehaviour
  {
    public string scene;

    public void Load() => SceneManager.LoadScene(scene);
  }
}