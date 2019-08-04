using System;
public class Scenes
{
	public const string DontDestroyOnLoad = "DontDestroyOnLoad";
	public const string TitleScreen = "TitleScreen";
	public const string Credits = "Credits";
	public const string BitdecaySplash = "BitdecaySplash";
	public const string GameJamSplash = "GameJamSplash";
	public const string Test = "Test";
	public const string TestKabobSpear = "TestKabobSpear";
	public const string Tutorial01 = "Tutorial01";
	public const string TestGameUI = "TestGameUI";
	public const string TestShootSkewer = "TestShootSkewer";
	public const string GameScene = "GameScene";
	public enum SceneEnum
	{
		TitleScreen = 0,
		Credits = 1,
		BitdecaySplash = 2,
		GameJamSplash = 3,
		Test = 4,
		TestKabobSpear = 5,
		Tutorial01 = 6,
		TestGameUI = 7,
		TestShootSkewer = 8,
		GameScene = 9,
	}
	public static string GetSceneNameFromEnum(SceneEnum sceneEnum)
	{
		switch (sceneEnum)
		{
			case SceneEnum.TitleScreen:
				return TitleScreen;
			case SceneEnum.Credits:
				return Credits;
			case SceneEnum.BitdecaySplash:
				return BitdecaySplash;
			case SceneEnum.GameJamSplash:
				return GameJamSplash;
			case SceneEnum.Test:
				return Test;
			case SceneEnum.TestKabobSpear:
				return TestKabobSpear;
			case SceneEnum.Tutorial01:
				return Tutorial01;
			case SceneEnum.TestGameUI:
				return TestGameUI;
			case SceneEnum.TestShootSkewer:
				return TestShootSkewer;
			case SceneEnum.GameScene:
				return GameScene;
			default:
				throw new Exception("Unable to resolve scene name for: " + sceneEnum);
		}
	}
}
