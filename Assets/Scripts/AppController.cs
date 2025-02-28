using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using System;
using NUnit.Framework.Api;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class AppController : MonoBehaviour
{
    [SerializeField] GameObject GameOverAlert;
    [SerializeField] GameObject HUDPanel;
    [SerializeField] GameObject WelcomePanel;
    [SerializeField] GameObject StartGamePanel;
    // [SerializeField] GameObject CapybaraCharacter;
    [SerializeField] Button InitARButton;
    [SerializeField] Button InitARGame;
    [SerializeField] Transform UICanvasTransform;

    [SerializeField] GameObject ARContentPrefab;

    [SerializeField] ARTrackedImageManager m_ImageManager;

    public static AppController SharedInstance;
    // public GameObject spawnOrigin;
    public bool isRunning = false;
    // public float spawnTime = 1.6f;
    public int points = 0;
    public int[] leaderPoints = new int[] { 0, 0, 0, 0, 0 };
    Vector3 initialGameOverAlertPos;
    int scoreCount = 0; // Tracks how many scores have been added
    GameOverAlertController GameOverAlertController;
    ARContentController arController;
    public TextMeshProUGUI pointsUIText;
    GameObject ARContent;
    public bool startTracking = false, isPlaying = false;

    private void Awake()
    {
        SharedInstance = this;
    }


    void OnEnable() => m_ImageManager.trackablesChanged.AddListener(OnChanged);

    void OnDisable() => m_ImageManager.trackablesChanged.RemoveListener(OnChanged);


    // Start is called before the first frame update
    void Start()
    {
        pointsUIText = HUDPanel.transform.Find("UpperPanel/TextPoints").GetComponent<TextMeshProUGUI>();
        isRunning = false;
        isPlaying = false;
        GameOverAlertController = GameOverAlert.GetComponent<GameOverAlertController>();
        initialGameOverAlertPos = GameOverAlert.transform.localPosition;

        // GameOverAlert.SetActive(false);
        // HUDPanel.SetActive(false);

        InitARButton.onClick.AddListener(() =>
        {
            WelcomePanel.SetActive(false);
            startTracking = true;
        }
        );
        InitARGame.onClick.AddListener(() =>
        {
            StartGamePanel.SetActive(false);
            StartARGame();
        });
    }

    void OnChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {

        foreach (ARTrackedImage newTrackedImage in eventArgs.added)
        {
            // Handle added event
            InitializeARGame(newTrackedImage);
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            // Handle updated event
            if (startTracking)
                UpdateImage(updatedImage);
        }

        foreach (var removed in eventArgs.removed)
        {
            // Handle removed event
            TrackableId removedImageTrackableId = removed.Key;
            ARTrackedImage removedImage = removed.Value;
        }
    }

    void UpdateImage(ARTrackedImage trackedImage)
    {

        if (trackedImage == null) return;
        string name = trackedImage.referenceImage.name;
        if (name == "ARCapyMarker")
        {
            if (trackedImage.trackingState == TrackingState.Tracking && !isPlaying && startTracking)
                ShowStartGamePanel();
            if (trackedImage.trackingState != TrackingState.Tracking)
            {
                isRunning = false;
                isPlaying = false;
                arController.StopRunning();
                HUDPanel.SetActive(false);
            }

        }

    }

    void InitializeARGame(ARTrackedImage trackedImage)
    {
        // Debug.Log("InitializeARGameScene.......");
        if (trackedImage == null) return;
        string name = trackedImage.referenceImage.name;
        Debug.Log("Image Detected: " + name);
        if (name == "ARCapyMarker")
        {
            ARContent = Instantiate(ARContentPrefab, trackedImage.transform.position, trackedImage.transform.rotation);
            ARContent.transform.parent = trackedImage.transform;
            arController = ARContent.GetComponent<ARContentController>();
            arController.InitARContent();
            HUDPanel.transform.Find("BottomPanel/LeftButton").GetComponent<Button>().onClick.AddListener(() =>
                arController.MoveLeft());
            HUDPanel.transform.Find("BottomPanel/RightButton").GetComponent<Button>().onClick.AddListener(() =>
                arController.MoveRight());
            // ShowStartGamePanel();
        }
    }
    void ShowStartGamePanel()
    {
        //TODO: Change Scanning Panel to TutorialInit()
        // WelcomePanel.SetActive(false);
        StartGamePanel.SetActive(true);
        StartGamePanel.transform.position = UICanvasTransform.position;
    }

    void StartARGame()
    {
        isPlaying = true;
        HUDPanel.SetActive(true);
        SetInitValues();
        arController.InitARContent();
        arController.StartGame();
        // StartCoroutine(RestartDelay());
    }

    void SetInitValues()
    {
        points = 0;
        pointsUIText.text = "0";
        //  CapybaraCharacter.transform.position = capyInitPos;
        // ObstaculePool.SharedInstance.ResetPool();
        // capyPlayerController.moveRight = false;
        // capyPlayerController.moveLeft = false;
        HUDPanel.transform.Find("BottomPanel/LeftButton").transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        HUDPanel.transform.Find("BottomPanel/RightButton").transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

    }

    public void GameOver()
    {
        AddScoreToLeaderBoard(points);
        GameOverAlert.SetActive(true);
        GameOverAlertController.UpdateScoreDisplay();
        HUDPanel.SetActive(false);
        iTween.MoveTo(GameOverAlert, iTween.Hash("position", new Vector3(0, 0, 0),
            "islocal", true, "time", 0.75f, "easeType", iTween.EaseType.easeOutBack, "delay", 0.3));
    }

    public void RestartGame()
    {
        // SetInitValues();
        StartARGame();
        iTween.MoveTo(GameOverAlert, iTween.Hash("position", initialGameOverAlertPos,
            "islocal", true, "time", 0.75f, "easeType", iTween.EaseType.easeOutBack, "delay", 0.15));
        // StartCoroutine(RestartDelay());
    }

    public void ExitGame()
    {
        //TODO: Add stop AR Engine
        startTracking = false;
        isPlaying = false;
        WelcomePanel.SetActive(true);
        GameOverAlert.SetActive(false);
        GameOverAlert.transform.localPosition = initialGameOverAlertPos;
    }

    public void AddScoreToLeaderBoard(int newScore)
    {
        if (scoreCount < leaderPoints.Length)
        {
            // Add the new score if there's space
            leaderPoints[scoreCount] = newScore;
            scoreCount++;

            // Sort the array in descending order
            Array.Sort(leaderPoints, 0, scoreCount);
            Array.Reverse(leaderPoints, 0, scoreCount);
        }
        else
        {
            // If the array is full, check if the new score is higher than any existing scores
            for (int i = 0; i < leaderPoints.Length; i++)
            {
                if (newScore > leaderPoints[i])
                {
                    // Insert the new score at this position
                    // Shift lower scores down to make room
                    for (int j = leaderPoints.Length - 1; j > i; j--)
                    {
                        leaderPoints[j] = leaderPoints[j - 1];
                    }

                    // Set the new score in the correct spot
                    leaderPoints[i] = newScore;
                    break;
                }
            }
        }

    }


}
