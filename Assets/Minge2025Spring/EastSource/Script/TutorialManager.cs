using System;
using Cost;
using UnityEngine;
using UnityEngine.Serialization;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance{get; private set;}

    private float showUITimeController = 0;
    private float showUITimer = 0;
    private bool isShowingTutorialUI = true;
    private bool isShowingPuzzleOnOffButtonArrow = false;
    private bool isShowingCharacterUIButtonArrow = false;
    
    private const int TUTORIAL_NEXT_STEP_VALUE = 20;

    [Header("Binding")]
    [SerializeField] private GameObject tutorialUI; 
    [SerializeField] private GameObject puzzleOnOffButtonArrow;
    [SerializeField] private CostControllerModel costControllerModel;
    [SerializeField] private GameObject characterUIButtonArrow;

    private void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        showUITimer += Time.deltaTime * showUITimeController;
        if (showUITimer >= 6f)
        {
            HideCharacterUIButtonArrow();
            showUITimer = 0;
            showUITimeController = 0;
            HideTutorialUI();
        }
    }

    public void ShowPuzzleOnOffButtonArrow()
    {
        if (!isShowingPuzzleOnOffButtonArrow && isShowingTutorialUI)
        {
            puzzleOnOffButtonArrow.SetActive(true);
            isShowingPuzzleOnOffButtonArrow = true;
        }
    }

    public void HidePuzzleOnOffButtonArrow()
    {
        if (isShowingPuzzleOnOffButtonArrow && isShowingTutorialUI)
        {
            puzzleOnOffButtonArrow.SetActive(false);
            isShowingPuzzleOnOffButtonArrow = false;
        }
    }

    public void ShowtutorialUI()
    {
        if (!isShowingTutorialUI)
        {
            tutorialUI.SetActive(true);
            isShowingTutorialUI = true;
        }
    }

    public void HideTutorialUI()
    {
        if (isShowingTutorialUI)
        {
            tutorialUI.SetActive(false);
            isShowingTutorialUI = false;
        }
    }

    public void ShowCharacterUIButtonArrow()
    {
        if (!isShowingCharacterUIButtonArrow 
            && isShowingTutorialUI && costControllerModel.Cost.Value >= TUTORIAL_NEXT_STEP_VALUE)
        {
            characterUIButtonArrow.SetActive(true);
            isShowingCharacterUIButtonArrow = true;
            StartUITimer();
        }
    }

    public void HideCharacterUIButtonArrow()
    {
        if (isShowingCharacterUIButtonArrow && isShowingTutorialUI)
        {
            characterUIButtonArrow.SetActive(false);
            isShowingCharacterUIButtonArrow = false;
        }
    }

    private void StartUITimer()
    {
        showUITimeController = 1;
    }
}
