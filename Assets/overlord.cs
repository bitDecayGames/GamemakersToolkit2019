using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class overlord : MonoBehaviour {
    //current level
    public int currentLevel;
    private int nextLevel;

    public Board Board;

    private void Start() {
        nextLevel = currentLevel + 1;
    }

    // Update is called once per frame
    void Update() {
        bool acceptingInput = true;

        //some timer controlling acceptingInput

        Vector2 input = new Vector2();
        if (acceptingInput) {
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                input = Directions.North;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                input = Directions.South;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                input = Directions.West;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                input = Directions.East;
            }

            if (Input.GetKeyDown(KeyCode.Backspace)) {
                //un-does the previous move
                //Board.undo();
            }

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
        }

        //Send input to board
        //Board.boardInput(input);

        //Did game end
        //may need more meta data for the types of foods that were skewered on the level
        //for better graphics on the win screen.
        GameOverStatus gameOver = new GameOverStatus(GameOverReason.SKEWERD_YOURSELF);

        //gameOver = Board.getGameOverStatus();

        if (gameOver != null) {
            if (gameOver.win) {
                //player won
                //TODO :scene transition 
                //SceneManager.LoadScene("Level" + nextLevel.ToString());
            } else {
                //player lost 
                //TODO :scene transition 
                //SceneManager.LoadScene("Level" + currentLevel.ToString());
            }
        }
    }
}