using Articy.Lose_Change;
using UnityEngine;

namespace src.Dialogue
{
  public class ScriptProvider : MonoBehaviour, IScriptMethodProvider
  {
    public bool IsCalledInForecast { get; set; }
    
    public void LoadFallScene()
    {
      if (IsCalledInForecast) return;
      Debug.Log("Load falling scene");
    }
  }
}