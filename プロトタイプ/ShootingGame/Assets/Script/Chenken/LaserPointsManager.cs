using System;
using System.Collections.Generic;
using UnityEngine;


public class LaserPointsManager : Singleton<LaserPointsManager>
{
	public class PointsList
	{
		public List<GameObject> points;
	}

	private Dictionary<int, PointsList> allPointsDic = new Dictionary<int, PointsList>();
}

