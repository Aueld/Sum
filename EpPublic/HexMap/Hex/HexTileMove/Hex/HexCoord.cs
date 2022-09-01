using UnityEngine;
using System;
using System.Collections.Generic;

namespace Hex
{
	public struct HexCoord
	{
		[SerializeField]
		public float q;

		[SerializeField]
		public float r;

		public HexCoord(float q, float r)
		{
			this.q = q;
			this.r = r;
		}

		// qr ��ǥ�� ���
		public float Z
		{
			get { return -q - r; }
		}

		// y = 0 �϶� x ��ǥ
		public float O
		{
			get { return q + ((int)r >> 1); }
		}

		// ����Ƽ �󿡼� HexŸ�� ��ǥ
		public Vector2 Position()
		{
			return q * Q_XY + r * R_XY;
		}

		// (0, 0)���κ����� �Ÿ�
		public float AxialLength()
		{
			if (q == 0 && r == 0) return 0;
			if (q > 0 && r >= 0) return q + r;
			if (q <= 0 && r > 0) return (-q < r) ? r : -q;
			if (q < 0) return -q - r;
			return (-r > q) ? -r : q;
		}

		// (0, 0)���κ����� ������ �ܰ� ��
		public float AxialSkew()
		{
			if (q == 0 && r == 0) return 0;
			if (q > 0 && r >= 0) return (q < r) ? q : r;
			if (q <= 0 && r > 0) return (-q < r) ? Math.Min(-q, q + r) : Math.Min(r, -q - r);
			if (q < 0) return (q > r) ? -q : -r;
			return (-r > q) ? Math.Min(q, -q - r) : Math.Min(-r, q + r);
		}

		// (0, 0)���κ����� 16���� �߽� ����
		public float PolarAngle()
		{
			Vector3 pos = Position();
			return (float)Math.Atan2(pos.y, pos.x);
		}

		// (0, 0)���κ����� 16���� �ݴ� ���� ��ġ
		public float PolarIndex()
		{
			if (q == 0 && r == 0) return 0;
			if (q > 0 && r >= 0) return r;
			if (q <= 0 && r > 0) return (-q < r) ? r - q : -3 * q - r;
			if (q < 0) return -4 * (q + r) + q;
			return (-r > q) ? -4 * r + q : 6 * q + r;
		}

		// ���� ������ �̿� ������ �ð� �ݴ�������� ����
		public HexCoord Neighbor(int index)
		{
			return NeighborVector(index) + this;
		}

		public HexCoord PolarNeighbor(bool CCW = false)
		{
			if (q > 0)
			{
				if (r < 0)
				{
					if (q > -r) return this + neighbors[CCW ? 1 : 4];
					if (q < -r) return this + neighbors[CCW ? 0 : 3];
					return this + neighbors[CCW ? 1 : 3];
				}
				if (r > 0) return this + neighbors[CCW ? 2 : 5];
				return this + neighbors[CCW ? 2 : 4];
			}
			if (q < 0)
			{
				if (r > 0)
				{
					if (r > -q) return this + neighbors[CCW ? 3 : 0];
					if (r < -q) return this + neighbors[CCW ? 4 : 1];
					return this + neighbors[CCW ? 4 : 0];
				}
				if (r < 0) return this + neighbors[CCW ? 5 : 2];
				return this + neighbors[CCW ? 5 : 1];
			}
			if (r > 0) return this + neighbors[CCW ? 3 : 5];
			if (r < 0) return this + neighbors[CCW ? 0 : 2];
			return this;
		}


		// �ش� �������� �ֺ� 6���� ���� 0�� ������ �ð� �ݴ���� ��
		public IEnumerable<HexCoord> Neighbors(int first = 0)
		{
			foreach (HexCoord hex in NeighborVectors(first))
				yield return hex + this;
		}

		// �𼭸� ���� Unity ��ġ ��ȯ, �𼭸� 0�� ������ ��� ���� �ð� �ݴ����
		public Vector2 Corner(int index)
		{
			return CornerVector(index) + Position();
		}

		// �ش� ������ 6���� �𼭸��� ����, �𼭸� 0�� ������ ��� ���� �ð� �ݴ����
		public IEnumerable<Vector2> Corners(int first = 0)
		{
			Vector2 pos = Position();
			foreach (Vector2 v in CornerVectors(first))
				yield return v + pos;
		}

		// �𼭸� ������ ���� �ذ�, (0, 0) �߽ɿ��� 16������ ���õ� �𼭸������� ����(������)
		public float CornerPolarAngle(int index)
		{
			Vector2 pos = Corner(index);
			return (float)Math.Atan2(pos.y, pos.x);
		}
		
