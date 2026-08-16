using Godot;
using System;

public static class Score
{
	public static uint CurrentScore { get; private set; } = 0;
	public static uint Highscore { get; private set; } = 0;

	public static void IncrementScore()
	{
		CurrentScore++;
	}

    public static void ResetScore()
	{
		CurrentScore = 0;
	}
	public static void SetHighscore()
	{
		if (CurrentScore > Highscore)
		{
			Highscore = CurrentScore;
		}
	}
}
