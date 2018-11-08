using UnityEngine;

public class Module : MonoBehaviour
{
	public string[] Tags;

	public ModuleConnector[] GetExits()
	{
		return GetComponentsInChildren<ModuleConnector>();
	}

	public EnemyConnector[] GetEnemies() {
		return GetComponentsInChildren<EnemyConnector>();
	}
	public PlayerConnector[] getPlayer() {
		return GetComponentsInChildren<PlayerConnector>();
	}
}