		// �ð���� ��� �𼭸��� ���� �ذ� ��ȯ, �� �� ��� �𼭸��� �ذ����� ���� ���� ȣ�� �����ϴ� �𼭸�
		public float PolarBoundingAngle(bool CCW = false)
		{
			return CornerPolarAngle(PolarBoundingCornerIndex(CCW));
		}

		// �ð���� ��� �𼭸��� X Y ��ġ�� ��ȯ, �� �� ��� �𼭸��� �ذ����� ���� ���� ȣ�� �����ϴ� �𼭸�
		public Vector2 PolarBoundingCorner(bool CCW = false)
		{
			return Corner(PolarBoundingCornerIndex(CCW));
		}

		// �ð� ���� ��� �𼭸��� �ε��� ��ȯ, �� �� ��� �𼭸��� �ذ����� ���� ���� ȣ�� �����ϴ� �𼭸�,
		// true�� �����ϸ� �ð� �ݴ� ���� ���𼭸� ��Ȳ, ������ �� �̿��� �����ϴ� �ٸ� �𼭸� ��ȯ
		public int PolarBoundingCornerIndex(bool CCW = false)
		{
			if (q == 0 && r == 0) return 0;
			if (q > 0 && r >= 0) return CCW ?
				(q > r) ? 1 : 2 :
				(q < r) ? 5 : 4;
			if (q <= 0 && r > 0) return (-q < r) ?
				CCW ?
					(r > -2 * q) ? 2 : 3 :
					(r < -2 * q) ? 0 : 5 :
				CCW ?
					(q > -2 * r) ? 3 : 4 :
					(q < -2 * r) ? 1 : 0;
			if (q < 0) return CCW ?
				(q < r) ? 4 : 5 :
				(q > r) ? 2 : 1;
			return (-r > q) ?
				CCW ?
					(r < -2 * q) ? 5 : 0 :
					(r > -2 * q) ? 3 : 2 :
				CCW ?
					(q < -2 * r) ? 0 : 1 :
					(q > -2 * r) ? 4 : 3;
		}

		// 16������ �����ϴ� ������ ���� ������ ��ȯ
		// CornerSextant�� HalfSextant / 2. NeighborSextant�� (HalfSextant+1) / 2
		public int HalfSextant()
		{
			if (q > 0 && r >= 0 || q == 0 && r == 0)
				return (q > r) ? 0 : 1;
			if (q <= 0 && r > 0)
				return (-q < r) ?
					(r > -2 * q) ? 2 : 3 :
					(q > -2 * r) ? 4 : 5;
			if (q < 0)
				return (q < r) ? 6 : 7;
			return (-r > q) ?
				(r < -2 * q) ? 8 : 9 :
				(q < -2 * r) ? 10 : 11;
		}

		// 16������ ����ǥ ���Ϳ� ���� ����� �𼭸� �ε��� 0. 0 ��ȯ
		public int CornerSextant()
		{
			if (q > 0 && r >= 0 || q == 0 && r == 0) return 0;
			if (q <= 0 && r > 0) return (-q < r) ? 1 : 2;
			if (q < 0) return 3;
			return (-r > q) ? 4 : 5;
		}

		// 16������ ����ǥ ���Ͱ� ����ϴ� 0, 0�� �̿� �ε��� ��ȯ
		public int NeighborSextant()
		{
			if (q == 0 && r == 0) return 0;
			if (q > 0 && r >= 0) return (q <= r) ? 1 : 0;
			if (q <= 0 && r > 0) return (-q <= r) ?
				(r <= -2 * q) ? 2 : 1 :
				(q <= -2 * r) ? 3 : 2;
			if (q < 0) return (q >= r) ? 4 : 3;
			return (-r > q) ?
				(r >= -2 * q) ? 5 : 4 :
				(q >= -2 * r) ? 0 : 5;
		}

		// ������ �������� 0, 0 ������ ȸ��
		// ȸ�� �� �̸� ��Ÿ���� ���ο� ��
		// sextants�� ȸ���� ������ ��
		public HexCoord SextantRotation(int sextants)
		{
			if (this == origin) return this;
			sextants = NormalizeRotationIndex(sextants, 6);
			if (sextants == 0) return this;
			if (sextants == 1) return new HexCoord(-r, -Z);
			if (sextants == 2) return new HexCoord(Z, q);
			if (sextants == 3) return new HexCoord(-q, -r);
			if (sextants == 4) return new HexCoord(r, Z);
			return new HexCoord(-Z, -q);
		}

