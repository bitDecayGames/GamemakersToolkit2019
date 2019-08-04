using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class BoardParser : MonoBehaviour {
    public Node Node;

    [Header("Entities")] public Entity Player;
    public Entity NoMoveEnemy;
    public Entity StandardMoveEnemy;
    public Entity EveryOtherStandardMoveEnemy;
    public Entity InverseMoveEnemy;

    [Header("Floor Tile")] public Tile Floor;
    [Header("Consume Tile")] public Tile ConsumeFloor;
    public List<Sprite> ConsumeFloorSprites;
    [Header("Void Tile")] public Tile Void;
    public List<Sprite> VoidSprites;
    [Header("Wall Tile")] public Tile Wall;
    public List<Sprite> WallSprites;

    [Header("Pipes")] public Tile TopLeftPipe;
    public Tile TopRightPipe;
    public Tile BottomRightPipe;
    public Tile BottomLeftPipe;

    public BoardStep ParseBoard(string levelFileContents, float tileWidth, float tileHeight) {
        BoardStep initialBoard = new BoardStep();
        string[] allLines = Regex.Split(levelFileContents, "\n|\r|\r|\n");
        List<string> lines = new List<string>();
        for (int i = allLines.Length - 1; i >= 0; i--) {
            if (allLines[i].Length == 0) continue;
            lines.Add(allLines[i]);
        }

        for (int y = 0; y < lines.Count; y++) {
            if (lines[y].Length == 0) continue;

            initialBoard.AddRow();

            for (int x = 0; x < lines[y].Length; x++) {
                Node newNode = Instantiate(Node, new Vector3(tileWidth * x, tileHeight * y, 0), Quaternion.identity);
                newNode.name = "Node(" + x + "," + y + ")";
                Tile newTile;
                Entity newEntity;

                newNode.ascii = lines[y][x].ToString();

                switch (lines[y][x]) {
                    // Void Node
                    case '#':
                        newTile = Instantiate(Void);
                        newTile.SetSprite(edgePicker(VoidSprites, lines, y, x));
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
                        newNode.tile = newTile;

                        break;
                    // Empty Floor Node
                    case '.':
                        newTile = Instantiate(Floor);
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
                        newNode.tile = newTile;

                        break;
                    // Player Node
                    case 'P':
                        newTile = Instantiate(Floor);
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
                        newNode.tile = newTile;

                        newEntity = Instantiate(Player);
                        newEntity.transform.parent = newNode.transform;
                        newEntity.transform.localPosition = new Vector3();
                        newNode.entity = newEntity;

                        break;
                    // No Move Enemy Node
                    case 'u':
                        newTile = Instantiate(Floor);
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
                        newNode.tile = newTile;

                        newEntity = Instantiate(NoMoveEnemy);
                        newEntity.transform.parent = newNode.transform;
                        newEntity.transform.localPosition = new Vector3();
                        newNode.entity = newEntity;

                        break;
                    // Standard Move Enemy Node
                    case 'o':
                        newTile = Instantiate(Floor);
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
                        newNode.tile = newTile;

                        newEntity = Instantiate(StandardMoveEnemy);
                        newEntity.transform.parent = newNode.transform;
                        newEntity.transform.localPosition = new Vector3();
                        newNode.entity = newEntity;

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
                        newNode.tile = newTile;

                        newEntity = Instantiate(EveryOtherStandardMoveEnemy);
                        newEntity.transform.parent = newNode.transform;
                        newEntity.transform.localPosition = new Vector3();
                        newNode.entity = newEntity;

                        break;
                    // Inverse Move Enemy Node
                    case 'i':
                        newTile = Instantiate(Floor);
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
                        newNode.tile = newTile;

                        newEntity = Instantiate(InverseMoveEnemy);
                        newEntity.transform.parent = newNode.transform;
                        newEntity.transform.localPosition = new Vector3();
                        newNode.entity = newEntity;

                        break;
                    // Consume Floor Node
                    case '&':
                        newTile = Instantiate(ConsumeFloor);
                        newTile.SetSprite(edgePicker(ConsumeFloorSprites, lines, y, x));
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
                        newNode.tile = newTile;
                        break;
                    // Wall node
                    case 'H':
                        newTile = Instantiate(Wall);
                        newTile.SetSprite(edgePicker(WallSprites, lines, y, x));
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
                        newNode.tile = newTile;
                        break;
                    case '{':
                        // For reflectors, not ready yet, just use floor for now
                        newTile = Instantiate(TopLeftPipe);
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
                        newNode.tile = newTile;

                        break;
                    case '}':
                        // For reflectors, not ready yet, just use floor for now
                        newTile = Instantiate(TopRightPipe);
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
                        newNode.tile = newTile;

                        break;
                    case '[':
                        // For reflectors, not ready yet, just use floor for now
                        newTile = Instantiate(BottomLeftPipe);
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
                        newNode.tile = newTile;

                        break;
                    case ']':
                        // For reflectors, not ready yet, just use floor for now
                        newTile = Instantiate(BottomRightPipe);
                        newTile.transform.parent = newNode.transform;
                        newTile.transform.localPosition = new Vector3();
                        newNode.tile = newTile;

                        break;
                    default:
                        throw new Exception("YOU DONE BROKE THE GAME WITH YOUR DUMB CSV FILER!");
                }

                newNode.transform.parent = gameObject.transform;
                initialBoard[y].Add(newNode);
            }
        }

        return initialBoard;
    }

    private Sprite edgePicker(List<Sprite> sprites, List<string> lines, int row, int column) {
        int mask = 0;
        char ascii = lines[row][column];
        if (row - 1 >= 0 && lines[row - 1][column] == ascii) mask |= Edge.BOTTOM;
        if (row + 1 < lines.Count && lines[row + 1][column] == ascii) mask |= Edge.TOP;
        if (column - 1 >= 0 && lines[row][column - 1] == ascii) mask |= Edge.LEFT;
        if (column + 1 < lines[row].Length && lines[row][column + 1] == ascii) mask |= Edge.RIGHT;
        return sprites[mask];
    }

    private static class Edge {
        public const int TOP = 1;
        public const int RIGHT = 2;
        public const int BOTTOM = 4;
        public const int LEFT = 8;
    }
}