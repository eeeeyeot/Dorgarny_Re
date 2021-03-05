using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour
{
	public EnemyStats enemy;
	public PlayerStats player;
	public GameObject particleGameObject;
	ParticleSystem[] _particleSystems;

	private void Start()
	{
		_particleSystems = new ParticleSystem[particleGameObject.transform.childCount];
		_particleSystems = particleGameObject.GetComponentsInChildren<ParticleSystem>();
		Debug.Log(_particleSystems.Length);
	}

	void OnTriggerEnter(Collider other)
	{
		if ((enemy = other.GetComponent<EnemyStats>()) == null)
			return;

		if (other.gameObject.tag == "Enemy")
		{
			enemy.TakeDamage(player.GetDamage());
			for (int i = 0; i < _particleSystems.Length; i++)
			{
				_particleSystems[i].gameObject.SetActive(true);
				_particleSystems[i].Play();
			}
			_particleSystems[0].gameObject.GetComponent<AudioSource>().Play();
		}
	}

}
