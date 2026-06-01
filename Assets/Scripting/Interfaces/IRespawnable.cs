using System.Collections;
using UnityEngine;

public interface IRespawnable
{
    void Respawn(Vector3 respawnPos);
    IEnumerator RespawnCoroutine(Vector3 respawnPos, float seconds);
}