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
    public UnityEvent onPickUpPin;
    public UnityEvent onLoadCrackScene;
    public UnityEvent onHideLockette;
    public UnityEvent onRemovePinkPin;
    
    public bool IsCalledInForecast { get; set; }

    public void RemovePinkPin() => onRemovePinkPin.Invoke();

    public void LoadFallScene()
    {
      if (IsCalledInForecast) return;
      onLoadFallScene.Invoke();
    }

    public void LoadFloorScene()
    {
      if (IsCalledInForecast) return;
      Debug.Log("LoadFloorScene");
      onLoadFloorScene.Invoke();
    }

    public void HideLocketteSprite() => onHideLockette.Invoke();

    public void HideCigarSprite() => onHideCigar.Invoke();
    public void PickUpPin() => onPickUpPin.Invoke();

    public void LoadCrackScene() => onLoadCrackScene.Invoke();
    
  }
}