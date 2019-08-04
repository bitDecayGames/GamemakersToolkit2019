﻿using System;
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
    public SkewerThrower skewerThrower;

    List<List<List<GameObject>>> BoardSteps = new List<List<List<GameObject>>>();

    public const float TileHeight = 0.8f;
    public const float TileWidth = TileHeight;

    float xOffset;
    float yOffset;

    // Start is called before the first frame update
    void Start()
    {
        ParseLevel();
        // DebugBoardSteps();
        // FindAllEntityCoords();
        // Vector2 entityCoord = FindEntityCoords(StandardMoveEnemy.GetComponent<Entity>())[0];
        // Vector2 entityWantMove = FindEntityMove(entityCoord, Directions.East);
        // Debug.Log("Entity past coord: " + entityCoord.x + ", " + entityCoord.y);
        // Debug.Log("Entity future coord: " + entityWantMove.x + ", " + entityWantMove.y);
    }

 

    List<List<GameObject>> GetLatestBoardStep()
    {
        int latest = BoardSteps.Count -1;
        return BoardSteps[latest];
    }

    List<Vector2> FindEntityCoords(Entity entity)
    {
        int latest = BoardSteps.Count-1;
        List<Vector2> entityCoords = new List<Vector2>();

        List<Vector2> allEntities = FindAllEntityCoords();

        foreach (Vector2 coord in allEntities)
        {
            if (BoardSteps[latest][(int)coord.y][(int)coord.x].GetComponent<Node>().entity.GetComponent<Entity>().Name == entity.Name)
            {
                entityCoords.Add(coord);
            }
        }

        return entityCoords;
    }


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

    public GameOverStatus ThrowSkewer(Vector2 input)
    {        
        return SkewerLogic.ShootSkewer(GetLatestBoardStep().ConvertAll(row => row.ConvertAll(cell => cell.GetComponent<Node>())), input, skewerThrower);
    }

    List<Vector2> FindAllEntityCoords()
    {
        List<Vector2> entityCoords = new List<Vector2>();
        int latest = BoardSteps.Count-1;

        for (int y = 0; y < BoardSteps[latest].Count; y++)
        {
            for (int x = 0; x < BoardSteps[latest][y].Count; x++)
            {
                GameObject tempEntity = BoardSteps[latest][y][x].GetComponent<Node>().entity;
                if(tempEntity != null)
                {
                    Debug.Log("temp entity: " + tempEntity.GetComponent<Entity>().Name);
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

    Vector2 FindEntityMoveFuture(Vector2 entityCoord, Vector2 direction)
    {
        int latest = BoardSteps.Count -1;

        GameObject nodeGO = BoardSteps[latest][(int) entityCoord.y][(int) entityCoord.x];
        Node node = nodeGO.GetComponent<Node>();
        GameObject entity = node.entity;
        IMovementBehavior entityMovementBehavior = FindMovementBehavior(entity);
        Vector2 entityWantMove;

        if(entityMovementBehavior != null)
        {
            entityWantMove = entityMovementBehavior.GetMovementIntent(direction).direction + entityCoord;
        } else 
        {
            return entityCoord;
        }
       
        GameObject nodeToCheckGO = BoardSteps[latest][(int) entityWantMove.y][(int) entityWantMove.x];
        Node nodeToCheck = nodeToCheckGO.GetComponent<Node>();

        if(nodeToCheck != null && nodeToCheck.tile.GetComponent<Tile>().Standable)
        {
            return entityWantMove;
        } else {
            return entityCoord;
        }
    }

    IMovementBehavior FindMovementBehavior(GameObject entity)
    {
        foreach (Component component in entity.GetComponents<IMovementBehavior>())
        {
            if (component is IMovementBehavior)
            {
                return (IMovementBehavior)component;
            }
        }
        return null;
    }

    List<List<GameObject>> GetBlankBoardStep()
    {
        List<List<GameObject>> latestBoardStep = GetLatestBoardStep();
        List<List<GameObject>> blankBoardStep = new List<List<GameObject>>();
        for(int y = 0; y < latestBoardStep.Count; y++)
        {
            blankBoardStep.Add(new List<GameObject>());
            for (int x = 0; x < latestBoardStep[y].Count; x++)
            {
                GameObject blankNode = Instantiate(Node, latestBoardStep[y][x].transform.position, Quaternion.identity);
                blankNode.name = BoardSteps.Count + "Node(" + x + ", " + y + ")";

                blankNode.GetComponent<Node>().tile = latestBoardStep[y][x].GetComponent<Node>().tile;
                latestBoardStep[y][x].GetComponent<Node>().tile.transform.parent = blankNode.transform;

                blankNode.transform.parent = gameObject.transform;
                blankBoardStep[y].Add(blankNode);
            }
        }
        return blankBoardStep;
    }

    public GameOverStatus NextBoardState(Vector2 direction)
    {
        List<List<GameObject>> newBoardStep = GetBlankBoardStep();

        List<Vector2> AllEntityPastCoords = FindAllEntityCoords();

        List<Vector2> FutureEntityCoords = new List<Vector2>();

        foreach (Vector2 coord in AllEntityPastCoords)
        {
            FutureEntityCoords.Add(FindEntityMoveFuture(coord, direction));
        }

        List<Vector2> CyclicCheckCoords = CyclicCheck(AllEntityPastCoords, FutureEntityCoords);

        List<Vector2> CollidedEntityCoords = CollisionCheck(AllEntityPastCoords, CyclicCheckCoords);


        // for(int i = 0; i < CyclicCheckCoords.Count; i++)
        // {
        //     Debug.Log("Entity Past Coord: " + AllEntityPastCoords[i].x + ", " + AllEntityPastCoords[i].y);
        //     Debug.Log("Entity Cyclic Coord: " + CyclicCheckCoords[i].x + ", " + CyclicCheckCoords[i].y);
        // }
        // foreach (Vector2 coord in CollidedEntityCoords)
        // {
        //     Debug.Log("Entity Collision Coord: " + coord.x + ", " + coord.y);
        // }

        Debug.Log("We get here");
        Debug.Log("All entity pst crod" + AllEntityPastCoords.Count);

        List<List<GameObject>> latestBoardStep = GetLatestBoardStep();

        for(int i = 0; i < AllEntityPastCoords.Count; i++)
        {
            Debug.Log("Entity Past Coord: " + AllEntityPastCoords[i].x + ", " + AllEntityPastCoords[i].y);
            Debug.Log("Entity Cyclic Coord: " + CyclicCheckCoords[i].x + ", " + CyclicCheckCoords[i].y);

            GameObject entity = latestBoardStep[(int)AllEntityPastCoords[i].y][(int)AllEntityPastCoords[i].x].GetComponent<Node>().entity;
            Node newNode = newBoardStep[(int)CyclicCheckCoords[i].y][(int)CyclicCheckCoords[i].x].GetComponent<Node>();
            newNode.entity = entity;

            newNode.entity.transform.parent = newNode.transform;

            entity.GetComponent<Entity>().Move(newNode.entity.transform.position, 1, ()=>{});
        }

        DestroyBoardStepNodes(latestBoardStep);
        BoardSteps.Add(newBoardStep);
        // TODO Create a gameoverstate or return null without one
        return null;
    }

    void DestroyBoardStepNodes(List<List<GameObject>> boardStep)
    {
        for (int y = 0; y < boardStep.Count; y ++)
        {
            for (int x = 0; x < boardStep[y].Count; x ++)
            {
                Destroy(boardStep[y][x]);
            }
        }
    }

    List<Vector2> CollisionCheck (List<Vector2> PastCoords, List<Vector2> FutureCoords)
    {
        List<Vector2> FinalCoordList = new List<Vector2>();

        for (int i = 0; i < FutureCoords.Count; i++)
        {
            int matchedCoords = 0;
            foreach (Vector2 coord in FutureCoords)
            {
                if (coord == FutureCoords[i]) matchedCoords ++;
            }
            if(matchedCoords > 1)
            {
                FinalCoordList.Add(PastCoords[i]);
            }
        }
        return FinalCoordList;
    }

    List<Vector2> CyclicCheck (List<Vector2> PastCoords, List<Vector2> FutureCoords)
    {
        List<Vector2> FinalCoordList = new List<Vector2>();

        for(int i = 0; i < FutureCoords.Count; i++)
        {
            int cyclicCount = 0;
            int checkingIndex = i;
            List<int> cyclicList = new List<int>();
            while(true)
            {
                int matchedPastIndex = PastCoords.IndexOf(FutureCoords[checkingIndex]);
                if(matchedPastIndex != -1)
                {
                    cyclicCount++;
                    if(cyclicList.Contains(matchedPastIndex))
                    {
                        if (matchedPastIndex != i || cyclicCount == 2)
                        {
                            FinalCoordList.Add(PastCoords[i]);
                        } else 
                        {
                            FinalCoordList.Add(FutureCoords[i]);
                        }
                        break;
                    }
                    cyclicList.Add(matchedPastIndex);
                    checkingIndex = matchedPastIndex;
                } else {
                    FinalCoordList.Add(FutureCoords[i]);
                    break;
                }
            }
        }

        return FinalCoordList;
    }

    // void Undo()
    // {

    // }

    // bool WinCondition()
    // {

    // }
    void ParseLevel()
    {
        List<List<GameObject>> initialBoard = new List<List<GameObject>>();

        string levelFileContents = levelFile.text;
        string[] allLines = Regex.Split(levelFileContents, "\n|\r|\r|\n");
        List<string> lines = new List<string>();
        for (int i = allLines.Length - 1; i >= 0; i--)
        {
            if (allLines[i].Length == 0) continue;
            lines.Add(allLines[i]);
        }
        
        
        for(int y = 0; y < lines.Count; y++)
        {
            if(lines[y].Length == 0) continue;

            initialBoard.Add(new List<GameObject>());

            for (int x = 0; x < lines[y].Length; x++)
            {
                GameObject newNode = Instantiate(Node, new Vector3(TileWidth*x, TileHeight*y, 0), Quaternion.identity);
                newNode.name = "Node(" + x + "," + y + ")";
                GameObject newTile;
                GameObject newEntity;

                newNode.GetComponent<Node>().ascii = lines[y][x].ToString();

                switch (lines[y][x])
                {
                    // Void Node
                    case '#':
                        newTile = Instantiate(Void);
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
                        newNode.GetComponent<Node>().tile = newTile;

                        break;
                    // Empty Floor Node
                    case '.':
                        newTile = Instantiate(Floor);
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
                        newNode.GetComponent<Node>().tile = newTile;

                        break;
                    // Player Node
                    case 'P':
                        newTile = Instantiate(Floor);
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
                        newNode.GetComponent<Node>().tile = newTile;

                        newEntity = Instantiate(Player);
                        newEntity.transform.parent = newNode.transform;
                        newEntity.transform.localPosition = new Vector3();
                        newNode.GetComponent<Node>().entity = newEntity;

                        break;
                    // No Move Enemy Node
                    case 'u':
                        newTile = Instantiate(Floor);
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
                        newNode.GetComponent<Node>().tile = newTile;
                        
                        newEntity = Instantiate(NoMoveEnemy);
                        newEntity.transform.parent = newNode.transform;
                        newEntity.transform.localPosition = new Vector3();
                        newNode.GetComponent<Node>().entity = newEntity;

                        break;
                    // Standard Move Enemy Node
                    case 'o':
                        newTile = Instantiate(Floor);
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
                        newNode.GetComponent<Node>().tile = newTile;
                        
                        newEntity = Instantiate(StandardMoveEnemy);
                        newEntity.transform.parent = newNode.transform;
                        newEntity.transform.localPosition = new Vector3();
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
                        newTile.transform.localPosition = new Vector3();
                        newNode.GetComponent<Node>().tile = newTile;
                        
                        newEntity = Instantiate(EveryOtherStandardMoveEnemy);
                        newEntity.transform.parent = newNode.transform;
                        newEntity.transform.localPosition = new Vector3();
                        newNode.GetComponent<Node>().entity = newEntity;

                        break;
                    // Inverse Move Enemy Node
                    case 'i':
                        newTile = Instantiate(Floor);
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
                        newNode.GetComponent<Node>().tile = newTile;
                        
                        newEntity = Instantiate(InverseMoveEnemy);
                        newEntity.transform.parent = newNode.transform;
                        newEntity.transform.localPosition = new Vector3();
                        newNode.GetComponent<Node>().entity = newEntity;

                        break;
                    // Consume Floor Node
                    case '&':
                        newTile = Instantiate(ConsumeFloor);
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
                        newNode.GetComponent<Node>().tile = newTile;
                        break;
                    case '{':
                    case '}':
                    case '[':
                    case ']':
                        // For reflectors, not ready yet, just use floor for now
                        newTile = Instantiate(Floor);
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
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
