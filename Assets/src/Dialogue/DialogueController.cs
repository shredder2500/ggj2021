using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Articy.Lose_Change;
using Articy.Unity;
using Articy.Unity.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour, IArticyFlowPlayerCallbacks
{
  [SerializeField] private Text dialogueText;
  [SerializeField] private Text speakerText;
  [SerializeField] private Image speakerImage;
  [SerializeField] private Canvas dialogueCanvas;
  [SerializeField] private Transform optionsContainer;
  
  [SerializeField] private Button optionBtn;
  [SerializeField] private ControlsAsset controls;
  [SerializeField] private float displaySpeed = .05f;

  private ArticyFlowPlayer _flowPlayer;
  private List<(string displayText, Branch branch)> _options;
  private readonly List<GameObject> _btns = new List<GameObject>();
  public bool skip = true;
  private AudioSource audioSource;

  private void Start()
  {
    _flowPlayer = GetComponent<ArticyFlowPlayer>();
    audioSource = GetComponent<AudioSource>();
  }
  
  public void OnFlowPlayerPaused(IFlowObject aObject)
  {
    StopAllCoroutines();
    _btns.ForEach(Destroy);
    if (aObject == null)
    {
      dialogueCanvas.gameObject.SetActive(false);
      controls.EnableGamePlay();
      controls.DisableUIControls();
      return;
    }

    dialogueCanvas.gameObject.SetActive(true);
    controls.DisableGamePlay();
    controls.EnableUIControls();

    if (aObject is Dialogue_With_Sound soundObj)
    {
      // Debug.Log(soundObj.Template.Dialogue_Sound.DialogueSound.Children[0].Id);
      var a = (Asset) soundObj.GetFeatureDialogue_Sound().DialogueSound;
      var sound = a.LoadAsset<AudioClip>();
      
      audioSource.clip = sound;
      audioSource.Play();
    }

    if (aObject is IObjectWithText textObj)
    {
      dialogueText.text = string.Empty;
      StartCoroutine(DisplayText(textObj.Text));
    }

    if (aObject is IObjectWithSpeaker speakerObj)
    {
      speakerText.gameObject.SetActive(true);
      speakerImage.gameObject.SetActive(true);
      var entity = (Entity) speakerObj.Speaker;
      speakerText.text = entity.DisplayName;;
      speakerImage.sprite = entity.PreviewImage.Asset?.LoadAssetAsSprite();
    }
    else
    {
      speakerText.gameObject.SetActive(false);
      speakerImage.gameObject.SetActive(false);
    }
  }

  private IEnumerator DisplayText(string text)
  {
    skip = false;
    controls.Submit += Skip;
    EventSystem.current.SetSelectedGameObject(null);
    for (var i = 0; i < text.Length; i++)
    {
      dialogueText.text += text[i];
      if (!skip)
      {
        yield return new WaitForSeconds(displaySpeed);
      }
    }

    controls.Submit -= Skip;
    _options.ForEach(option =>
    {
      var btn = Instantiate(optionBtn, optionsContainer);
      _btns.Add(btn.gameObject);
      btn.gameObject.SetActive(true);
      btn.GetComponentInChildren<Text>().text = option.displayText;
      btn.onClick.AddListener(() =>
      {
        audioSource.Stop();
        _flowPlayer.Play(option.branch);
      });
    });
    EventSystem.current.SetSelectedGameObject(_btns.Last());
  }

  public void OnBranchesUpdated(IList<Branch> aBranches)
  {
    if (aBranches.Any())
    {
      _options = aBranches.Aggregate(new List<(string displayText, Branch branch)>(), (list, branch) =>
      {
        if (branch.IsValid)
        {
          var text = "Continue...";
          if (branch.Target is IObjectWithMenuText menuTextObject)
          {
            text = string.IsNullOrEmpty(menuTextObject.MenuText) ? text : menuTextObject.MenuText;
          }
          list.Add((text, branch));
        }
        return list;
      });
      _options.Reverse();
    }
  }

  private void Skip() => skip = true;
}
