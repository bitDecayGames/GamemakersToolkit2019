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
	public const string Level10 = "Level10";
	public const string TestGameUI = "TestGameUI";
	public const string TestShootSkewer = "TestShootSkewer";
	public const string Level11 = "Level11";
	public enum SceneEnum
	{
		TitleScreen = 0,
		Credits = 1,
		BitdecaySplash = 2,
		GameJamSplash = 3,
		Test = 4,
		TestKabobSpear = 5,
		Level01 = 6,
		Level02 = 7,
		Level03 = 8,
		Level04 = 9,
		Level05 = 10,
		Level06 = 11,
		Level07 = 12,
		Level08 = 13,
		Level09 = 14,
		Level10 = 15,
		TestGameUI = 16,
		TestShootSkewer = 17,
		Level11 = 18,
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
			case SceneEnum.Level10:
				return Level10;
			case SceneEnum.TestGameUI:
				return TestGameUI;
			case SceneEnum.TestShootSkewer:
				return TestShootSkewer;
			case SceneEnum.Level11:
				return Level11;
			default:
				throw new Exception("Unable to resolve scene name for: " + sceneEnum);
		}
	}
}
