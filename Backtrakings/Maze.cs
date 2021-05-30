using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Backtracking.Backtrakings
{
	class Maze : Backtracking
	{
		class Point : IEquatable<Point>
		{
			private int x = 0;
			private int y = 0;
			public int X { get => x; set => x = value; }
			public int Y { get => y; set => y = value; }
			public Point() { }
			public Point(int x, int y)
			{
				this.x = x;
				this.y = y;
			}
			public Point(Point p)
			{
				x = p.x;
				y = p.y;
			}
			public override bool Equals(object obj) => Equals(obj as Point);
			public bool Equals(Point other) => other != null &&
					   x == other.x &&
					   y == other.y;
			public override int GetHashCode()
			{
				int hashCode = 1502939027;
				hashCode = hashCode * -1521134295 + x.GetHashCode();
				hashCode = hashCode * -1521134295 + y.GetHashCode();
				return hashCode;
			}
			public static bool operator ==(Point left, Point right) =>
				EqualityComparer<Point>.Default.Equals(left, right);
			public static bool operator !=(Point left, Point right) => !(left == right);
		}
		List<Point> state;
		Point place;
		Point finish;
		int[,] map;
		public Maze()
		{
			Name = "Лабиринт";
			Index = 8;
		}
		public override void Solve()
		{
			ReadMap("map.txt");
			state = new List<Point>();
			try
			{
				Solve(1);
			}
			catch (Exception e) { if (e.Message != "Done!") throw e; }
			PrintMap();
		}
		private void PrintMap()
		{
			int w = map.GetLength(1), h = map.GetLength(0);
			for (int i = 0; i < h; i++)
			{
				Console.Write('|');
				for (int j = 0; j < w; j++)
					Console.Write((map[i, j] == 1 ? "####" : string.Format("{0,4}", -map[i, j])) + '|');
				Console.WriteLine();
			}
		}
		private void Solve(int deap)
		{
			state.Add(new Point(place));
			if (state.IndexOf(place) < state.Count - 1) return;
			map[place.X, place.Y] = -deap;
			if (place == finish) throw new Exception("Done!");
			if (map[place.X - 1, place.Y] != 1)
			{
				place = new Point(place.X - 1, place.Y);
				Solve(deap + 1);
				place = new Point(place.X + 1, place.Y);
			}
			if (map[place.X + 1, place.Y] != 1)
			{
				place = new Point(place.X + 1, place.Y);
				Solve(deap + 1);
				place = new Point(place.X - 1, place.Y);
			}
			if (map[place.X, place.Y - 1] != 1)
			{
				place = new Point(place.X, place.Y - 1);
				Solve(deap + 1);
				place = new Point(place.X, place.Y + 1);
			}
			if (map[place.X, place.Y + 1] != 1)
			{
				place = new Point(place.X, place.Y + 1);
				Solve(deap + 1);
				place = new Point(place.X, place.Y - 1);
			}
			map[place.X, place.Y] = 0;
			state.RemoveAt(state.Count - 1);
		}
		private void ReadMap(string path)
		{
			using (StreamReader sr = new StreamReader(path))
			{
				var t = sr.ReadLine().Split(' ').Select(s => Convert.ToInt32(s)).ToArray();
				map = new int[t[0], t[1]];
				for (int i = 0; i < t[0]; i++)
				{
					var s = sr.ReadLine();
					for (int j = 0; j < t[1]; j++)
					{
						switch (s[j])
						{
							case '0':
								map[i, j] = 1;
								break;
							case '1':
								place = new Point(i, j);
								break;
							case '2':
								finish = new Point(i, j);
								break;
						}
					}
				}
			}
			if (place == null || finish == null)
				throw new ArgumentException("Нет начальной позиции или конечной");
		}
	}
}
