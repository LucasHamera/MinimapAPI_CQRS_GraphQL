using System;

namespace MinimapAPIDemo.Core.Identity;

public class User
{
    private User()
    {

    }

    public User(string login, string password)
    {
        Login = login;
        Password = password;
    }

    public string Login { get; private set; }

    public string Password { get; private set; }
}