using Articy.Lose_Change;
using UnityEngine;
using UnityEngine.Events;

namespace src.Dialogue
{
  public class ScriptProvider : MonoBehaviour, IScriptMethodProvider
  {
    public UnityEvent onHideCigar;
    
    public bool IsCalledInForecast { get; set; }
    
    public void LoadFallScene()
    {
      if (IsCalledInForecast) return;
      Debug.Log("Load falling scene");
    }

    public void HideCigarSprite() => onHideCigar.Invoke();
  }
}