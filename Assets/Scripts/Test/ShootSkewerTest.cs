using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ShootSkewerTest : MonoBehaviour {
    public Board board;
    public SkewerThrower thrower;


    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            var result = SkewerLogic.ShootSkewer(ParseLevel(@"

##########
###{#u#}##
###[#u##}#
#P#u###]##
#[######]#
##########




"), Directions.East, thrower);
            Debug.Log("Result: " + result.reason);
        }
    }


    public List<List<Node>> ParseLevel(String levelFileContents) {
        List<List<Node>> initialBoard = new List<List<Node>>();

        string[] allLines = Regex.Split(levelFileContents, "\n|\r|\r|\n");
        List<string> lines = new List<string>();
        for (int i = allLines.Length - 1; i >= 0; i--) {
            if (allLines[i].Length == 0) continue;
            lines.Add(allLines[i]);
        }

        for (int y = 0; y < lines.Count; y++) {
            if (lines[y].Length == 0) continue;

            initialBoard.Add(new List<Node>());

            for (int x = 0; x < lines[y].Length; x++) {
                Node newNode = Instantiate(board.Node.GetComponent<Node>(), new Vector3(Board.TileWidth * x, Board.TileHeight * y, 0), Quaternion.identity);
                GameObject newTile;
                GameObject newEntity;

                newNode.ascii = lines[y][x].ToString();
                
                switch (lines[y][x]) {
                    // Void Node
                    case '#':
                        newTile = Instantiate(board.Void);
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
                        newNode.tile = newTile;

                        break;
                    // Empty Floor Node
                    case '.':
                        newTile = Instantiate(board.Floor);
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
                        newNode.GetComponent<Node>().tile = newTile;

                        break;
                    // Player Node
                    case 'P':
                        newTile = Instantiate(board.Floor);
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
                        newNode.GetComponent<Node>().tile = newTile;

                        newEntity = Instantiate(board.Player);
                        newEntity.transform.parent = newNode.transform;
                        newEntity.transform.localPosition = new Vector3();
                        newNode.GetComponent<Node>().entity = newEntity;

                        break;
                    // No Move Enemy Node
                    case 'u':
                        newTile = Instantiate(board.Floor);
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
                        newNode.GetComponent<Node>().tile = newTile;

                        newEntity = Instantiate(board.NoMoveEnemy);
                        newEntity.transform.parent = newNode.transform;
                        newEntity.transform.localPosition = new Vector3();
                        newNode.GetComponent<Node>().entity = newEntity;

                        break;
                    // Standard Move Enemy Node
                    case 'o':
                        newTile = Instantiate(board.Floor);
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
                        newNode.GetComponent<Node>().tile = newTile;

                        newEntity = Instantiate(board.StandardMoveEnemy);
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
                        newTile = Instantiate(board.Floor);
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
                        newNode.GetComponent<Node>().tile = newTile;

                        newEntity = Instantiate(board.EveryOtherStandardMoveEnemy);
                        newEntity.transform.parent = newNode.transform;
                        newEntity.transform.localPosition = new Vector3();
                        newNode.GetComponent<Node>().entity = newEntity;

                        break;
                    // Inverse Move Enemy Node
                    case 'i':
                        newTile = Instantiate(board.Floor);
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
                        newNode.GetComponent<Node>().tile = newTile;

                        newEntity = Instantiate(board.InverseMoveEnemy);
                        newEntity.transform.parent = newNode.transform;
                        newEntity.transform.localPosition = new Vector3();
                        newNode.GetComponent<Node>().entity = newEntity;

                        break;
                    // Consume Floor Node
                    case '&':
                        newTile = Instantiate(board.ConsumeFloor);
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
                        newNode.GetComponent<Node>().tile = newTile;
                        break;
                    case '{':
                    case '}':
                    case '[':
                    case ']':
                        // For reflectors, not ready yet, just use floor for now
                        newTile = Instantiate(board.Floor);
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

        return initialBoard;
    }
}