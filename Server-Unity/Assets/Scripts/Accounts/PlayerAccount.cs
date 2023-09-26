using System;
using System.Collections.Generic;

public struct PlayerAccount
{
    public long id;
    public List<long> longTermAuthTokens;
    public List<long> shortTermAuthTokens;

    public string username;
    public string password;
    public string emailAddress;

    public void Init()
    {
        id = -1;
        longTermAuthTokens = new();
        shortTermAuthTokens = new();

        username = "DEFAULT_USERNAME";
        password = "DEFAULT_PASSWORD";
        emailAddress = "DEFAULT_EMAIL";
    }
}

public struct PlayerAccountBlock
{
    public List<PlayerAccount> accounts;

    public void Init()
    {
        accounts = new();
    }
}