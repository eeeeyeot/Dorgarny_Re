using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class SpawnPoint
{
    public string enemyType;
    public Transform point;
}

public class EnemySpawner : MonoBehaviour
{
   
    public List<SpawnPoint> spawnPoints;
    private EnemyPooler enemyPooler;
    private bool isEntered = false;

    private void Start()
    {
        enemyPooler = EnemyPooler.Instance;
    }

	//Trigger => 몬스터 SpawnPoint에 스폰
	private void OnTriggerEnter(Collider other)
	{
		if (!isEntered)
		{
			if (other.gameObject.tag == "MainPlayer")
			{
				if (enemyPooler.checkAllDead())
				{
					foreach (Transform child in this.transform)
					{
						if (child.gameObject.tag == "CheckAllDead")
						{
							child.GetComponent<NavMeshObstacle>().enabled = false;
						}
					}
					enemyPooler.GetPooledObject(spawnPoints);
					isEntered = true;
				}
			}
		}
	}
}
