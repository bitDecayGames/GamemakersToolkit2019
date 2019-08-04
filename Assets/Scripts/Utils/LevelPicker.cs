using System;

[Obsolete("Don't use this, you should be setting level number dynamically with CurrentLevelNumber")]
public static class LevelPicker {
    private const int maxLevel = 10;

    public static String nextLevel(String currentLevel) {
        var curLevelNum = int.Parse(currentLevel.Substring(currentLevel.Length - 2, 2));
        if (curLevelNum + 1 > maxLevel) return "TitleScreen";
        return "Level" + (curLevelNum + 1 < 10 ? "0" : "") + (curLevelNum + 1);
    }
}