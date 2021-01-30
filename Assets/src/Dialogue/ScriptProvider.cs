using Articy.Lose_Change;
using UnityEngine;
using UnityEngine.Events;

namespace src.Dialogue
{
  public class ScriptProvider : MonoBehaviour, IScriptMethodProvider
  {
    public UnityEvent onHideCigar;
    public UnityEvent onLoadFallScene;
    public UnityEvent onLoadFloorScene;
    
    public bool IsCalledInForecast { get; set; }
    
    public void LoadFallScene()
    {
      if (IsCalledInForecast) return;
      onLoadFallScene.Invoke();
    }

    public void LoadFloorScene()
    {
      if (IsCalledInForecast) return;
      onLoadFloorScene.Invoke();
    }

    public void HideCigarSprite() => onHideCigar.Invoke();
  }
}