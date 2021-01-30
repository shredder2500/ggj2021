using UnityEngine;
using static UnityEngine.Mathf;
using static UnityEngine.Time;
using static UnityEngine.Vector2;

public class MovementController : MonoBehaviour
{
  [SerializeField] private ControlsAsset controls;
  [SerializeField] private float speed = 5;

  private Vector2 _input;
  private Rigidbody2D _rigidbody2D;
  private RaycastHit2D[] _hits = new RaycastHit2D[100];
  private Collider2D _collider2D;

  private void Start()
  {
    controls.Init();
    controls.OnMoveInput += x => _input = x.normalized;
    _rigidbody2D = GetComponent<Rigidbody2D>();
  }

  private void FixedUpdate()
  {
    var deltaSpeed = speed * fixedDeltaTime;
    var hitCount = _rigidbody2D.Cast(_input, _hits, deltaSpeed);

    var collided = false;
    var moveDelta = _input * deltaSpeed;

    for (var i = 0; i < hitCount && !collided; i++)
    {
      var contact = _hits[i];
      var distance = contact.distance;

      if (distance > Epsilon)
      {
        moveDelta = _input * distance;
        _rigidbody2D.MovePosition(_rigidbody2D.position + moveDelta);
        collided = true;
      }
    }

    if (!collided)
    {
      _rigidbody2D.MovePosition(_rigidbody2D.position + moveDelta);
    }
  }
}
