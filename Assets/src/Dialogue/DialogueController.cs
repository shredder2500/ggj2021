using System.Collections.Generic;
using System.Linq;
using Articy.Lose_Change;
using Articy.Unity;
using Articy.Unity.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour, IArticyFlowPlayerCallbacks
{
  [SerializeField] private Text dialogueText;
  [SerializeField] private Text speakerText;
  [SerializeField] private Image speakerImage;
  [SerializeField] private Canvas dialogueCanvas;
  
  [SerializeField] private List<Button> optionBtns;
  [SerializeField] private ControlsAsset controls;

  private ArticyFlowPlayer _flowPlayer;

  private void Start()
  {
    _flowPlayer = GetComponent<ArticyFlowPlayer>();
  }
  
  public void OnFlowPlayerPaused(IFlowObject aObject)
  {
    if (aObject == null)
    {
      dialogueCanvas.gameObject.SetActive(false);
      controls?.EnableGamePlay();
      return;
    }

    dialogueCanvas.gameObject.SetActive(true);
    controls?.DisableGamePlay();

    if (aObject is IObjectWithText textObj)
    {
      dialogueText.text = textObj.Text;
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

  public void OnBranchesUpdated(IList<Branch> aBranches)
  {
    Assert.IsTrue(aBranches.Count <= optionBtns.Count, $"There are more than {optionBtns.Count} branches but only {optionBtns.Count} buttons setup");
    optionBtns.ForEach(x =>
    {
      x.gameObject.SetActive(false);
      x.onClick.RemoveAllListeners();
    });

    if (aBranches.Any())
    {
      var options = aBranches.Aggregate(new List<(string displayText, Branch branch)>(), (list, branch) =>
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
      
      options.ForEach(option =>
      {
        var idx = options.IndexOf(option);
        var btn = optionBtns[idx];
        btn.gameObject.SetActive(true);
        btn.GetComponentInChildren<Text>().text = option.displayText;
        btn.onClick.AddListener(() => _flowPlayer.Play(option.branch));
      });
    }
  }
}
