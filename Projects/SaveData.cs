using System;

namespace DemoMoteursDeJeu
{
	[Serializable]
	public class SaveData
	{
		public float X { get; set; }  // X position
		public float Y { get; set; }  // Y position

		public SaveData() { }

		public SaveData(float x, float y)
		{
			X = x;
			Y = y;
		}
	}
}
