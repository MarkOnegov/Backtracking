using System;
using System.Collections.Generic;
using System.Linq;

namespace Backtracking.Backtrakings
{
	class ColoringPages : Backtracking
	{
		List<byte> colors;
		List<List<byte>> solutions;
		bool findeFirst;
		int mode;
		readonly byte[,] matrix = new byte[8, 8]
		{
			{0,1,0,1,1,0,0,0 },
			{1,0,1,1,0,0,0,0 },
			{0,1,0,1,0,0,1,0 },
			{1,1,1,0,0,1,0,0 },
			{1,0,0,0,0,1,0,1 },
			{0,0,0,1,1,0,1,1 },
			{0,0,1,0,0,1,0,1 },
			{0,0,0,0,1,1,1,0 }
		};
		int colorCount;
		public ColoringPages()
		{
			Name = "Раскраски";
			Index = 12;
		}
		public override void Solve()
		{
			colors = new List<byte>();
			solutions = new List<List<byte>>();
			PrintColoringPage();
			if (!GetMode()) return;
			Console.Clear();
			PrintColoringPage();
			colorCount = GetInt("Введите кол-во цветов: ", 1);
			Console.Clear();
			PrintColoringPage();
			try
			{
				GetAvailableColors(0).ToList().ForEach(c => Solve(0, c));
			}
			catch (Exception e) { if (e.Message != "Done!") throw e; }
			switch (mode)
			{
				case 1:
					if (solutions.Count == 0)
						Console.WriteLine("Не найдено ни одной раскраски");
					else
						PrintSolution(solutions[0]);
					break;
				case 2:
					Console.WriteLine("Кол-во раскрасок: {0}", solutions.Count);
					break;
				case 3:
					Console.WriteLine(solutions.Count == 0 ? "Раскраска не возможна" : "Раскраска возможна");
					break;
			}
		}
		private int GetInt(string q, int min = int.MinValue, int max = int.MaxValue)
		{
			while (true)
			{
				try
				{
					Console.Write(q);
					int x = Convert.ToInt32(Console.ReadLine());
					if (x < max && x >= min) return x;
					throw new Exception();
				}
				catch (Exception) { Console.WriteLine("Ошибка ввода"); }
			}
		}
		private void PrintColoringPage() => Console.WriteLine(
				"  ____\n" +
				" /\\__/\\\n" +
				"/_/__\\_\\\n" +
				"\\ \\__/ /\n" +
				" \\/__\\/"
				);
		private bool GetMode()
		{
			Console.WriteLine("1)\tПервая раскраска");
			Console.WriteLine("2)\tКоличество раскрасок");
			Console.WriteLine("3)\tВозможна ли раскраска");
			while (true)
			{
				try
				{
					int t = Convert.ToInt32(Console.ReadLine());
					mode = t;
					switch (t)
					{
						case 0:
							return false;
						case 1:
							findeFirst = true;
							return true;
						case 2:
							findeFirst = false;
							return true;
						case 3:
							findeFirst = true;
							return true;
					}
					Console.WriteLine("Ошибка ввода, попробуйте ещё раз");
				}
				catch { Console.WriteLine("Ошибка ввода, попробуйте ещё раз"); }
			}
		}
		private void PrintSolution(List<byte> colors)
		{
			colors.ForEach(c => Console.Write("{0} ", c));
			Console.WriteLine();
		}
		private void Solve(int deep, byte color)
		{
			colors.Add(color);
			if (colors.Count == matrix.GetLength(0))
			{
				var copy = new List<byte>();
				copy.AddRange(colors);
				solutions.Add(copy);
				if (findeFirst) throw new Exception("Done!");
			}
			else
				GetAvailableColors(deep + 1).ToList()
					.ForEach(c => Solve(deep + 1, c));
			colors.RemoveAt(colors.Count - 1);
		}
		private byte[] GetAvailableColors(int n)
		{
			List<byte> available = new List<byte>();
			for (byte i = 0; i < colorCount; i++)
				available.Add(i);
			for (int i = 0; i < n; i++)
				if (matrix[n, i] > 0)
					available.Remove(colors[i]);
			return available.ToArray();
		}
	}
}
