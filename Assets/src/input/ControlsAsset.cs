using System;
using UnityEngine;

[CreateAssetMenu]
public class ControlsAsset : ScriptableObject
{
  private Controls _controls;
  
  public event Action<Vector2> OnMoveInput;
  public event Action Interact;
  public event Action Submit;

  public bool GamePlayControlsEnabled;
  public bool UIControlsEnabled;

  public void Init()
  {
    if (_controls != null) return;
    
    _controls = new Controls();
    _controls.Enable();
    _controls.GamePlay.Enable();

    _controls.GamePlay.Move.performed += ctx => OnMoveInput?.Invoke(ctx.ReadValue<Vector2>());
    _controls.GamePlay.Move.canceled += _ => OnMoveInput?.Invoke(Vector2.zero);
    _controls.GamePlay.Interact.performed += _ => Interact?.Invoke();

    _controls.UI.Submit.performed += _ => Submit?.Invoke();

    GamePlayControlsEnabled = true;
    UIControlsEnabled = false;
  }

  public void DisableGamePlay()
  {
    GamePlayControlsEnabled = false;
    _controls.GamePlay.Disable();
  }

  public void EnableGamePlay()
  {
    GamePlayControlsEnabled = true;
    _controls.GamePlay.Enable();
  }

  public void EnableUIControls()
  {
    UIControlsEnabled = true;
    _controls.UI.Enable();
  }

  public void DisableUIControls()
  {
    UIControlsEnabled = false;
    _controls.UI.Disable();
  }
}
