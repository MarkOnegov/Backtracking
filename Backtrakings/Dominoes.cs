using System;
using System.Collections.Generic;

namespace Backtracking.Backtrakings
{
	class Dominoes : Backtracking
	{
		public List<byte[]> dominoes = new List<byte[]>()
		{
			new byte[]{1, 2},
			new byte[]{2, 5},
			new byte[]{3, 6},
			new byte[]{5, 1},
			new byte[]{6, 2},
		};
		public List<bool> available = new List<bool>();
		int max;
		public Dominoes()
		{
			Index = 3;
			Name = "Домино";
		}
		public override void Solve()
		{
			dominoes.ForEach(_ => available.Add(true));
			max = 0;
			try
			{
				for (int i = dominoes.Count - 1; i >= 0; i--)
				{
					Solve(i, false, 0);
					Solve(i, true, 0);
				}
			}
			catch (Exception e)
			{
				if (!e.Message.Equals("Done!")) throw e;
			}
			Console.WriteLine(max);
		}
		private void FillIndent(int deap)
		{
			for (int i = 0; i < deap; i++)
				Console.Write("      ");
		}
		private void Solve(int index, bool revers, int deap)
		{
			byte[] d = dominoes[index];
			available[index] = false;
			byte k = d[0], t = d[1];
			if (revers) { k = d[1]; t = d[0]; }
			FillIndent(deap);
			Console.WriteLine("[{0}|{1}]", k, t);
			max = Math.Max(deap + 1, max);
			if (max == dominoes.Count) throw new Exception("Done!");
			for (int i = dominoes.Count - 1; i >= 0; i--)
			{
				if (!available[i]) continue;
				byte t0 = dominoes[i][0];
				if (t0 == t)
					Solve(i, false, deap + 1);
				t0 = dominoes[i][1];
				if (t0 == t && dominoes[i][0] != dominoes[i][1])
					Solve(i, true, deap + 1);
			}
			available[index] = true;
		}
	}
}
