using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooler : MonoBehaviour
{
	[System.Serializable]
	public class Enemy
	{
		public string tag;
		public GameObject prefab;
	}
	
	public static EnemyPooler Instance;
	public List<Enemy> enemies; // FROM UNITY
	public Dictionary<string, List<GameObject>> enemiesDictionary;

	void Awake()
	{
		Instance = this;

		enemiesDictionary = new Dictionary<string, List<GameObject>>();
		InitEnemy();
	}

    #region Initalizations
    void InitEnemy()
	{
		foreach (Enemy enemy in enemies)
		{
			List<GameObject> objectPool = new List<GameObject>();

			GameObject obj = Instantiate(enemy.prefab) as GameObject;
            obj.transform.parent = this.transform;
			if(enemy.tag == "boss")
				obj.GetComponent<EnemyStats>().Type = "boss";

			obj.SetActive(false);
			objectPool.Add(obj);

			enemiesDictionary.Add(enemy.tag, objectPool);
		}
	}
    #endregion

    #region Pooling
    public void GetPooledObject(List<SpawnPoint> spawnPoints)
	{
		string type;
		Transform point;
		bool expand = false;

		foreach (SpawnPoint spawnPoint in spawnPoints)
		{
			type = spawnPoint.enemyType;
			point = spawnPoint.point;
			int curSize = enemiesDictionary[type].Count;

			for (int i = 0; i < curSize; i++)
			{
				expand = false;
				if (!enemiesDictionary[type][i].activeInHierarchy)
				{
					//SetActive(false) 처리된 객체가 있을 경우
					enemiesDictionary[type][i].transform.position = point.position;
					enemiesDictionary[type][i].transform.rotation = point.rotation;
					enemiesDictionary[type][i].SetActive(true);
					break;
				}
				expand = true;
			}

			if (expand)
			{
				//SetActive(false) 처리된 객체가 없을 경우
				foreach (Enemy enemy in enemies)
				{
					if (enemy.tag == type)
					{
						GameObject obj = Instantiate(enemy.prefab, point.position, point.rotation) as GameObject;
                        obj.transform.parent = this.transform;

                        obj.SetActive(true);
						enemiesDictionary[type].Add(obj);
						break;
					}
				}
			}
		}
	}
	#endregion

	#region checkSurvier
	public bool checkAllDead()
	{
		foreach (Transform child in this.transform)
		{
			if (child.gameObject.activeInHierarchy)
			{
				return false;
			}
		}

		return true;
	}
	#endregion
}