using System;
using System.Collections.Generic;

public static class BaseBga
{
	public enum PlaybackMode {
		Seeded,	// A-Z
		Random,
		Shuffle
	}
	private static Random rand;
	private static int currentPlaybackMode = -1;
	private static int currentIndex = -1;
	private static string[] bgaPaths = new string[]{""};
	private static bool initialized;

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
		if (currentPlaybackMode > -1 && bgaPaths.Length > 0)
		{
			int length = bgaPaths.Length;
			switch ((PlaybackMode) currentPlaybackMode)
			{
				case PlaybackMode.Seeded:
					int hash = 0;
					foreach (byte b in Guid.Parse(guid).ToByteArray())
					{
						hash += b;
					}
					rand = new Random(hash + (int) System.DateTime.Now.Ticks);
					currentIndex = rand.Next(length);
					break;
				case PlaybackMode.Random:
					currentIndex = rand.Next(length);
					break;
				case PlaybackMode.Shuffle:
					int prevIndex = currentIndex;
					currentIndex = (currentIndex + 1) % length;
					if (prevIndex > -1 && currentIndex == 0)
					{
						bgaPaths = UniqueRandom(bgaPaths);
					}
					break;
			}
		}
	}

	public static string GetCurrentBgaPath()
	{
		return (bgaPaths.Length > 0 && currentIndex > -1) ? bgaPaths[currentIndex] : "";
	}

	public static void SetMode(PlaybackMode mode)
	{
		currentIndex = -1;
		if (mode == PlaybackMode.Random ||
			mode == PlaybackMode.Shuffle)
		{
			rand = new Random((int) DateTime.Now.Ticks % 2000000);
			bgaPaths = UniqueRandom(bgaPaths);
		}
		currentPlaybackMode = (int) mode;
	}

	public static void SetPaths(List<String> paths) { bgaPaths = paths.ToArray(); }

	public static void Reset(String path)
	{
		SetPaths(Paths.GetAllVideoFiles(path));
		SetMode((PlaybackMode) currentPlaybackMode);
	}

	public static bool IsBgaPoolEmpty() { return bgaPaths.Length == 0; }

	public static bool IsInitialized() { return currentPlaybackMode > -1; }
}