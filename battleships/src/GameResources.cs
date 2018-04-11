using SwinGameSDK;
using System.Collections.Generic;
public class GameResources
{

	private static void LoadFonts()
	{
		GameResources.NewFont("ArialLarge", "arial.ttf", 80);
		GameResources.NewFont("Courier", "cour.ttf", 14);
		GameResources.NewFont("CourierSmall", "cour.ttf", 8);
		GameResources.NewFont("Menu", "ffaccess.ttf", 8);
	}

	private static void LoadImages()
	{
		// Backgrounds
		GameResources.NewImage("Menu", "main_page.jpg");
		GameResources.NewImage("Discovery", "discover.jpg");
		GameResources.NewImage("Deploy", "deploy.jpg");
		// Deployment
		GameResources.NewImage("LeftRightButton", "deploy_dir_button_horiz.png");
		GameResources.NewImage("UpDownButton", "deploy_dir_button_vert.png");
		GameResources.NewImage("SelectedShip", "deploy_button_hl.png");
		GameResources.NewImage("PlayButton", "deploy_play_button.png");
		GameResources.NewImage("RandomButton", "deploy_randomize_button.png");
		// Ships
		int i;
		for (i = 1; (i <= 5); i++)
		{
			GameResources.NewImage(("ShipLR" + i), ("ship_deploy_horiz_"
							+ (i + ".png")));
			GameResources.NewImage(("ShipUD" + i), ("ship_deploy_vert_"
							+ (i + ".png")));
		}

		// Explosions
		GameResources.NewImage("Explosion", "explosion.png");
		GameResources.NewImage("Splash", "splash.png");
	}

	private static void LoadSounds()
	{
		GameResources.NewSound("Error", "error.wav");
		GameResources.NewSound("Hit", "hit.wav");
		GameResources.NewSound("Sink", "sink.wav");
		GameResources.NewSound("Siren", "siren.wav");
		GameResources.NewSound("Miss", "watershot.wav");
		GameResources.NewSound("Winner", "winner.wav");
		GameResources.NewSound("Lose", "lose.wav");
	}

	private static void LoadMusic()
	{
		GameResources.NewMusic("Background", "horrordrone.mp3");
	}

	// '' <summary>
	// '' Gets a Font Loaded in the Resources
	// '' </summary>
	// '' <param name="font">Name of Font</param>
	// '' <returns>The Font Loaded with this Name</returns>
	public static Font GameFont(string font)
	{
		return _Fonts(font);
	}

	// '' <summary>
	// '' Gets an Image loaded in the Resources
	// '' </summary>
	// '' <param name="image">Name of image</param>
	// '' <returns>The image loaded with this name</returns>
	public static Bitmap GameImage(string image)
	{
		return _Images(image);
	}

	// '' <summary>
	// '' Gets an sound loaded in the Resources
	// '' </summary>
	// '' <param name="sound">Name of sound</param>
	// '' <returns>The sound with this name</returns>
	public static SoundEffect GameSound(string sound)
	{
		return _Sounds(sound);
	}

	// '' <summary>
	// '' Gets the music loaded in the Resources
	// '' </summary>
	// '' <param name="music">Name of music</param>
	// '' <returns>The music with this name</returns>
	public static Music GameMusic(string music)
	{
		return _Music(music);
	}

	private Dictionary<string, Bitmap> _Images = new Dictionary<string, Bitmap>();

	private Dictionary<string, Font> _Fonts = new Dictionary<string, Font>();

	private Dictionary<string, SoundEffect> _Sounds = new Dictionary<string, SoundEffect>();

	private Dictionary<string, Music> _Music = new Dictionary<string, Music>();

	private Bitmap _Background;

	private Bitmap _Animation;

	private Bitmap _LoaderFull;

	private Bitmap _LoaderEmpty;

	private Font _LoadingFont;

	private SoundEffect _StartSound;

	// '' <summary>
	// '' The Resources Class stores all of the Games Media Resources, such as Images, Fonts
	// '' Sounds, Music.
	// '' </summary>
	public static void LoadResources()
	{
		int width;
		int height;
		width = SwinGame.ScreenWidth();
		height = SwinGame.ScreenHeight();
		SwinGame.ChangeScreenSize(800, 600);
		GameResources.ShowLoadingScreen();
		GameResources.ShowMessage("Loading fonts...", 0);
		GameResources.LoadFonts();
		SwinGame.Delay(100);
		GameResources.ShowMessage("Loading images...", 1);
		GameResources.LoadImages();
		SwinGame.Delay(100);
		GameResources.ShowMessage("Loading sounds...", 2);
		GameResources.LoadSounds();
		SwinGame.Delay(100);
		GameResources.ShowMessage("Loading music...", 3);
		GameResources.LoadMusic();
		SwinGame.Delay(100);
		SwinGame.Delay(100);
		GameResources.ShowMessage("Game loaded...", 5);
		SwinGame.Delay(100);
		GameResources.EndLoadingScreen(width, height);
	}

