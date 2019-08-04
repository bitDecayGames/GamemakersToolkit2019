using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class overlord : MonoBehaviour {
    private bool inSkewerThrowMode = false;
    private bool acceptingInput = true;

    public Board Board;

    void Update() {
        //some timer controlling acceptingInput
        bool gotInput = false;
        Vector2 input = new Vector2();
        if (acceptingInput) {
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                input = Directions.North;
                gotInput = true;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                input = Directions.South;
                gotInput = true;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                input = Directions.West;
                gotInput = true;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                input = Directions.East;
                gotInput = true;
            }

            //OUT OF SCOPE
            //if (Input.GetKeyDown(KeyCode.Backspace)) {
            //    //un-does the previous move
            //    //Board.undo();
            //}

            if (Input.GetKeyDown(KeyCode.R)) {
                //resets the level
                //Board.goToLevel(current)
                //Board.resetLevel();??
            }

            if (Input.GetKeyDown(KeyCode.Escape)) {
                //quits the game to menu
            }

            if (Input.GetKeyDown(KeyCode.P)) {
                //unpause game if paused, otherwise pause
                if (Time.timeScale == 0) {
                    Time.timeScale = 1;
                    //stop pause music
                    //start normal game music
                    //***If we care, probably don't***
                } else {
                    Time.timeScale = 0;
                    //stop normal game music
                    //start pause music
                    //***If we care, probably don't***
                }
            }

            if (Input.GetKeyDown(KeyCode.Space)) {
                inSkewerThrowMode = true;
            }
        }

        GameOverStatus status = null;
        //Send input to board
        if (gotInput) {
            if (inSkewerThrowMode) {
                status = Board.ThrowSkewer(input);
                inSkewerThrowMode = false;
            } else {
                if (Board.RequestedMoveValid(input)) {
                    //when not in throw mode, past directional input as movement
                    status = Board.NextBoardState(input);
                }
            }
        }

        if (status != null) {
            var endGamePlayer = FindObjectOfType<TintCanvasController>();
            if (endGamePlayer == null) throw new Exception("We forgot to put the TintCanvasController on the scene, whoops...");
            acceptingInput = false;

            if (status.win) {
                //player won
                endGamePlayer.Success(() => {
                    Debug.Log("The user pressed space to go to the next scene");
                    endGamePlayer.Reset();
                    // Go to next level
                    if (CurrentLevelNumber.Instance.LevelNumber + 1 > LevelContent.levels.Count) {
                        // they won the entire game!
                        goToScene("Credits");
                    } else {
                        CurrentLevelNumber.Instance.LevelNumber += 1;
                        goToScene(SceneManager.GetActiveScene().name);
                    }
                });
            } else {
                //player lost 
                endGamePlayer.Fail(() => {
                    Debug.Log("The user pressed space to restart this level");
                    endGamePlayer.Reset();
                    // Restart this level
                    goToScene(SceneManager.GetActiveScene().name);
                });
            }
        }
    }

    private void goToScene(String sceneName) {
        var nav = FindObjectOfType<EasyNavigator>();
        if (nav == null) throw new Exception("We forgot to put the EasyNavigator here, our bad");
        nav.GoToSceneWithSoundWithClick(sceneName);
    }
}