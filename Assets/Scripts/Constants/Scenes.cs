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
	public const string Level01 = "Level01";
	public const string Level02 = "Level02";
	public const string Level03 = "Level03";
	public const string Level04 = "Level04";
	public const string Level05 = "Level05";
	public const string Level06 = "Level06";
	public const string Level07 = "Level07";
	public const string Level08 = "Level08";
	public const string Level09 = "Level09";
	public enum SceneEnum
	{
		TitleScreen = 98,
		Credits = 206,
		BitdecaySplash = 144,
		GameJamSplash = 253,
		Test = 160,
		TestKabobSpear = 122,
		Level01 = 89,
		Level02 = 90,
		Level03 = 91,
		Level04 = 92,
		Level05 = 93,
		Level06 = 94,
		Level07 = 95,
		Level08 = 96,
		Level09 = 97,
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
			case SceneEnum.Level01:
				return Level01;
			case SceneEnum.Level02:
				return Level02;
			case SceneEnum.Level03:
				return Level03;
			case SceneEnum.Level04:
				return Level04;
			case SceneEnum.Level05:
				return Level05;
			case SceneEnum.Level06:
				return Level06;
			case SceneEnum.Level07:
				return Level07;
			case SceneEnum.Level08:
				return Level08;
			case SceneEnum.Level09:
				return Level09;
			default:
				throw new Exception("Unable to resolve scene name for: " + sceneEnum);
		}
	}
}
