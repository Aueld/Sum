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

		// qr 좌표계 사용
		public float Z
		{
			get { return -q - r; }
		}

		// y = 0 일때 x 좌표
		public float O
		{
			get { return q + ((int)r >> 1); }
		}

		// 유니티 상에서 Hex타일 좌표
		public Vector2 Position()
		{
			return q * Q_XY + r * R_XY;
		}

		// (0, 0)으로부터의 거리
		public float AxialLength()
		{
			if (q == 0 && r == 0) return 0;
			if (q > 0 && r >= 0) return q + r;
			if (q <= 0 && r > 0) return (-q < r) ? r : -q;
			if (q < 0) return -q - r;
			return (-r > q) ? -r : q;
		}

		// (0, 0)으로부터의 육각형 단계 수
		public float AxialSkew()
		{
			if (q == 0 && r == 0) return 0;
			if (q > 0 && r >= 0) return (q < r) ? q : r;
			if (q <= 0 && r > 0) return (-q < r) ? Math.Min(-q, q + r) : Math.Min(r, -q - r);
			if (q < 0) return (q > r) ? -q : -r;
			return (-r > q) ? Math.Min(q, -q - r) : Math.Min(-r, q + r);
		}

		// (0, 0)으로부터의 16진수 중심 각도
		public float PolarAngle()
		{
			Vector3 pos = Position();
			return (float)Math.Atan2(pos.y, pos.x);
		}

		// (0, 0)으로부터의 16진수 반대 방향 위치
		public float PolarIndex()
		{
			if (q == 0 && r == 0) return 0;
			if (q > 0 && r >= 0) return r;
			if (q <= 0 && r > 0) return (-q < r) ? r - q : -3 * q - r;
			if (q < 0) return -4 * (q + r) + q;
			return (-r > q) ? -4 * r + q : 6 * q + r;
		}

		// 인접 육각형 이웃 오른쪽 시계 반대방향으로 진행
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


		// 해당 육각형의 주변 6개를 열거 0은 오른쪽 시계 반대방향 순
		public IEnumerable<HexCoord> Neighbors(int first = 0)
		{
			foreach (HexCoord hex in NeighborVectors(first))
				yield return hex + this;
		}

		// 모서리 정점 Unity 위치 반환, 모서리 0은 오른쪽 상단 이후 시계 반대방향
		public Vector2 Corner(int index)
		{
			return CornerVector(index) + Position();
		}

		// 해당 육각형 6개의 모서리를 열거, 모서리 0은 오른쪽 상단 이후 시계 반대방향
		public IEnumerable<Vector2> Corners(int first = 0)
		{
			Vector2 pos = Position();
			foreach (Vector2 v in CornerVectors(first))
				yield return v + pos;
		}

		// 모서리 정점에 대한 극각, (0, 0) 중심에서 16진수의 선택된 모서리까지의 각도(반지름)
		public float CornerPolarAngle(int index)
		{
			Vector2 pos = Corner(index);
			return (float)Math.Atan2(pos.y, pos.x);
		}
		
		// 시계방향 경계 모서리에 대한 극각 반환, 두 극 경계 모서리는 극각도가 가장 넓은 호를 형성하는 모서리
		public float PolarBoundingAngle(bool CCW = false)
		{
			return CornerPolarAngle(PolarBoundingCornerIndex(CCW));
		}

		// 시계방향 경계 모서리의 X Y 위치를 반환, 두 극 경계 모서리는 극각도가 가장 넓은 호를 형성하는 모서리
		public Vector2 PolarBoundingCorner(bool CCW = false)
		{
			return Corner(PolarBoundingCornerIndex(CCW));
		}

		// 시계 방향 경계 모서리의 인덱스 반환, 두 극 경계 모서리는 극각도가 가장 넓은 호를 형성하는 모서리,
		// true로 설정하면 시계 반대 방향 경계모서리 반황, 동일한 링 이웃이 공유하는 다른 모서리 반환
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

		// 16진수를 포함하는 원점의 절반 육분의 반환
		// CornerSextant는 HalfSextant / 2. NeighborSextant는 (HalfSextant+1) / 2
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

		// 16진수의 극좌표 벡터에 가장 가까운 모서리 인덱스 0. 0 반환
		public int CornerSextant()
		{
			if (q > 0 && r >= 0 || q == 0 && r == 0) return 0;
			if (q <= 0 && r > 0) return (-q < r) ? 1 : 2;
			if (q < 0) return 3;
			return (-r > q) ? 4 : 5;
		}

		// 16진수의 극좌표 벡터가 통과하는 0, 0의 이웃 인덱스 반환
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

		// 육분의 증분으로 0, 0 주위를 회전
		// 회전 후 이를 나타내는 새로운 값
		// sextants는 회전할 육분의 수
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

		// 3차 축을 가로질러 미러링, 3차축은 육각형의 대해 대각선, 두개의 반대 모서리 통과
		public HexCoord Mirror(int axis = 1)
		{
			if (this == origin) return this;
			axis = NormalizeRotationIndex(axis, 3);
			if (axis == 0) return new HexCoord(r, q);
			if (axis == 1) return new HexCoord(Z, r);
			return new HexCoord(q, Z);
		}


		// 벡터로 크기를 자르고 반환
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

		// 16진수가 지정된 사각형 내에 있는지 여부 결정, 사각형 내에 있으면 true
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

		// 16진수가 점과 b를 통과하는 무한선에 있는지 확인
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

		// 점 a 와 점 b 사이의 선분에 있는지 여부 결정
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

		// 좌표 String 반환
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

		
		// 두 육각형 사이의 거리
		public static float Distance(HexCoord a, HexCoord b)
		{
			return (a - b).AxialLength();
		}

		// 인덱스만큼 회전 인덱스 정규화
		public static int NormalizeRotationIndex(int index, int cycle = 6)
		{
			if (index < 0 ^ cycle < 0)
				return (index % cycle + cycle) % cycle;
			else
				return index % cycle;
		}

		// 주어진 주기에 대한 두 회전 인덱스의 동일성 결정
		public static bool IsSameRotationIndex(int a, int b, int cycle = 6)
		{
			return 0 == NormalizeRotationIndex(a - b, cycle);
		}

		// 16진수에서 이웃으로의 벡터, 이웃은 오른쪽 이후 시계 반대 방향 진행
		public static HexCoord NeighborVector(int index)
		{ return neighbors[NormalizeRotationIndex(index, 6)]; }

		// 6개의 이웃 벡터 열거, 이웃은 오른쪽 이후 시계 반대 방향 진행
		public static IEnumerable<HexCoord> NeighborVectors(int first = 0)
		{
			first = NormalizeRotationIndex(first, 6);
			for (int i = first; i < 6; i++)
				yield return neighbors[i];
			for (int i = 0; i < first; i++)
				yield return neighbors[i];
		}

		// 극각이 통과하는 (0, 0)의 이웃 인덱스
		public static int AngleToNeighborIndex(float angle)
		{ return Mathf.RoundToInt(angle / SEXTANT); }

		// (0, 0)의 이웃에 대한 극각
		public static float NeighborIndexToAngle(int index)
		{ return index * SEXTANT; }

		// 16진수 중심에서 모서리까지의 유니티 위치 벡터, 모서리 0은 오른쪽, 이후 시계 반대 방향 진행
		// 인덱스는 원하는 코너의 인덱스
		public static Vector2 CornerVector(int index)
		{
			return corners[NormalizeRotationIndex(index, 6)];
		}

		// 6개의 모서리 벡터 열거, 모서리 0은 오른쪽 상단, 이후 시계 반대 방향 진행
		// first는 열거할 첫 번째 모서리 벡터 인덱스
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

		// 극각에 가장 가까운 0,0의 모서리
		public static int AngleToCornerIndex(float angle)
		{ return Mathf.FloorToInt(angle / SEXTANT); }

		// 0,0의 모서리에 대한 극각.
		public static float CornerIndexToAngle(int index)
		{ return (index + 0.5f) * SEXTANT; }

		// 극각이 통과하는 0,0의 반 육분의
		public static int AngleToHalfSextant(float angle)
		{ return Mathf.RoundToInt(2 * angle / SEXTANT); }

		// 반 육분의가 시작되는 극각
		public static float HalfSextantToAngle(int index)
		{ return index * SEXTANT / 2; }

		// 유니티에서의 위치 좌표
		public static HexCoord AtPosition(Vector2 position)
		{ return FromQRVector(VectorXYtoQR(position)); }

		// 육각 극좌표 원을 육각형 고리로 근사화, 시계 반대 방향 인덱스
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

		// 반지름에서 각도에 가장 가까운 육각형 극 인덱스, 원을 육각형 고리로 근사화
		public static int FindPolarIndex(int radius, float angle)
		{
			return (int)Math.Round(angle * radius * 3 / Mathf.PI);
		}

		// 오프셋 좌표 x = q + r / 2 
		public static HexCoord AtOffset(int x, int y)
		{
			return new HexCoord(x - (y >> 1), y);
		}

		// 부동소수점 q r 벡터
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

		// x, y 벡터 -> q, r 벡터 변환
		public static Vector2 VectorXYtoQR(Vector2 XYvector)
		{
			return XYvector.x * X_QR + XYvector.y * Y_QR;
		}

		// q, r 벡터 -> x, y 벡터 변환
		public static Vector2 VectorQRtoXY(Vector2 QRvector)
		{
			return QRvector.x * Q_XY + QRvector.y * R_XY;
		}
		
		// X Y 공간 직사각형과 접하는 모든 셀을 포함하는 Q R 공간 직사각형의 모서리 반환
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

		// QR/XY 혼동 피하기 위해 QR 공간에서 Vector2 캐스트.
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

		public override bool Equals(object o)
		{ return (o is HexCoord) && this == (HexCoord)o; }

		public override int GetHashCode()
		{
			return (int)q & (int)0xFFFF | (int)r << 16;
		}

		public static readonly float SEXTANT = Mathf.PI / 3;
		public static readonly float SQRT3 = Mathf.Sqrt(3);

		// 방향 배열
		static readonly HexCoord[] neighbors = {
			new HexCoord(1, 0),
			new HexCoord(0, 1),
			new HexCoord(-1, 1),
			new HexCoord(-1, 0),
			new HexCoord(0, -1),
			new HexCoord(1, -1)
		};

		// XY 공간 코너 위치
		static readonly Vector2[] corners = {
			new Vector2(Mathf.Sin(SEXTANT), Mathf.Cos(SEXTANT)),
			new Vector2(0, 1),
			new Vector2(Mathf.Sin(-SEXTANT), Mathf.Cos(-SEXTANT)),
			new Vector2(Mathf.Sin(Mathf.PI + SEXTANT), Mathf.Cos(Mathf.PI - SEXTANT)),
			new Vector2(0, -1),
			new Vector2(Mathf.Sin(Mathf.PI - SEXTANT), Mathf.Cos(Mathf.PI - SEXTANT))
		};

		// QR 과 XY 공간 사이의 벡터 변환
		static readonly Vector2 Q_XY = new Vector2(SQRT3, 0);
		static readonly Vector2 R_XY = new Vector2(SQRT3 / 2, 1.5f);
		static readonly Vector2 X_QR = new Vector2(SQRT3 / 3, 0);
		static readonly Vector2 Y_QR = new Vector2(-1 / 3f, 2 / 3f);

	}
}
