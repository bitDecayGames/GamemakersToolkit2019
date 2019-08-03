using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class Board : MonoBehaviour
{
    public TextAsset levelFile;
    public GameObject Node;

    public GameObject Floor;
    public GameObject ConsumeFloor;
    public GameObject Void;

    public GameObject Player;
    public GameObject NoMoveEnemy;
    public GameObject StandardMoveEnemy;
    public GameObject EveryOtherStandardMoveEnemy;
    public GameObject InverseMoveEnemy;

    List<List<List<GameObject>>> BoardSteps = new List<List<List<GameObject>>>();

    float xOffset;
    float yOffset;

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
        List<List<GameObject>> initialBoard = new List<List<GameObject>>();
        int y = 0;

        string levelFileContents = levelFile.text;
        string[] lines = Regex.Split(levelFileContents, "\n|\r|\r|\n");
        for(int i = 0; i < lines.Length; i++)
        {
            initialBoard.Add(new List<GameObject>());
            foreach ( char c in lines[i])
            {
                GameObject newNode = Instantiate(Node);
                GameObject newTile;
                GameObject newEntity;

                switch (c)
                {
                    // Void Node
                    case '#':
                        newTile = Instantiate(Void);
                        newTile.transform.parent = newNode.transform;
                        newNode.GetComponent<Node>().tile = newTile;

                        break;
                    // Empty Floor Node
                    case '.':
                        newTile = Instantiate(Floor);
                        newTile.transform.parent = newNode.transform;
                        newNode.GetComponent<Node>().tile = newTile;

                        break;
                    // Player Node
                    case 'P':
                        newTile = Instantiate(Floor);
                        newTile.transform.parent = newNode.transform;
                        newNode.GetComponent<Node>().tile = newTile;

                        newEntity = Instantiate(Player);
                        newEntity.transform.parent = newNode.transform;
                        newNode.GetComponent<Node>().entity = newEntity;

                        break;
                    // No Move Enemy Node
                    case 'u':
                        newTile = Instantiate(Floor);
                        newTile.transform.parent = newNode.transform;
                        newNode.GetComponent<Node>().tile = newTile;
                        
                        newEntity = Instantiate(NoMoveEnemy);
                        newEntity.transform.parent = newNode.transform;
                        newNode.GetComponent<Node>().entity = newEntity;

                        break;
                    // Standard Move Enemy Node
                    case 'o':
                        newTile = Instantiate(Floor);
                        newTile.transform.parent = newNode.transform;
                        newNode.GetComponent<Node>().tile = newTile;
                        
                        newEntity = Instantiate(StandardMoveEnemy);
                        newEntity.transform.parent = newNode.transform;
                        newNode.GetComponent<Node>().entity = newEntity;

                        break;
                    // Every Other Move Enemy Node
                    case 's':
                        newTile = Instantiate(Floor);
                        newTile.transform.parent = newNode.transform;
                        newNode.GetComponent<Node>().tile = newTile;
                        
                        newEntity = Instantiate(EveryOtherStandardMoveEnemy);
                        newEntity.transform.parent = newNode.transform;
                        newNode.GetComponent<Node>().entity = newEntity;

                        break;
                    // Inverse Move Enemy Node
                    case 'i':
                        newTile = Instantiate(Floor);
                        newTile.transform.parent = newNode.transform;
                        newNode.GetComponent<Node>().tile = newTile;
                        
                        newEntity = Instantiate(InverseMoveEnemy);
                        newEntity.transform.parent = newNode.transform;
                        newNode.GetComponent<Node>().entity = newEntity;

                        break;
                    // Consume Floor Node
                    case '&':
                        newTile = Instantiate(ConsumeFloor);
                        newTile.transform.parent = newNode.transform;
                        newNode.GetComponent<Node>().tile = newTile;
                        break;
                    default:
                        throw new System.StackOverflowException("YOU DONE BROKE THE GGAME WITH YOUR DUMB CSV FILER!");
                        break;
                }

                newNode.transform.parent = gameObject.transform;
                initialBoard[y].Add(newNode);
            }
            y++;
        }
    }
}
