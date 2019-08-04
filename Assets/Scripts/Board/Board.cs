using System;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;

public class Board : MonoBehaviour {
    public const float TIME_TO_ANIMATE = 0.15f;

    [Header("Be careful!!!")] [Tooltip("Don't modify this unless you are in a test level, or set it back to -1 after you are done at least")]
    public int LevelNumberOverride = -1;

    [Header("These are required to be on the same object")]
    public SkewerThrower skewerThrower;

    public BoardParser parser;

    private List<BoardStep> BoardSteps = new List<BoardStep>();

    public const float TileHeight = 0.8f; // 80x80 => .8x.8
    public const float TileWidth = TileHeight;

    private float xOffset;
    private float yOffset;

    void Start() {
        ParseLevel();
    }


    private BoardStep GetLatestBoardStep() {
        int latest = BoardSteps.Count - 1;
        return BoardSteps[latest];
    }

    private List<Vector2> FindEntityCoords(String entityName) {
        int latest = BoardSteps.Count - 1;
        List<Vector2> entityCoords = new List<Vector2>();

        List<Vector2> allEntities = FindAllEntityCoords();

        foreach (Vector2 coord in allEntities) {
            if (BoardSteps[latest][(int) coord.y][(int) coord.x].entity.Name == entityName) {
                entityCoords.Add(coord);
            }
        }

        return entityCoords;
    }

    private void DebugBoardSteps() {
        for (int y = 0; y < BoardSteps[0].Rows(); y++) {
            for (int x = 0; x < BoardSteps[0][y].Count; x++) {
                Debug.Log("POS X,Y: " + x + "," + y);
                Debug.Log("Tyle: " + BoardSteps[0][y][x].tile.name);
                var tempEnt = BoardSteps[0][y][x].entity;
                if (tempEnt != null) {
                    Debug.Log("Entyty: " + tempEnt);
                }
            }
        }
    }

    public GameOverStatus ThrowSkewer(Vector2 input) {
        // MW hack, but it kinda works...
        FindObjectOfType<PlayerAnimationCtrl>().AnimateShoot(input);
        return SkewerLogic.ShootSkewer(GetLatestBoardStep(), input, skewerThrower);
    }

    private List<Vector2> FindAllEntityCoords() {
        List<Vector2> entityCoords = new List<Vector2>();
        int latest = BoardSteps.Count - 1;

        for (int y = 0; y < BoardSteps[latest].Rows(); y++) {
            for (int x = 0; x < BoardSteps[latest][y].Count; x++) {
                var tempEntity = BoardSteps[latest][y][x].entity;
                if (tempEntity != null) {
                    //Debug.Log("temp entity: " + tempEntity.GetComponent<Entity>().Name);
                    entityCoords.Add(new Vector2(x, y));
                }
            }
        }

        return entityCoords;
    }

    public bool RequestedMoveValid(Vector2 direction) {
        int latest = BoardSteps.Count - 1;
        List<Vector2> playerCoord = FindEntityCoords("Player");

        if (playerCoord.Count != 1) {
            throw new StackOverflowException("THERE IS NO PLAYER!!! OH GOD NO!!!!!!!!!");
        }

        Vector2 nodeCoordToCheck = playerCoord[0] + direction;
        var nodeToCheck = BoardSteps[latest][(int) nodeCoordToCheck.y][(int) nodeCoordToCheck.x];

        if (nodeToCheck != null && nodeToCheck.tile.Standable) {
            return true;
        }

        return false;
    }

    private Vector2 FindEntityMoveFuture(Vector2 entityCoord, Vector2 direction) {
        int latest = BoardSteps.Count - 1;

        Node node = BoardSteps[latest][(int) entityCoord.y][(int) entityCoord.x];
        Entity entity = node.entity;
        IMovementBehavior entityMovementBehavior = FindMovementBehavior(entity);
        Vector2 entityWantMove;

        if (entityMovementBehavior != null) {
            entityWantMove = entityMovementBehavior.GetMovementIntent(direction).direction + entityCoord;
        } else {
            return entityCoord;
        }

        Node nodeToCheck = BoardSteps[latest][(int) entityWantMove.y][(int) entityWantMove.x];

        if (nodeToCheck != null && nodeToCheck.tile.Standable) {
            return entityWantMove;
        } else {
            return entityCoord;
        }
    }

