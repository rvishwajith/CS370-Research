using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountManager : MonoBehaviour
{
    [SerializeField] private TextAsset[] playerAccountFiles;

    public Dictionary<long, PlayerAccount> idToAccount = new();
    public Dictionary<string, long> usernameToId = new();

    private void Start()
    {
        foreach (var account in GetFileAccounts())
            TryRegister(account);
    }

    private List<PlayerAccount> GetFileAccounts()
    {
        List<PlayerAccount> fileAccounts = new();
        if (playerAccountFiles == null)
            return fileAccounts;
        foreach (var file in playerAccountFiles)
        {
            if (file != null)
                fileAccounts.AddRange(JsonUtility.FromJson<List<PlayerAccount>>(file.text));
        }
        return fileAccounts;
    }

    private bool TryRegister(PlayerAccount account)
    {
        if (account.id == -1)
        {
            Debug.Log("AccountManager: Failed to register ID " + account.id + " (no id)");
            return false;
        }
        else if (idToAccount.ContainsKey(account.id))
        {
            Debug.Log("AccountManager: Failed to register ID " + account.id + " (clone id)");
            return false;
        }
        idToAccount[account.id] = account;
        Debug.Log("AccountManager: Registered new ID " + account.id);
        return true;
    }
}