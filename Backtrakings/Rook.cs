using System;
using System.Collections.Generic;
using System.Linq;

namespace Backtracking.Backtrakings
{
	class Rook : Backtracking
	{
		readonly bool[] freeCol = new bool[] { true, true, true };
		readonly bool[] freeRow = new bool[] { true, true, true };
		bool[,] field = new bool[3, 3];
		readonly List<bool[,]> solutions = new List<bool[,]>();
		public Rook()
		{
			Name = "Ладьи";
			Index = 1;
		}
		private void FillIndent(int deap)
		{
			for (int i = 0; i < deap; i++)
				Console.Write("       ");
		}

		public override void Solve()
		{
			for (int i = 0; i < 3; i++)
				for (int j = 0; j < 3; j++)
					Solve(i, j, 0);
			solutions.ForEach(WriteSollution);
		}
		private void WriteSollution(bool[,] sol)
		{
			Console.WriteLine();
			for (int i = 0; i < 3; i++)
			{
				if (i != 0)
					Console.WriteLine("-----------");
				Console.WriteLine("   |   |   ");
				for (int j = 0; j < 3; j++)
				{
					if (j != 0)
						Console.Write("|");
					Console.Write(sol[i, j] ? " # " : "   ");
				}
				Console.WriteLine();
				Console.WriteLine("   |   |   ");
			}
		}
		private bool CompareSolutions(bool[,] a, bool[,] b)
		{
			for (int i = 0; i < 3; i++)
				for (int j = 0; j < 3; j++)
					if (a[i, j] != b[i, j]) return false;
			return true;
		}
		private void Solve(int i, int j, int deap)
		{
			if (deap == 3)
			{
				if (!solutions.Any(s => CompareSolutions(s, field)))
				{
					solutions.Add(field);
					var t = field;
					field = new bool[3, 3];
					for (int i0 = 0; i0 < 3; i0++)
						for (int j0 = 0; j0 < 3; j0++)
							field[i0, j0] = t[i0, j0];
				}
			}
			else if (freeRow[i] && freeCol[j])
			{
				freeRow[i] = freeCol[j] = false;
				field[i, j] = true;
				FillIndent(deap);
				Console.WriteLine("-({0},{1})", i, j);
				for (int i0 = 0; i0 < 3; i0++)
					for (int j0 = 0; j0 < 3; j0++)
						Solve(i0, j0, deap + 1);
				field[i, j] = false;
				freeRow[i] = freeCol[j] = true;
			}
		}
	}
}
