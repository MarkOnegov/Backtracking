using System;

namespace Backtracking.Backtrakings
{
	class Calculator : Backtracking
	{
		int count;
		public Calculator()
		{
			Name = "Калькулятор";
			Index = 5;
		}
		public override void Solve()
		{
			count = 0;
			Solve(1, "1");
			Console.WriteLine(count);
		}
		private void Solve(int res, string s)
		{
			if (res == 6)
			{
				Console.WriteLine(s + " = 6");
				count++;
				return;
			}
			if (res < 6)
			{
				Console.WriteLine(s + " +1");
				Solve(res + 1, s + " +1");
				Console.WriteLine(s + " *2");
				Solve(res * 2, s + " *2");
				return;
			}
			Console.WriteLine(s + " > 6");
		}
	}
}