    private IMovementBehavior FindMovementBehavior(Entity entity) {
        foreach (Component component in entity.GetComponents<IMovementBehavior>()) {
            if (component is IMovementBehavior) {
                return (IMovementBehavior) component;
            }
        }

        return null;
    }

    private BoardStep GetBlankBoardStep() {
        BoardStep latestBoardStep = GetLatestBoardStep();
        BoardStep blankBoardStep = new BoardStep();
        for (int y = 0; y < latestBoardStep.Rows(); y++) {
            blankBoardStep.AddRow();
            for (int x = 0; x < latestBoardStep[y].Count; x++) {
                Node blankNode = Instantiate(parser.Node, latestBoardStep[y][x].transform.position, Quaternion.identity);
                blankNode.name = BoardSteps.Count + "Node(" + x + ", " + y + ")";

                blankNode.tile = latestBoardStep[y][x].tile;
                blankNode.ascii = latestBoardStep[y][x].ascii;
                latestBoardStep[y][x].tile.transform.parent = blankNode.transform;

                blankNode.transform.parent = gameObject.transform;
                blankBoardStep[y].Add(blankNode);
            }
        }

        return blankBoardStep;
    }

    public GameOverStatus NextBoardState(Vector2 direction) {
        BoardStep newBoardStep = GetBlankBoardStep();

        List<Vector2> AllEntityPastCoords = FindAllEntityCoords();

        List<Vector2> FutureEntityCoords = new List<Vector2>();

        foreach (Vector2 coord in AllEntityPastCoords) {
            FutureEntityCoords.Add(FindEntityMoveFuture(coord, direction));
        }

        VectorSet CyclicCheckCoords = CyclicCheck(AllEntityPastCoords, FutureEntityCoords);

        VectorSet CollidedEntityCoords = CollisionCheck(AllEntityPastCoords, CyclicCheckCoords.vecs);

        // for(int i = 0; i < CyclicCheckCoords.Count; i++)
        // {
        //     Debug.Log("Entity Past Coord: " + AllEntityPastCoords[i].x + ", " + AllEntityPastCoords[i].y);
        //     Debug.Log("Entity Cyclic Coord: " + CyclicCheckCoords[i].x + ", " + CyclicCheckCoords[i].y);
        // }
        // foreach (Vector2 coord in CollidedEntityCoords)
        // {
        //     Debug.Log("Entity Collision Coord: " + coord.x + ", " + coord.y);
        // }

        //Debug.Log("We get here");
        //Debug.Log("All entity pst crod" + AllEntityPastCoords.Count);

        BoardStep latestBoardStep = GetLatestBoardStep();

        for (int i = 0; i < AllEntityPastCoords.Count; i++) {
            //Debug.Log("Entity Past Coord: " + AllEntityPastCoords[i].x + ", " + AllEntityPastCoords[i].y);
            //Debug.Log("Entity Cyclic Coord: " + CyclicCheckCoords.vecs[i].x + ", " + CyclicCheckCoords.vecs[i].y);

            var entity = latestBoardStep[(int) AllEntityPastCoords[i].y][(int) AllEntityPastCoords[i].x].entity;
            Node newNode = newBoardStep[(int) CyclicCheckCoords.vecs[i].y][(int) CyclicCheckCoords.vecs[i].x];
            newNode.entity = entity;

            newNode.entity.transform.parent = newNode.transform;
            entity.Move(newNode.transform.position, TIME_TO_ANIMATE, () => { });
        }

        VectorSet SpikeCoords = SpikeTileCheck(newBoardStep);


        DestroyBoardStepNodes(latestBoardStep);
        BoardSteps.Add(newBoardStep);

        // Create a gameoverstate or return null without one
        GameOverStatus finalStatus = CyclicCheckCoords.status;
        if (finalStatus == null) {
            finalStatus = CollidedEntityCoords.status;
        }

        if (finalStatus == null) {
            finalStatus = SpikeCoords.status;
        }

        return finalStatus;
    }

