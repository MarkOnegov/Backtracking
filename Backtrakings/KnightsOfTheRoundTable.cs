using System;
using System.Collections.Generic;
using System.Linq;

namespace Backtracking.Backtrakings
{
	class KnightsOfTheRoundTable : Backtracking
	{
		List<int> state;
		readonly byte[,] matrix = new byte[5, 5]
		{
			{ 0,1,0,1,0},
			{ 1,0,1,1,1},
			{ 0,1,0,0,1},
			{ 1,1,0,0,1},
			{ 0,1,1,1,0}
		};
		public KnightsOfTheRoundTable()
		{
			Name = "Рыцари Круглого Стола";
			Index = 10;
		}
		private void FillIndent(int deep)
		{
			for (int i = 0; i < deep; i++)
				Console.Write("    ");
		}
		public override void Solve()
		{
			state = new List<int>();
			try
			{
				for (int i = 0; i < 5; i++)
					Solve(0, i);
			}
			catch (Exception e) { if (e.Message != "Done!") throw e; }
			for (int i = 0; i < state.Count; i++)
				Console.Write("{0,4}", state[i]);
			Console.WriteLine();
		}
		private void Solve(int deep, int n)
		{
			FillIndent(deep);
			Console.WriteLine("{0,4}", n);
			state.Add(n);
			if (state.Count == 5)
				if (matrix[state[0], state[4]] > 0)
					throw new Exception("Done!");
				else
					return;
			for (int i = 0; i < 5; i++)
				if (matrix[n, i] > 0 && !state.Any(t => i == t))
					Solve(deep + 1, i);
			state.Remove(n);
		}
	}
}
