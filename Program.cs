using System;
using System.Collections.Generic;
using System.Linq;

namespace Backtracking
{
	class Program
	{
		static Backtracking Select(List<Backtracking> backtrackings)
		{
			Console.WriteLine("Выберите задачу:");
			int index = 1;
			backtrackings.ForEach(bt =>
			{ Console.WriteLine("{0})\t{1}", bt.Index, bt.Name); });
			Console.WriteLine("0)\tВыход");
			while (true)
			{
				try
				{
					index = Convert.ToInt32(Console.ReadLine());
					if (index == 0) return null;
					return backtrackings.Find(i => i.Index == index);
				}
				catch { Console.WriteLine("Ошибка ввода. Попробуйте ещё раз"); }
			}
		}
		static void Main()
		{
			var backtrackings = typeof(Backtracking).Assembly
				.GetTypes().Where(t => t.IsSubclassOf(typeof(Backtracking)) && !t.IsAbstract)
				.Select(t => (Backtracking)Activator.CreateInstance(t))
				.OrderBy(i => i.Index).ToList();
			while (true)
			{
				Console.Clear();
				Backtracking bt = Select(backtrackings);
				if (bt == null) return;
				Console.Clear();
				bt.Solve();
				Console.ReadKey();
			}
		}
	}
}
