﻿using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public CameraFollow cameraFollow;
    public int currentBirdIndex;
    public SlingShot slingshot;
    [HideInInspector]
    public static GameState CurrentGameState = GameState.Start;
    private List<GameObject> Bricks;
    [SerializeField] private List<GameObject> Birds;
    private List<GameObject> Pigs;

    void Start()
    {
        Instantiate(Birds[PlayerPrefs.GetInt("selectedCharacter1")], Birds[PlayerPrefs.GetInt("selectedCharacter1")].transform.position, Quaternion.identity).SetActive(true);
        Instantiate(Birds[PlayerPrefs.GetInt("selectedCharacter2")], Birds[PlayerPrefs.GetInt("selectedCharacter2")].transform.position, Quaternion.identity).SetActive(true);
        Instantiate(Birds[PlayerPrefs.GetInt("selectedCharacter3")], Birds[PlayerPrefs.GetInt("selectedCharacter3")].transform.position, Quaternion.identity).SetActive(true);
        CurrentGameState = GameState.Start;
        slingshot.enabled = false;
        Bricks = new List<GameObject>(GameObject.FindGameObjectsWithTag("Brick"));
        Birds = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bird"));
        Pigs = new List<GameObject>(GameObject.FindGameObjectsWithTag("Pig"));
        slingshot.BirdThrown -= Slingshot_BirdThrown; slingshot.BirdThrown += Slingshot_BirdThrown;
    }

    void Update()
    {
        switch (CurrentGameState)
        {
            case GameState.Start:
                if (Input.GetMouseButtonUp(0))
                    AnimateBirdToSlingshot();
                break;
            case GameState.BirdMovingToSlingshot:
                break;
            case GameState.Playing:
                if (slingshot.slingshotState == SlingshotState.BirdFlying &&
                    (BricksBirdsPigsStoppedMoving() || Time.time - slingshot.TimeSinceThrown > 5f))
                {
                    slingshot.enabled = false;
                    AnimateCamera_ToStartPosition();
                    CurrentGameState = GameState.BirdMovingToSlingshot;
                }
                break;
            case GameState.Won:
            case GameState.Lost:
                if (Input.GetMouseButtonUp(0))
                    //Application.LoadLevel(Application.loadedLevel);   OBSOLETO
                    SceneManager.LoadScene("Game");
                break;
            default:
                break;
        }
    }
    
    private bool TodosLosCerdosDestruidos()
    {
        return Pigs.All(x => x == null);
    }

    private void AnimateCamera_ToStartPosition()
    {
        float duration = Vector2.Distance(Camera.main.transform.position, cameraFollow.StartingPosition) / 10f;
        if (duration == 0.0f) duration = 0.1f;
        //animate the camera to start
        Camera.main.transform.positionTo(duration,cameraFollow.StartingPosition). //end position
            setOnCompleteHandler((x) =>
            {
                cameraFollow.IsFollowing = false;
                if (TodosLosCerdosDestruidos())
                    CurrentGameState = GameState.Won;
                else if (currentBirdIndex == Birds.Count - 1)
                    CurrentGameState = GameState.Lost;
                else
                {
                    slingshot.slingshotState = SlingshotState.Idle;
                    currentBirdIndex++;
                    AnimateBirdToSlingshot();
                }
            });
    }

    void AnimateBirdToSlingshot()
    {
        CurrentGameState = GameState.BirdMovingToSlingshot;
        Birds[currentBirdIndex].transform.positionTo
            (Vector2.Distance(Birds[currentBirdIndex].transform.position / 10,
            slingshot.BirdWaitPosition.transform.position) / 10, //duration
            slingshot.BirdWaitPosition.transform.position). //final position
                setOnCompleteHandler((x) =>
                        {
                            x.complete();
                            x.destroy();
                            CurrentGameState = GameState.Playing;
                            slingshot.enabled = true;
                            slingshot.BirdToThrow = Birds[currentBirdIndex];
                        });
    }

    private void Slingshot_BirdThrown(object sender, System.EventArgs e)
    {
        cameraFollow.BirdToFollow = Birds[currentBirdIndex].transform;
        cameraFollow.IsFollowing = true;
    }

    bool BricksBirdsPigsStoppedMoving()
    {
        foreach (var item in Bricks.Union(Birds).Union(Pigs))
        {
            if (item != null && item.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > Constants.Min_Velocity)
            {
                return false;
            }
        }

        return true;
    }

    public static void AutoResize(int screenWidth, int screenHeight)
    {
        Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
    }

    void OnGUI()
    {
        AutoResize(800, 480);
        switch (CurrentGameState)
        {
            case GameState.Start:
                GUI.Label(new Rect(0, 150, 200, 100), "Tap the screen to start");
                break;
            case GameState.Won:
                GUI.Label(new Rect(0, 150, 200, 100), "You Win! Tap the screen to restart");
                break;
            case GameState.Lost:
                GUI.Label(new Rect(0, 150, 200, 100), "You Lost! Tap the screen to restart");
                break;
            default:
                break;
        }
    }
}
