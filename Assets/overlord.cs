using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overlord : MonoBehaviour
{

    public Board Board;

    // Update is called once per frame
    void Update()
    {
        bool acceptingInput = true;

        //first level
        int levelId = 01;

        //some timer controlling acceptingInput

        Vector2 input = new Vector2();
        if(acceptingInput)
            {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                input = Directions.North;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                input = Directions.South;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                input = Directions.West;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                input = Directions.East;
            }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                //un-does the previous move
                //Board.undo();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                //resets the leve
                //Board.goToLevel(current)
                //Board.resetLevel();??
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //quits the game to menu
            }
        }

        //Send input to board
        //Board.boardInput(input);

        //Did game end
        //may need more meta data for the types of foods that were skewered on the level
        //for better graphics on the win screen.
        GameOverStatus gameOver = new GameOverStatus();

        //gameOver = Board.getGameOverStatus();

        if (gameOver.isLevelDone)
        {
            if (gameOver.playerWin)
            {
                //player won
                //go to next level
            }
            else
            {
                //player lost 
                //restart current level
            }
        }

    }
}
