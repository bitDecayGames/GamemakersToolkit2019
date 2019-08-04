using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class overlord : MonoBehaviour {
    //current level
    public int currentLevel;
    private int nextLevel;
    private bool inSkewerThrowMode = false;
    private bool acceptingInput = true;

    public Board Board;

    private void Start() {
        nextLevel = currentLevel + 1;
    }

    // Update is called once per frame
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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                inSkewerThrowMode = true;
            }
        }
        GameOverStatus status = null;
        //Send input to board
        if (gotInput)
        {
            if (inSkewerThrowMode)
            {
                status = Board.ThrowSkewer(input);
                inSkewerThrowMode = false;
                acceptingInput = false;
            }
            else
            {
                if (Board.RequestedMoveValid(input))
                {
                    //when not in throw mode, past directional input as movement
                    status = Board.NextBoardState(input);
                }
            }
        }

        if (status != null) {
            if (status.win) {
                Debug.Log("WE KAAAAA-WON!!!:" + status.reason);
                //player won
                //TODO :scene transition 
                //SceneManager.LoadScene("Level" + nextLevel.ToString());
            } else {
                Debug.Log("WE KAAAAA-LOST!!!:" + status.reason);
                //player lost 
                //TODO :scene transition 
                //SceneManager.LoadScene("Level" + currentLevel.ToString());
            }
        }
    }
}