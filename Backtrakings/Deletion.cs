using System;
using System.Collections.Generic;

namespace Backtracking.Backtrakings
{
	class Deletion : Backtracking
	{
		readonly List<int> state = new List<int>() { 2, 4, 6, 5, 1, 3 };
		int min;
		public Deletion()
		{
			Name = "Вычёркивание";
			Index = 9;
		}
		public override void Solve()
		{
			min = int.MaxValue;
			for (int i = state.Count - 2; i > 0; i--)
			{
				int t = state[i];
				int c = state[i + 1] + state[i - 1];
				state.RemoveAt(i);
				Solve(0, c, t);
				state.Insert(i, t);
			}
			Console.WriteLine(min);
		}
		private void FillIndent(int deep)
		{
			for (int i = 0; i < deep; i++)
				Console.Write("    ");
		}
		private void Solve(int deap, int count, int del)
		{
			FillIndent(deap);
			Console.WriteLine("{0,4}", -del);
			if (state.Count == 2)
			{
				min = Math.Min(min, count);
				return;
			}
			if (count > min) return;
			for (int i = state.Count - 2; i > 0; i--)
			{
				int t = state[i];
				int c = state[i + 1] + state[i - 1];
				state.RemoveAt(i);
				Solve(deap + 1, count + c, t);
				state.Insert(i, t);
			}
		}
	}
}
