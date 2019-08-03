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
	public const string FisherTest = "FisherTest";
	public enum SceneEnum
	{
		TitleScreen = 98,
		Credits = 206,
		BitdecaySplash = 144,
		GameJamSplash = 253,
		Test = 160,
		TestKabobSpear = 122,
		FisherTest = 1,
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
			case SceneEnum.FisherTest:
				return FisherTest;
			default:
				throw new Exception("Unable to resolve scene name for: " + sceneEnum);
		}
	}
}