		// 3�� ���� �������� �̷���, 3������ �������� ���� �밢��, �ΰ��� �ݴ� �𼭸� ���
		public HexCoord Mirror(int axis = 1)
		{
			if (this == origin) return this;
			axis = NormalizeRotationIndex(axis, 3);
			if (axis == 0) return new HexCoord(r, q);
			if (axis == 1) return new HexCoord(Z, r);
			return new HexCoord(q, Z);
		}


		// ���ͷ� ũ�⸦ �ڸ��� ��ȯ
		public HexCoord Scale(float factor)
		{
			q = (int)(q * factor);
			r = (int)(r * factor);
			return this;
		}

		public HexCoord Scale(int factor)
		{
			q *= factor;
			r *= factor;
			return this;
		}

		public Vector2 ScaleToVector(float factor)
		{ return new Vector2(q * factor, r * factor); }

		// 16������ ������ �簢�� ���� �ִ��� ���� ����, �簢�� ���� ������ true
		public bool IsWithinRectangle(HexCoord cornerA, HexCoord cornerB)
		{
			if (r > cornerA.r && r > cornerB.r || r < cornerA.r && r < cornerB.r)
				return false;
			bool reverse = cornerA.O > cornerB.O;   // Travel right to left.
			bool offset = cornerA.r % 2 != 0;   // Starts on an odd row, bump alternate rows left.
			bool trim = Math.Abs(cornerA.r - cornerB.r) % 2 == 0;   // Even height, trim alternate rows.
			bool odd = (r - cornerA.r) % 2 != 0; // This is an alternate row.
			float width = Math.Abs(cornerA.O - cornerB.O);
			bool hasWidth = width != 0;
			if (reverse && (odd && (trim || !offset) || !(trim || offset || odd))
				|| !reverse && (trim && odd || offset && !trim && hasWidth))
				width -= 1;
			float x = (O - cornerA.O) * (reverse ? -1 : 1);
			if (reverse && odd && !offset
				|| !reverse && offset && odd && hasWidth)
				x -= 1;
			return (x <= width && x >= 0);
		}

		// 16������ ���� b�� ����ϴ� ���Ѽ��� �ִ��� Ȯ��
		public bool IsOnCartesianLine(Vector2 a, Vector2 b)
		{
			Vector2 AB = b - a;
			bool bias = Vector3.Cross(AB, Corner(0) - a).z > 0;
			for (int i = 1; i < 6; i++)
			{
				if (bias != (Vector3.Cross(AB, Corner(i) - a).z > 0))
					return true;
			}
			return false;
		}

		// �� a �� �� b ������ ���п� �ִ��� ���� ����
		public bool IsOnCartesianLineSegment(Vector2 a, Vector2 b)
		{
			Vector2 AB = b - a;
			float mag = AB.sqrMagnitude;
			Vector2 AC = Corner(0) - a;
			bool within = AC.sqrMagnitude <= mag && Vector2.Dot(AB, AC) >= 0;
			int sign = Math.Sign(Vector3.Cross(AB, AC).z);
			for (int i = 1; i < 6; i++)
			{
				AC = Corner(i) - a;
				bool newWithin = AC.sqrMagnitude <= mag && Vector2.Dot(AB, AC) >= 0;
				int newSign = Math.Sign(Vector3.Cross(AB, AC).z);
				if ((within || newWithin) && (sign * newSign <= 0))
					return true;
				within = newWithin;
				sign = newSign;
			}
			return false;
		}

		// ��ǥ String ��ȯ
		public override string ToString()
		{
			return "(" + q + "," + r + ")";
		}

		/*
		 * Static Methods
		 */

		/// <summary>
		/// HexCoord at (0,0)
		/// </summary>
		public static readonly HexCoord origin = default(HexCoord);

		
		// �� ������ ������ �Ÿ�
		public static float Distance(HexCoord a, HexCoord b)
		{
			return (a - b).AxialLength();
		}

		// �ε�����ŭ ȸ�� �ε��� ����ȭ
		public static int NormalizeRotationIndex(int index, int cycle = 6)
		{
			if (index < 0 ^ cycle < 0)
				return (index % cycle + cycle) % cycle;
			else
				return index % cycle;
		}

		// �־��� �ֱ⿡ ���� �� ȸ�� �ε����� ���ϼ� ����
		public static bool IsSameRotationIndex(int a, int b, int cycle = 6)
		{
			return 0 == NormalizeRotationIndex(a - b, cycle);
		}

