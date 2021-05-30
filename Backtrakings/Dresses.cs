using System;
using System.Collections.Generic;
using System.Linq;

namespace Backtracking.Backtrakings
{
	class Dresses : Backtracking
	{
		List<byte> colors;
		List<List<byte>> solutions;
		readonly byte[,] matrix = new byte[4, 4]
		{
			{ 0,1,1,1},
			{ 1,0,0,1},
			{ 1,0,0,1},
			{ 1,1,1,0}
		};
		public Dresses()
		{
			Name = "Платья";
			Index = 11;
		}
		public override void Solve()
		{
			colors = new List<byte>();
			solutions = new List<List<byte>>();
			GetAvailableColors(0).ToList().ForEach(c => Solve(0, c));
			solutions.ForEach(PrintSolution);
		}
		private void PrintSolution(List<byte> colors)
		{
			colors.ForEach(c =>
			{
				switch (c)
				{
					case 0:
						Console.BackgroundColor = ConsoleColor.Red;
						break;
					case 1:
						Console.BackgroundColor = ConsoleColor.Yellow;
						break;
					case 2:
						Console.BackgroundColor = ConsoleColor.Green;
						break;
				}
				Console.Write(" ");
			});
			Console.BackgroundColor = ConsoleColor.Black;
			Console.WriteLine();
		}
		private void FillIndent(int deep)
		{
			for (int i = 0; i < deep; i++)
				Console.Write("  ");
		}
		private void Solve(int deep, byte color)
		{
			FillIndent(deep);
			Console.WriteLine("{0,2}", color);
			colors.Add(color);
			if (colors.Count == 4)
			{
				var copy = new List<byte>();
				copy.AddRange(colors);
				solutions.Add(copy);
			}
			else
				GetAvailableColors(deep + 1).ToList()
					.ForEach(c => Solve(deep + 1, c));
			colors.RemoveAt(colors.Count - 1);
		}
		private byte[] GetAvailableColors(int n)
		{
			List<byte> available = new List<byte>() { 0, 1, 2 };
			for (int i = 0; i < n; i++)
				if (matrix[n, i] > 0)
					available.Remove(colors[i]);
			return available.ToArray();
		}
	}
}
