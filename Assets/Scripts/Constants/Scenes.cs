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
	public const string TestAnimations = "TestAnimations";
	public const string TestGameUI = "TestGameUI";
	public enum SceneEnum
	{
		TitleScreen = 98,
		Credits = 206,
		BitdecaySplash = 144,
		GameJamSplash = 253,
		Test = 160,
		TestKabobSpear = 122,
		TestAnimations = 179,
		TestGameUI = 184,
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
			case SceneEnum.TestAnimations:
				return TestAnimations;
			case SceneEnum.TestGameUI:
				return TestGameUI;
			default:
				throw new Exception("Unable to resolve scene name for: " + sceneEnum);
		}
	}
}
