using Articy.Unity;
using UnityEngine;

namespace src.Dialogue
{
  public class DialoguePlayer : MonoBehaviour
  {
    [SerializeField] private ArticyRef dialogue;

    private ArticyFlowPlayer _articyFlowPlayer;

    private void Start()
    {
      _articyFlowPlayer = GetComponentInParent<ArticyFlowPlayer>();
    }

    public void Play() => Play(false);

    public void Play(bool callPlay)
    {
      _articyFlowPlayer.StartOn = dialogue.GetObject();
      if (callPlay)
      {
        _articyFlowPlayer.Play();
      }
    }
  }
}