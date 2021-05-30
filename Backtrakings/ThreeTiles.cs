using System;
using System.Collections.Generic;

namespace Backtracking.Backtrakings
{
	class ThreeTiles : Backtracking
	{
		class Field : IEquatable<Field>
		{
			byte field;
			public byte[,] ArrayField
			{
				get
				{
					byte[,] field = new byte[2, 2];
					for (int i = 0; i < 2; i++)
						for (int j = 0; j < 2; j++)
							field[i, j] = (byte)((this.field >> ((i * 2 + j) * 2)) & 0x3);
					return field;
				}
				set
				{
					field = 0;
					for (int i = 0; i < 2; i++)
						for (int j = 0; j < 2; j++)
							field |= (byte)(value[i, j] << ((i * 2 + j) * 2));
				}
			}
			public byte[] Empty
			{
				get
				{
					for (byte i = 0; i < 2; i++)
						for (byte j = 0; j < 2; j++)
							if (((field >> ((i * 2 + j) * 2)) & 0x3) == 0)
								return new byte[] { i, j };
					return null;
				}
			}
			public void PrintField()
			{
				int startX = Console.CursorLeft;
				int startY = Console.CursorTop;
				Console.Write("{0}|{1}", field & 3, (field >> 2) & 3);
				Console.CursorLeft = startX;
				Console.CursorTop = startY + 1;
				Console.Write("---");
				Console.CursorLeft = startX;
				Console.CursorTop = startY + 2;
				Console.Write("{0}|{1}", (field >> 4) & 3, (field >> 6) & 3);
				Console.CursorLeft = 0;
				Console.CursorTop = startY + 3;
			}
			public void Swap(int i, int j, int i1, int j1)
			{
				var f = ArrayField;
				var t = f[i, j];
				f[i, j] = f[i1, j1];
				f[i1, j1] = t;
				ArrayField = f;
			}
			public override bool Equals(object obj)
			{
				return Equals(obj as Field);
			}

			public bool Equals(Field other)
			{
				return other != null &&
					   field == other.field;
			}

			public override int GetHashCode()
			{
				return -306121345 + field.GetHashCode();
			}

			public static bool operator ==(Field left, Field right)
			{
				return EqualityComparer<Field>.Default.Equals(left, right);
			}

			public static bool operator !=(Field left, Field right)
			{
				return !(left == right);
			}
		}

		readonly List<Field> last = new List<Field>();
		readonly Field field = new Field();
		readonly Field done = new Field();
		int count = 0;
		public ThreeTiles()
		{
			Name = "Трёшки";
			Index = 7;
		}
		public override void Solve()
		{
			field.ArrayField = new byte[2, 2] { { 0, 1 }, { 3, 2 } };
			done.ArrayField = new byte[2, 2] { { 1, 2 }, { 0, 3 } };
			Solve(0);
			Console.WriteLine(count);
		}
		private void Solve(int deap)
		{
			last.Add(new Field() { ArrayField = field.ArrayField });
			Console.CursorLeft = deap * 4;
			field.PrintField();
			if (done.Equals(field))
			{
				count++;
				return;
			}
			if (last.IndexOf(field) < last.Count - 1)
				return;
			var empty = field.Empty;
			field.Swap(empty[0], 0, empty[0], 1);
			Solve(deap + 1);
			field.Swap(empty[0], 0, empty[0], 1);
			field.Swap(0, empty[1], 1, empty[1]);
			Solve(deap + 1);
			field.Swap(0, empty[1], 1, empty[1]);
			last.RemoveAt(last.Count - 1);
		}
	}
}
