using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Board : MonoBehaviour
{
    public string levelFile;
    public GameObject Player;
    public GameObject Floor;
    public GameObject ZeroMoveEnemy;
    public GameObject StandardMovementEnemy;
    public GameObject EveryOtherMovementEnemy;
    public GameObject InverseMovementEnemy;

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

        StreamReader reader = File.OpenText(levelFile);
        string line;
        while((line = reader.ReadLine()) != null)
        {
            if(!initialBoardinitialized)
            {
                initialBoard = new GameObject[line.Length,line.Length];
                initialBoardinitialized = true;
            }
            
            foreach ( char c in line)
            {
                // Node node = new Node();

                switch (c)
                {
                    case '.':
                        break;
                    // case '_':
                    //     node.Tile = new Floor();
                    //     break;
                    // case 'P':
                    //     node.Tile = new Floor();
                    //     node.Entity = new Player();
                    //     break;
                    // case '0':
                    //     node.Tile = new Floor();
                    //     node.Entity = new ZeroMoveEnemy();
                    //     break;
                    // case '1':
                    //     node.Tile = new Floor();
                    //     node.Entity = new StandardMovementEnemy();
                    //     break;
                    // case 'E':
                    //     node.Tile = new Floor();
                    //     node.Entity = new EveryOtherMovementEnemy();
                    //     break;
                    // case 'I':
                    //     node.Tile = new Floor();
                    //     node.Entity = new InverseMovementEnemy();
                    //     break;
                    default:
                        throw new System.StackOverflowException("YOU DONE BROKE THE GGAME WITH YOUR DUMB CSV FILER!");
                        break;
                }

                // initialBoard[x,y] = node;
                x++;
            }
            y++;
        }
    }
}
