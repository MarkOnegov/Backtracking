using System;
using System.Collections.Generic;
using System.Linq;

namespace Backtracking.Backtrakings
{
	class Flags : Backtracking
	{
		enum Color
		{
			White = ConsoleColor.White,
			Blue = ConsoleColor.Blue,
			Red = ConsoleColor.Red
		}

		readonly List<Color> flag = new List<Color>();
		int count;
		public Flags()
		{
			Name = "Флаги";
			Index = 4;
		}
		public override void Solve()
		{
			count = 0;
			Solve(0);
			Console.WriteLine(count);
		}
		private void Solve(int deap)
		{
			if (deap == 4)
			{
				count++;
				return;
			}
			GetNextColors().ForEach(c =>
			{
				flag.Add(c);
				WriteFlag();
				Solve(deap + 1);
				flag.RemoveAt(flag.Count - 1);
			});
		}
		private void WriteFlag()
		{
			Console.WriteLine();
			flag.ForEach(c =>
			{
				Console.BackgroundColor = (ConsoleColor)c;
				Console.Write(" ");
			});
			Console.BackgroundColor = ConsoleColor.Black;
			Console.WriteLine();
		}
		private List<Color> GetNextColors()
		{

			if (flag.Count == 0) return new List<Color>() { Color.Red, Color.White };
			Color last = flag.Last();
			if (last == Color.Blue)
			{

				int t = flag.Count - 2;
				if (flag[t] == Color.Red) return new List<Color>() { Color.White };
				return new List<Color>() { Color.Red };
			}
			if (last == Color.Red)
				if (flag.Count != 3)
					return new List<Color>() { Color.Blue, Color.White };
				else
					return new List<Color>() { Color.White };
			if (flag.Count != 3)
				return new List<Color>() { Color.Blue, Color.Red };
			else
				return new List<Color>() { Color.Red };
		}
	}
}
