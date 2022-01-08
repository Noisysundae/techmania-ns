using System;
using System.Collections;

public static class BaseBga
{
	public static string CurrentDirectory = "";
	public enum PlaybackMode {
		Sorted,	// A-Z
		Random,
		Shuffle
	}
	private static Random rand = GetNewRandomSeed();
	public static PlaybackMode currentPlaybackMode = PlaybackMode.Shuffle;
	public static string[] bgaNames = new string[]{""};
	public static int currentIndex = 0;

	private static Random GetNewRandomSeed() { return new Random((int) DateTime.Now.Ticks % 2000000); }
	// In addition, this is also anti-repetitive:
	// The first shuffled index is never the same as the last of the one before shuffling.
	// This prevents playing the same BGA twice between 2 shuffle cycles.
	private static string[] UniqueRandom(string[] list)
	{
		Stack stack = new Stack();
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
			newList[rng] = (string) stack.Pop();
		}
		return newList;
	}

	public static string GetNextBgaName()
	{
		if (bgaNames.Length > 0)
		{
			string name = bgaNames[currentIndex];
			int length = bgaNames.Length;
			switch (currentPlaybackMode)
			{
				case PlaybackMode.Sorted:
					currentIndex = (currentIndex + 1) % length;
					break;
				case PlaybackMode.Random:
					currentIndex = (currentIndex + rand.Next(1, length)) % length;
					break;
				case PlaybackMode.Shuffle:
					currentIndex = (currentIndex + 1) % length;
					if (currentIndex == 0)
					{
						bgaNames = UniqueRandom(bgaNames);
					}
					break;
			}
			return name;
		}
		return "";
	}

	public static void SetMode(PlaybackMode mode)
	{
		rand = GetNewRandomSeed();
		switch (mode)
		{
			case PlaybackMode.Sorted:
				Array.Sort(bgaNames);
				currentIndex = 0;
				break;
			case PlaybackMode.Random:
				currentIndex = rand.Next(bgaNames.Length);
				break;
			case PlaybackMode.Shuffle:
				bgaNames = UniqueRandom(bgaNames);
				currentIndex = 0;
				break;
		}
		currentPlaybackMode = mode;
	}
}