	private static void ShowLoadingScreen()
	{
		_Background = SwinGame.LoadBitmap(SwinGame.PathToResource("SplashBack.png", ResourceKind.BitmapResource));
		SwinGame.DrawBitmap(_Background, 0, 0);
		SwinGame.RefreshScreen();
		SwinGame.ProcessEvents();
		_Animation = SwinGame.LoadBitmap(SwinGame.PathToResource("SwinGameAni.jpg", ResourceKind.BitmapResource));
		_LoadingFont = SwinGame.LoadFont(SwinGame.PathToResource("arial.ttf", ResourceKind.FontResource), 12);
		_StartSound = Audio.LoadSoundEffect(SwinGame.PathToResource("SwinGameStart.ogg", ResourceKind.SoundResource));
		_LoaderFull = SwinGame.LoadBitmap(SwinGame.PathToResource("loader_full.png", ResourceKind.BitmapResource));
		_LoaderEmpty = SwinGame.LoadBitmap(SwinGame.PathToResource("loader_empty.png", ResourceKind.BitmapResource));
		GameResources.PlaySwinGameIntro();
	}

	private static void PlaySwinGameIntro()
	{
		const int ANI_CELL_COUNT = 11;
		Audio.PlaySoundEffect(_StartSound);
		SwinGame.Delay(200);
		int i;
		for (i = 0; (i
					<= (ANI_CELL_COUNT - 1)); i++)
		{
			SwinGame.DrawBitmap(_Background, 0, 0);
			SwinGame.Delay(20);
			SwinGame.RefreshScreen();
			SwinGame.ProcessEvents();
		}

		SwinGame.Delay(1500);
	}

	private static void ShowMessage(string message, int number)
	{
		const int BG_Y = 453;
		int TX = 310;
		int TY = 493;
		int TW = 200;
		int TH = 25;
		int STEPS = 5;
		int BG_X = 279;
		int fullW;
		Rectangle toDraw;
		fullW = (260 * number);
		STEPS;
		SwinGame.DrawBitmap(_LoaderEmpty, BG_X, BG_Y);
		SwinGame.DrawCell(_LoaderFull, 0, BG_X, BG_Y);
		SwinGame.DrawBitmapPart(_LoaderFull, 0, 0, fullW, 66, BG_X, BG_Y);
		toDraw.X = TX;
		toDraw.Y = TY;
		toDraw.Width = TW;
		toDraw.Height = TH;
		SwinGame.DrawTextLines(message, Color.White, Color.Transparent, _LoadingFont, FontAlignment.AlignCenter, toDraw);
		SwinGame.DrawTextLines(message, Color.White, Color.Transparent, _LoadingFont, FontAlignment.AlignCenter, TX, TY, TW, TH);
		SwinGame.RefreshScreen();
		SwinGame.ProcessEvents();
	}

	private static void EndLoadingScreen(int width, int height)
	{
		SwinGame.ProcessEvents();
		SwinGame.Delay(500);
		SwinGame.ClearScreen();
		SwinGame.RefreshScreen();
		SwinGame.FreeFont(_LoadingFont);
		SwinGame.FreeBitmap(_Background);
		SwinGame.FreeBitmap(_Animation);
		SwinGame.FreeBitmap(_LoaderEmpty);
		SwinGame.FreeBitmap(_LoaderFull);
		Audio.FreeSoundEffect(_StartSound);
		SwinGame.ChangeScreenSize(width, height);
	}

	private static void NewFont(string fontName, string filename, int size)
	{
		_Fonts.Add(fontName, SwinGame.LoadFont(SwinGame.PathToResource(filename, ResourceKind.FontResource), size));
	}

	private static void NewImage(string imageName, string filename)
	{
		_Images.Add(imageName, SwinGame.LoadBitmap(SwinGame.PathToResource(filename, ResourceKind.BitmapResource)));
	}

	private static void NewTransparentColorImage(string imageName, string fileName, Color transColor)
	{
		_Images.Add(imageName, SwinGame.LoadBitmap(SwinGame.PathToResource(fileName, ResourceKind.BitmapResource)));
	}

	private static void NewTransparentColourImage(string imageName, string fileName, Color transColor)
	{
		GameResources.NewTransparentColorImage(imageName, fileName, transColor);
	}

	private static void NewSound(string soundName, string filename)
	{
		_Sounds.Add(soundName, Audio.LoadSoundEffect(SwinGame.PathToResource(filename, ResourceKind.SoundResource)));
	}

	private static void NewMusic(string musicName, string filename)
	{
		_Music.Add(musicName, Audio.LoadMusic(SwinGame.PathToResource(filename, ResourceKind.SoundResource)));
	}

	private static void FreeFonts()
	{
		Font obj;
		foreach (obj F in _Fonts.Values)
		{
			SwinGame.FreeFont(obj);
		}

	}

	private static void FreeImages()
	{
		Bitmap obj;
		foreach (obj O in _Images.Values)
		{
			SwinGame.FreeBitmap(O);
		}

	}

	private static void FreeSounds()
	{
		SoundEffect obj;
		foreach (obj  SE in _Sounds.Values)
		{
			Audio.FreeSoundEffect(obj);
		}

	}

	private static void FreeMusic()
	{
		Music obj;
		foreach (obj M in _Music.Values)
		{
			Audio.FreeMusic(obj);
		}

	}

	public static void FreeResources()
	{
		GameResources.FreeFonts();
		GameResources.FreeImages();
		GameResources.FreeMusic();
		GameResources.FreeSounds();
		SwinGame.ProcessEvents();
	}
}