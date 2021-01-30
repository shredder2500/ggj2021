using System;
using UnityEngine;

using static UnityEngine.Mathf;

namespace src.Controllers
{
  public class SortOnY : MonoBehaviour
  {
    private SpriteRenderer _renderer;

    private void Start() => _renderer = GetComponent<SpriteRenderer>();

    private void Update() => _renderer.sortingOrder = FloorToInt(-transform.position.y * 100);
  }
}