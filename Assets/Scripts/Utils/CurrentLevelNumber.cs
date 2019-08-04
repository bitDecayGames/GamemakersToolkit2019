using UnityEngine;

public class CurrentLevelNumber : MonoBehaviour {
    public int HighestLevel { get; private set; }

    private int _levelNumber = 0;

    public int LevelNumber {
        get => _levelNumber;
        set {
            if (value > HighestLevel) HighestLevel = value;
            _levelNumber = value;
        }
    }

    private static CurrentLevelNumber instance;

    public static CurrentLevelNumber Instance {
        get {
            if (instance == null) {
                GameObject gameObject = new GameObject();
                gameObject.name = "CurrentLevelNumber";
                instance = gameObject.AddComponent<CurrentLevelNumber>();
                DontDestroyOnLoad(gameObject);
            }

            return instance;
        }
    }

    private void Start() {
        if (instance == null) instance = this;
    }
}