using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class Board : MonoBehaviour
{
    public TextAsset levelFile;
    public GameObject Player;
    public GameObject Floor;
    public GameObject NoMoveEnemy;
    public GameObject StandardMoveEnemy;
    public GameObject EveryOtherStandardMoveEnemy;
    public GameObject InverseMoveEnemy;

    List<GameObject[,]> BoardSteps;

    // Start is called before the first frame update
    void Start()
    {
        ParseLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ParseLevel()
    {
        GameObject[,] initialBoard;
        bool initialBoardinitialized = false;
        int x = 0;
        int y = 0;

        string levelFileContents = levelFile.text;
        string[] lines = Regex.Split(levelFileContents, "\n|\r|\r|\n");
        for(int i = 0; i < lines.Length; i++)
        {
            if(!initialBoardinitialized)
            {
                initialBoard = new GameObject[lines[i].Length,lines[i].Length];
                initialBoardinitialized = true;
            }
            
            foreach ( char c in lines[i])
            {
                // Node node = new Node();

                switch (c)
                {
                    case '.':
                        Debug.Log("There's a VOID here, at " + x + ", " + y);
                        break;
                    case '_':
                        // node.Tile = new Floor();
                        Debug.Log("There's an empty floor here, at " + x + ", " + y);
                        break;
                    case 'P':
                        // node.Tile = new Floor();
                        // node.Entity = new Player();
                        Debug.Log("There's a player here, at " + x + ", " + y);
                        break;
                    case '0':
                        // node.Tile = new Floor();
                        // node.Entity = new ZeroMoveEnemy();
                        Debug.Log("There's a ZEROMOVEENEMY here, at " + x + ", " + y);
                        break;
                    case '1':
                        // node.Tile = new Floor();
                        // node.Entity = new StandardMovementEnemy();
                        Debug.Log("There's a STANDARDMOVENEMY here, at " + x + ", " + y);
                        break;
                    case 'E':
                        // node.Tile = new Floor();
                        // node.Entity = new EveryOtherMovementEnemy();
                        Debug.Log("There's a EVERYOTHERMOVEMENT ENEMY here, at " + x + ", " + y);
                        break;
                    case 'I':
                        // node.Tile = new Floor();
                        // node.Entity = new InverseMovementEnemy();
                        Debug.Log("There's a INVERSEMOVEENEMY here, at " + x + ", " + y);
                        break;
                    default:
                        throw new System.StackOverflowException("YOU DONE BROKE THE GGAME WITH YOUR DUMB CSV FILER!");
                        break;
                }

                // initialBoard[x,y] = node;
                x++;
            }
            x = 0;
            y++;
        }
    }
}
