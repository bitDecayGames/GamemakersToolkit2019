using System;
public class Scenes
{
	public const string DontDestroyOnLoad = "DontDestroyOnLoad";
	public const string BitdecaySplash = "BitdecaySplash";
	public const string TitleScreen = "TitleScreen";
	public const string Credits = "Credits";
	public const string Tutorial01 = "Tutorial01";
	public const string GameScene = "GameScene";
	public enum SceneEnum
	{
		BitdecaySplash = 0,
		TitleScreen = 1,
		Credits = 2,
		Tutorial01 = 3,
		GameScene = 4,
	}
	public static string GetSceneNameFromEnum(SceneEnum sceneEnum)
	{
		switch (sceneEnum)
		{
			case SceneEnum.BitdecaySplash:
				return BitdecaySplash;
			case SceneEnum.TitleScreen:
				return TitleScreen;
			case SceneEnum.Credits:
				return Credits;
			case SceneEnum.Tutorial01:
				return Tutorial01;
			case SceneEnum.GameScene:
				return GameScene;
			default:
				throw new Exception("Unable to resolve scene name for: " + sceneEnum);
		}
	}
}
