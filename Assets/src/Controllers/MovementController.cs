using UnityEngine;

public class MovementController : MonoBehaviour
{
  [SerializeField] private ControlsAsset controls;
  [SerializeField] private float speed = 5;

  private Vector2 _input;

  private void Start()
  {
    controls.Init();
    controls.OnMoveInput += x => _input = x;
  }

  private void Update()
  {
    var delta = _input * (speed * Time.deltaTime);
    
    transform.position += new Vector3(delta.x, delta.y, 0);
  }
}