		// 16�������� �̿������� ����, �̿��� ������ ���� �ð� �ݴ� ���� ����
		public static HexCoord NeighborVector(int index)
		{ return neighbors[NormalizeRotationIndex(index, 6)]; }

		// 6���� �̿� ���� ����, �̿��� ������ ���� �ð� �ݴ� ���� ����
		public static IEnumerable<HexCoord> NeighborVectors(int first = 0)
		{
			first = NormalizeRotationIndex(first, 6);
			for (int i = first; i < 6; i++)
				yield return neighbors[i];
			for (int i = 0; i < first; i++)
				yield return neighbors[i];
		}

		// �ذ��� ����ϴ� (0, 0)�� �̿� �ε���
		public static int AngleToNeighborIndex(float angle)
		{ return Mathf.RoundToInt(angle / SEXTANT); }

		// (0, 0)�� �̿��� ���� �ذ�
		public static float NeighborIndexToAngle(int index)
		{ return index * SEXTANT; }

		// 16���� �߽ɿ��� �𼭸������� ����Ƽ ��ġ ����, �𼭸� 0�� ������, ���� �ð� �ݴ� ���� ����
		// �ε����� ���ϴ� �ڳ��� �ε���
		public static Vector2 CornerVector(int index)
		{
			return corners[NormalizeRotationIndex(index, 6)];
		}

		// 6���� �𼭸� ���� ����, �𼭸� 0�� ������ ���, ���� �ð� �ݴ� ���� ����
		// first�� ������ ù ��° �𼭸� ���� �ε���
		public static IEnumerable<Vector2> CornerVectors(int first = 0)
		{
			if (first == 0)
			{
				foreach (Vector2 v in corners)
					yield return v;
			}
			else
			{
				first = NormalizeRotationIndex(first, 6);
				for (int i = first; i < 6; i++)
					yield return corners[i];
				for (int i = 0; i < first; i++)
					yield return corners[i];
			}
		}

		// �ذ��� ���� ����� 0,0�� �𼭸�
		public static int AngleToCornerIndex(float angle)
		{ return Mathf.FloorToInt(angle / SEXTANT); }

		// 0,0�� �𼭸��� ���� �ذ�.
		public static float CornerIndexToAngle(int index)
		{ return (index + 0.5f) * SEXTANT; }

		// �ذ��� ����ϴ� 0,0�� �� ������
		public static int AngleToHalfSextant(float angle)
		{ return Mathf.RoundToInt(2 * angle / SEXTANT); }

		// �� �����ǰ� ���۵Ǵ� �ذ�
		public static float HalfSextantToAngle(int index)
		{ return index * SEXTANT / 2; }

		// ����Ƽ������ ��ġ ��ǥ
		public static HexCoord AtPosition(Vector2 position)
		{ return FromQRVector(VectorXYtoQR(position)); }

		// ���� ����ǥ ���� ������ ���� �ٻ�ȭ, �ð� �ݴ� ���� �ε���
		public static HexCoord AtPolar(int radius, int index)
		{
			if (radius == 0) return origin;
			if (radius < 0) radius = -radius;
			index = NormalizeRotationIndex(index, radius * 6);
			int sextant = index / radius;
			index %= radius;
			if (sextant == 0) return new HexCoord(radius - index, index);
			if (sextant == 1) return new HexCoord(-index, radius);
			if (sextant == 2) return new HexCoord(-radius, radius - index);
			if (sextant == 3) return new HexCoord(index - radius, -index);
			if (sextant == 4) return new HexCoord(index, -radius);
			return new HexCoord(radius, index - radius);
		}

		// ���������� ������ ���� ����� ������ �� �ε���, ���� ������ ���� �ٻ�ȭ
		public static int FindPolarIndex(int radius, float angle)
		{
			return (int)Math.Round(angle * radius * 3 / Mathf.PI);
		}

		// ������ ��ǥ x = q + r / 2 
		public static HexCoord AtOffset(int x, int y)
		{
			return new HexCoord(x - (y >> 1), y);
		}

		// �ε��Ҽ��� q r ����
		public static HexCoord FromQRVector(Vector2 QRvector)
		{
			float z = -QRvector.x - QRvector.y;
			int ix = (int)Math.Round(QRvector.x);
			int iy = (int)Math.Round(QRvector.y);
			int iz = (int)Math.Round(z);
			if (ix + iy + iz != 0)
			{
				float dx = Math.Abs(ix - QRvector.x);
				float dy = Math.Abs(iy - QRvector.y);
				float dz = Math.Abs(iz - z);
				if (dx >= dy && dx >= dz)
					ix = -iy - iz;
				else if (dy >= dz)
					iy = -ix - iz;
			}
			return new HexCoord(ix, iy);
		}

