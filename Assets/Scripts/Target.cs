using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static GamePlayManager;

public class Target : MonoBehaviour
{
    [Header("Player reference")]
    [SerializeField] Transform player;

    [Header("Scriptable object reference")]
    public TargetData targetData;
    public Color color;

    private MeshRenderer meshRenderer;
    public NavMeshAgent enemy;


    private void Start()
    {
        // Dynamically find the player by tag if it isn't assigned
        if (player == null)
        {
            player = GameObject.Find("Player").transform;
        }

        // Ensure player was found
        if (player == null)
        {
            Debug.LogError("Player reference is missing or not set!");
            return;  // Prevent any further action if player is missing
        }

        meshRenderer = GetComponent<MeshRenderer>();
        Debug.Log("Color changed");
        meshRenderer.material.color = targetData.targetColor;
    }
    private void OnEnable()
    {
        // Start chasing the player when the enemy becomes active
        if (player != null)
        {
            enemy.SetDestination(player.position);
        }
    }
    private void Update()
    {
        if (player != null)
        {
            enemy.SetDestination(player.position);
        }
    }
}