    private void DestroyBoardStepNodes(BoardStep boardStep) {
        for (int y = 0; y < boardStep.Rows(); y++) {
            for (int x = 0; x < boardStep[y].Count; x++) {
                Destroy(boardStep[y][x]);
            }
        }
    }

    private VectorSet CollisionCheck(List<Vector2> PastCoords, List<Vector2> FutureCoords) {
        VectorSet result = new VectorSet();
        List<Vector2> FinalCoordList = new List<Vector2>();

        for (int i = 0; i < FutureCoords.Count; i++) {
            int matchedCoords = 0;
            foreach (Vector2 coord in FutureCoords) {
                if (coord == FutureCoords[i]) matchedCoords++;
            }

            if (matchedCoords > 1) {
                result.status = new GameOverStatus(GameOverReason.SMOOSHED_INGREDIENTS);
                FinalCoordList.Add(PastCoords[i]);
            }
        }

        result.vecs = FinalCoordList;

        return result;
    }

    private VectorSet SpikeTileCheck(BoardStep boardState) {
        var result = new VectorSet();
        result.vecs = new List<Vector2>();
        for (int y = 0; y < boardState.Rows(); y++) {
            for (int x = 0; x < boardState[y].Count; x++) {
                var node = boardState[y][x];
                if (node.ascii == "&") {
                    if (node.entity != null) {
                        result.vecs.Add(new Vector2(x, y));
                        result.status = new GameOverStatus(GameOverReason.DROPPED_INGREDIENT_IN_WATER);
                    }
                }
            }
        }

        return result;
    }

    private VectorSet CyclicCheck(List<Vector2> PastCoords, List<Vector2> FutureCoords) {
        VectorSet result = new VectorSet();
        List<Vector2> FinalCoordList = new List<Vector2>();

        List<Vector2> playerCoord = FindEntityCoords("Player");

        for (int i = 0; i < FutureCoords.Count; i++) {
            int cyclicCount = 0;
            int checkingIndex = i;
            List<int> cyclicList = new List<int>();
            while (true) {
                int matchedPastIndex = PastCoords.IndexOf(FutureCoords[checkingIndex]);
                if (matchedPastIndex != -1) {
                    if (playerCoord[0] == PastCoords[i]) {
                        // Player is running into something baddo
                        result.status = new GameOverStatus(GameOverReason.TOUCHED_INGREDIENT);
                    }

                    cyclicCount++;
                    if (cyclicList.Contains(matchedPastIndex)) {
                        if (matchedPastIndex != i || cyclicCount == 2) {
                            FinalCoordList.Add(PastCoords[i]);
                        } else {
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

        result.vecs = FinalCoordList;
        return result;
    }

    private void ParseLevel() {
        if (LevelNumberOverride >= 0) CurrentLevelNumber.Instance.LevelNumber = LevelNumberOverride;
        string levelFileContents = LevelContent.levels[CurrentLevelNumber.Instance.LevelNumber];

        BoardStep initialBoard = parser.ParseBoard(levelFileContents, TileWidth, TileHeight);

        setCameraLocation(getTheMiddlestNode(initialBoard).transform);

        BoardSteps.Add(initialBoard);
    }

    private void setCameraLocation(Transform middlestNode) {
        var cam = FindObjectOfType<ProCamera2D>();
        var target = new CameraTarget();
        target.TargetTransform = middlestNode;
        cam.CameraTargets.Add(target);
    }

    private Tile getTheMiddlestNode(BoardStep nodes) {
        var middleY = nodes.Rows() / 2;
        var middleX = nodes.Columns() / 2;

        // MW: sorry if this breaks... we suck, and we are tired
        return nodes[middleY][middleX].tile;
    }

    private class VectorSet {
        public List<Vector2> vecs;
        public GameOverStatus status;

        public VectorSet() { }

        public VectorSet(List<Vector2> v, GameOverStatus s) {
            vecs = v;
            status = s;
        }
    }
}