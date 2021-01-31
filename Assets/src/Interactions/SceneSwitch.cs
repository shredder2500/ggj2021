using UnityEngine;
using UnityEngine.Events;

namespace src.Interactions
{
  public class SceneSwitch : MonoBehaviour
  {
    [SerializeField] private Transform player;
    [SerializeField]
    private Transform spawnLocation;

    public UnityEvent onSwitchScene;
    
    public void SwitchScene()
    {
      onSwitchScene.Invoke();
      player.transform.position = spawnLocation.position;
    }
  }
}