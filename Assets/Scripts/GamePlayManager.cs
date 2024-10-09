using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> ToShootGameobjects;

    // define the delegate 
    public delegate void ScoreUpdate(int score);

    // event to notify score updates
    public static event ScoreUpdate OnScoreUpdate;

    float intervalTime = 1.5f;
    private int indexCount = 0;
    private void Start()
    {
        StartCoroutine(SpawnTarget());
    }

    IEnumerator SpawnTarget()
    {
        // Limit how many objects you can activate based on list count
        while (indexCount < ToShootGameobjects.Count)
        {
            yield return new WaitForSeconds(intervalTime);

            int index = Random.Range(0, ToShootGameobjects.Count);

            if (!ToShootGameobjects[index].activeInHierarchy && ToShootGameobjects[index] != null)
            {
                ToShootGameobjects[index].SetActive(true);
                indexCount++;
                //ToShootGameobjects.Remove(ToShootGameobjects[index]);
            }
        }
    }
}
