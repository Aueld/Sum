using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Hex
{
	public class HexCoordinate : MonoBehaviour
	{
		[SerializeField]
		public float q;
		[SerializeField]
		public float r;

		public HexCoord HexCoord
		{
			get
			{
				return (HexCoord)this;
			}
		}

		public HexCoordinate() { }

		public HexCoordinate(HexCoord hex)
		{
			Become(hex);
		}

		public void Become(HexCoord hex)
		{
			q = hex.q;
			r = hex.r;
		}

		public bool Is(HexCoord hex)
		{
			return this.q == hex.q && this.r == hex.r;
		}

		public static implicit operator HexCoord(HexCoordinate hex)
		{
			return new HexCoord(hex.q, hex.r);
		}

		public static implicit operator HexCoordinate(HexCoord hex)
		{
			return new HexCoordinate(hex);
		}
	}

}