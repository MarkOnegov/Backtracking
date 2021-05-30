using System;
using System.Collections.Generic;
using System.Linq;

namespace Backtracking.Backtrakings
{
	class Permutation : Backtracking
	{
		readonly List<byte> state = new List<byte>();
		int count;
		public Permutation()
		{
			Name = "Перестановка";
			Index = 6;
		}
		public override void Solve()
		{
			count = 0;
			Solve(0);
			Console.WriteLine(count);
		}
		private void Solve(byte index)
		{
			WriteState();
			if (index != 4)
				GetAvailable(index).ForEach(a =>
				{
					state.Add(a);
					Solve((byte)(index + 1));
					state.Remove(a);
				});
			else
				count++;
		}
		private void WriteState()
		{
			for (int i = 0; i < state.Count; i++)
			{
				if (i != 0)
					Console.Write(", ");
				Console.Write(state[i]);
			}
			Console.WriteLine();
		}
		private List<byte> GetAvailable(byte index)
		{
			var availabla = new List<byte>();
			for (byte i = 1; i < 5; i++)
				if (i != index + 1 && !state.Any(n => n == i))
					availabla.Add(i);
			return availabla;
		}
	}
}
