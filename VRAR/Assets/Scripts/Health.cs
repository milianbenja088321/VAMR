using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{
    private const float maxHealth = 100.0f;
    private bool isAlive { get; set; }

    [SerializeField] private GameObject self;

    // currentHealth linear change in network
    // needs to be synced on Awake()
    [SyncVar(hook = "OnChangeHealth")]
    [SerializeField] private int currentHealth = (int)maxHealth;

    private void Start()
    {
        isAlive = true;
    }

    public void TakeDamage(int _amount)
    {
        if (isServer == false) // server only allowed to change game state
            return;

        currentHealth -= _amount;

        if(currentHealth <= 0)
        {
            // player is dead do we need to respawn?
            RpcSetActive();
        }
    }

    private void OnChangeHealth(int _health)
    {
        // this will get called everytime the currentHealth
        // variable is changed
        Debug.Log("Player took a hit of " + _health);
        
    }

    /// <summary>
    /// 
    /// </summary>
    [Command]
    void CmdSetActive()
    {
        RpcSetActive();
    }

    /// <summary>
    /// 
    /// </summary>
    [ClientRpc]
    private void RpcSetActive()
    {
        self.SetActive(false);
    }
}
