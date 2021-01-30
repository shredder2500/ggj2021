using System;
using UnityEngine;

[CreateAssetMenu]
public class ControlsAsset : ScriptableObject
{
  private Controls _controls;
  
  public event Action<Vector2> OnMoveInput;
  public event Action Interact;
  public event Action Submit;

  public void Init()
  {
    _controls = new Controls();
    _controls.Enable();
    _controls.GamePlay.Enable();

    _controls.GamePlay.Move.performed += ctx => OnMoveInput?.Invoke(ctx.ReadValue<Vector2>());
    _controls.GamePlay.Move.canceled += _ => OnMoveInput?.Invoke(Vector2.zero);
    _controls.GamePlay.Interact.performed += _ => Interact?.Invoke();
  }

  public void DisableGamePlay() => _controls.GamePlay.Disable();
  public void EnableGamePlay() => _controls.GamePlay.Enable();
}
