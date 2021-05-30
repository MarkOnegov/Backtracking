using System;
using System.Collections.Generic;
using System.Linq;

namespace Backtracking.Backtrakings
{
	class Sum : Backtracking
	{
		readonly List<int> nums = new List<int>() { 2, 4, 1, 5 };
		readonly int sum = 8;
		readonly List<int> state = new List<int>();
		int count = 0;
		public Sum()
		{
			Name = "Сумма";
			Index = 2;
		}
		public override void Solve()
		{
			nums.Sort();
			if (nums.Count < 1) throw new ArgumentOutOfRangeException();
			if (nums[0] < 1) throw new NotImplementedException();
			Solve(nums.Count - 1, "");
			Console.WriteLine(count);
		}
		private void Solve(int index, string ans)
		{
			for (int i = index; i >= 0; i--)
			{
				state.Add(nums[i]);
				int sum = state.Sum();
				string newAns = ans + (ans.Length == 0 ? "" : "+") + nums[i];
				if (sum == this.sum)
				{
					count++;
					Console.WriteLine(newAns + "=" + this.sum);
				}
				else if (sum > this.sum)
					Console.WriteLine(newAns + ">" + this.sum);
				else
					Solve(i, newAns);
				state.Remove(nums[i]);
			}
		}
	}
}