using UnityEngine;
using System;
using System.Collections;

public class Utils : MonoBehaviour
{
    /// <summary>
    /// The Wait method is responsible for waiting for a specific time.
    /// </summary>
    /// <param name="time">The time to wait (seconds).</param>
    /// <param name="action">Action to execute after waiting.</param>
    /// <returns></returns>
    public static IEnumerator Wait(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }

    /// <summary>
    /// The IsPlayerAlive method is responsible for checking if the player is alive.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if the player is alive otherwise, <c>false</c>.
    /// </returns>
    public static bool IsPlayerAlive()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        return player.GetComponent<Player>().entityFSM.entitycurrentHealth > 0;
    }

    /// <summary>
    /// The  GetUnitaryVector method is responsible for getting a unitary vector from a direction vector.
    /// </summary>
    /// <returns>A unitary vector </returns>
    public static Vector2 GetUnitaryVector(Vector2 directionVector)
    {
        float xDirection = Mathf.Round(directionVector.x);
        float yDirection = Mathf.Round(directionVector.y);

        return new Vector2(xDirection, yDirection);
    }
}
