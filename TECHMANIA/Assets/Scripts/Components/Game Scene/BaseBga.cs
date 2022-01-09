using System;
using System.Collections.Generic;

public static class BaseBga
{
	public static string CurrentDirectory = "";
	public enum PlaybackMode {
		Seeded,	// A-Z
		Random,
		Shuffle
	}
	private static Random rand = GetNewRandomSeed();
	private static PlaybackMode currentPlaybackMode = PlaybackMode.Seeded;
	private static string[] bgaPaths = new string[]{""};
	private static int currentIndex;
	private static bool initialized;
	private static bool firstBgaPlayed;

	private static Random GetNewRandomSeed() { return new Random((int) DateTime.Now.Ticks % 2000000); }
	// In addition, this is also anti-repetitive:
	// The first shuffled index is never the same as the last of the one before shuffling.
	// This prevents playing the same BGA twice between 2 shuffle cycles.
	private static string[] UniqueRandom(string[] list)
	{
		if (list.Length < 2)
		{
			return list;
		}
		Stack<string> stack = new Stack<string>();
		foreach (string entry in list)
		{
			stack.Push(entry);
		}
		string[] newList = new string[stack.Count];
		int length = newList.Length;
		while (stack.Count > 0)
		{
			int rng = 0;
			if (stack.Count < newList.Length)
			{
				rng = rand.Next(length);
			}
			else
			{
				rng = rand.Next(1, length);
			}
			while (null != newList[rng])
			{
				rng = (rng + 1) % length;
			}
			newList[rng] = stack.Pop();
		}
		return newList;
	}

	public static void Forward(string guid = "")
	{
		if (bgaPaths.Length > 0)
		{
			string name = bgaPaths[currentIndex];
			int length = bgaPaths.Length;
			switch (currentPlaybackMode)
			{
				case PlaybackMode.Seeded:
					int sum = 0;
					foreach (byte b in Guid.Parse(guid).ToByteArray())
					{
						sum += b;
					}
					rand = new Random(sum);
					currentIndex = rand.Next(length);
					break;
				case PlaybackMode.Random:
					currentIndex = rand.Next(length);
					break;
				case PlaybackMode.Shuffle:
					currentIndex = firstBgaPlayed ? (currentIndex + 1) % length : 0;
					if (firstBgaPlayed && currentIndex == 0)
					{
						bgaPaths = UniqueRandom(bgaPaths);
					}
					firstBgaPlayed = true;
					break;
			}
		}
	}

	public static string GetCurrentBgaPath()
	{
		return bgaPaths.Length > 0 ? bgaPaths[currentIndex] : "";
	}

	public static void SetMode(PlaybackMode mode)
	{
		rand = GetNewRandomSeed();
		switch (mode)
		{
			case PlaybackMode.Seeded:
				currentIndex = 0;
				break;
			case PlaybackMode.Random:
				currentIndex = rand.Next(bgaPaths.Length);
				break;
			case PlaybackMode.Shuffle:
				currentIndex = 0;
				bgaPaths = UniqueRandom(bgaPaths);
				break;
		}
		currentPlaybackMode = mode;
	}

	public static void ResetPaths(List<String> paths)
	{
		BaseBga.bgaPaths = paths.ToArray();
		BaseBga.SetMode(BaseBga.currentPlaybackMode);
	}

	public static void Initialize(List<String> videoPaths)
	{
		if (!initialized)
		{
			initialized = true;
			ResetPaths(videoPaths);
		}
	}

	public static bool IsBgaPoolEmpty() { return bgaPaths.Length == 0; }
	public static bool IsInitialized() { return initialized; }
}