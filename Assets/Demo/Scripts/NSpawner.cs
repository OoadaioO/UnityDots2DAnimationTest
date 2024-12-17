using UnityEngine;

public class NSpawner : MonoBehaviour
{

	[SerializeField] Transform prefab;

	[SerializeField] int spawnCountMax;

	[SerializeField] int columnCount;

	private void Start()
	{
		Spawner();
	}

	private void Spawner()
	{
		Vector3 lowerLeft = new Vector3(-1, -3, 0);
		for (int i = 0; i < spawnCountMax; i++)
		{
			int row = i / columnCount;
			int column = i % columnCount;
			Transform t = Instantiate(prefab, transform);
			t.localPosition = new Vector3(row * 0.1f, column * 0.1f, 0) + lowerLeft;
		}
	}


}