		// x, y ���� -> q, r ���� ��ȯ
		public static Vector3 VectorXYtoQR(Vector3 XZvector)
		{
			return XZvector.x * X_QR + XZvector.z * Y_QR;
		}

		// q, r ���� -> x, y ���� ��ȯ
		public static Vector2 VectorQRtoXY(Vector2 QRvector)
		{
			return QRvector.x * Q_XY + QRvector.y * R_XY;
		}
		
		// X Y ���� ���簢���� ���ϴ� ��� ���� �����ϴ� Q R ���� ���簢���� �𼭸� ��ȯ
		public static HexCoord[] CartesianRectangleBounds(Vector2 cornerA, Vector2 cornerB)
		{
			Vector2 min = new Vector2(Math.Min(cornerA.x, cornerB.x), Math.Min(cornerA.y, cornerB.y));
			Vector2 max = new Vector2(Math.Max(cornerA.x, cornerB.x), Math.Max(cornerA.y, cornerB.y));
			HexCoord[] results = {
				HexCoord.AtPosition(min),
				HexCoord.AtPosition(max)
			};
			Vector2 pos = results[0].Position();
			if (pos.y - 0.5f >= min.y)
				results[0] += neighbors[4];
			else if (pos.x >= min.x)
				results[0] += neighbors[3];
			pos = results[1].Position();
			if (pos.y + 0.5f <= max.y)
				results[1] += neighbors[1];
			else if (pos.x <= max.x)
				results[1] += neighbors[0];
			return results;
		}

		/*
		 * Operators
		 */

		// Cast to Vector2 in QR space. Explicit to avoid QR/XY mix-ups.
		public static explicit operator Vector2(HexCoord h)
		{ return new Vector2(h.q, h.r); }
		// +, -, ==, !=
		public static HexCoord operator +(HexCoord a, HexCoord b)
		{ return new HexCoord(a.q + b.q, a.r + b.r); }

		public static HexCoord operator -(HexCoord a, HexCoord b)
		{ return new HexCoord(a.q - b.q, a.r - b.r); }

		public static bool operator ==(HexCoord a, HexCoord b)
		{ return a.q == b.q && a.r == b.r; }

		public static bool operator !=(HexCoord a, HexCoord b)
		{ return a.q != b.q || a.r != b.r; }

		// Mandatory overrides: Equals(), GetHashCode()
		public override bool Equals(object o)
		{ return (o is HexCoord) && this == (HexCoord)o; }

		public override int GetHashCode()
		{
			return (int)q & (int)0xFFFF | (int)r << 16;
		}

		/*
		 * Constants
		 */

		/// <summary>
		/// One sixth of a full rotation (radians).
		/// </summary>
		public static readonly float SEXTANT = Mathf.PI / 3;

		/// <summary>
		/// Square root of 3.
		/// </summary>
		public static readonly float SQRT3 = Mathf.Sqrt(3);

		// The directions array. These are private to prevent overwriting elements.
		static readonly HexCoord[] neighbors = {
			new HexCoord(1, 0),
			new HexCoord(0, 1),
			new HexCoord(-1, 1),
			new HexCoord(-1, 0),
			new HexCoord(0, -1),
			new HexCoord(1, -1)
		};

		// Corner locations in XY space. Private for same reason as neighbors.
		static readonly Vector2[] corners = {
			new Vector2(Mathf.Sin(SEXTANT), Mathf.Cos(SEXTANT)),
			new Vector2(0, 1),
			new Vector2(Mathf.Sin(-SEXTANT), Mathf.Cos(-SEXTANT)),
			new Vector2(Mathf.Sin(Mathf.PI + SEXTANT), Mathf.Cos(Mathf.PI - SEXTANT)),
			new Vector2(0, -1),
			new Vector2(Mathf.Sin(Mathf.PI - SEXTANT), Mathf.Cos(Mathf.PI - SEXTANT))
		};

		// Vector transformations between QR and XY space.
		// Private to keep IntelliSense tidy. Safe to make public, but sensible uses are covered above.
		static readonly Vector2 Q_XY = new Vector2(SQRT3, 0);
		static readonly Vector2 R_XY = new Vector2(SQRT3 / 2, 1.5f);
		static readonly Vector2 X_QR = new Vector2(SQRT3 / 3, 0);
		static readonly Vector2 Y_QR = new Vector2(-1 / 3f, 2 / 3f);

	}
}