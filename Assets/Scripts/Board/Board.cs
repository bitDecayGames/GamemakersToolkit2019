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

    const float TileHeight = 1;
    const float TileWidth = 1;

    float xOffset;
    float yOffset;

    // Start is called before the first frame update
    void Start()
    {
        ParseLevel();
        // DebugBoardSteps();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    List<Vector2> FindEntityCoords(Entity entity)
    {
        List<Vector2> entityCoords = new List<Vector2>();
        int latest = BoardSteps.Count-1;

        for (int y = 0; y < BoardSteps[latest].Count; y++)
        {
            for (int x = 0; x < BoardSteps[latest][y].Count; x++)
            {
                GameObject tempEntity = BoardSteps[latest][y][x].GetComponent<Node>().entity;
                if(tempEntity != null && entity.Name == tempEntity.GetComponent<Entity>().Name)
                {
                    entityCoords.Add(new Vector2(x, y));
                }
            }
            
        }

        return entityCoords;
    }

    bool RequestedMoveValid(Vector2 direction)
    {
        int latest = BoardSteps.Count -1;
        List<Vector2> playerCoord = FindEntityCoords(Player.GetComponent<Entity>());

        if(playerCoord.Count != 1)
        {
            throw new System.StackOverflowException("THERE IS NO PLAYER!!! OH GOD NO!!!!!!!!!");
        }

        Vector2 nodeCoordToCheck = playerCoord[0] + direction;
        GameObject nodeToCheckGO = BoardSteps[latest][(int) nodeCoordToCheck.y][(int) nodeCoordToCheck.x];
        Node nodeToCheck = nodeToCheckGO.GetComponent<Node>();

        if(nodeToCheck != null && nodeToCheck.tile.GetComponent<Tile>().Standable)
        {
            return true;
        }
        return false;
    }

    // List<List<GameObject>> NextBoardState()
    // {

    // }

    // void Undo()
    // {

    // }

    // bool WinCondition()
    // {

    // }

    void DebugBoardSteps()
    {
        for (int y = 0; y < BoardSteps[0].Count;y++)
        {
            for(int x = 0; x < BoardSteps[0][y].Count; x++)
            {
                Debug.Log("POS X,Y: " + x + "," + y);
                Debug.Log("Tyle: " + BoardSteps[0][y][x].GetComponent<Node>().tile.name);
                var tempEnt = BoardSteps[0][y][x].GetComponent<Node>().entity;
                if(tempEnt != null)
                {
                    Debug.Log("Entyty: " + tempEnt);
                }
            }
        }
    }

    void ParseLevel()
    {
        List<List<GameObject>> initialBoard = new List<List<GameObject>>();

        string levelFileContents = levelFile.text;
        string[] lines = Regex.Split(levelFileContents, "\n|\r|\r|\n");

        for(int y = 0; y < lines.Length; y++)
        {
            if(lines[y].Length == 0) continue;

            initialBoard.Add(new List<GameObject>());

            for (int x = 0; x < lines[y].Length; x++)
            {
                GameObject newNode = Instantiate(Node, new Vector3(TileWidth*x, TileHeight*y, 0), Quaternion.identity);
                GameObject newTile;
                GameObject newEntity;

                switch (lines[y][x])
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
                    case 'z':

                        // 's' is supposed to be a Sleeper that starts awake
                        // while 'z' is one that starts asleep. For now, let's
                        // just treat them the same
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
                    case '{':
                    case '}':
                    case '[':
                    case ']':
                        // For reflectors, not ready yet, just use floor for now
                        newTile = Instantiate(Floor);
                        newTile.transform.parent = newNode.transform;
                        newNode.GetComponent<Node>().tile = newTile;

                        break;
                    default:
                        throw new System.StackOverflowException("YOU DONE BROKE THE GGAME WITH YOUR DUMB CSV FILER!");
                    }

                newNode.transform.parent = gameObject.transform;
                initialBoard[y].Add(newNode);
            }
        }
        BoardSteps.Add(initialBoard);
    }
}
