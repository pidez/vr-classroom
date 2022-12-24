using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

[System.Serializable]
public class User {
    public string username;
    public string password;
}

[System.Serializable]
public class UserCollection {
    public User[] users;
}


public class AuthManager : MonoBehaviour
{
    private UserCollection userCollection;
    public static AuthManager Instance {get; private set;}

    void Awake() {
        //Singleton pattern
        if(Instance != null && Instance != this) {
            Destroy(this);
            return;
        }
        Instance = this;
        GetAuthorizedUsers();
    }

    public bool AuthenticateAsTeacher(string username, string password) {
        if(userCollection != null) {
            foreach(User user in userCollection.users) {
                if(string.Compare(user.username, username) == 0 && string.Compare(user.password, password) == 0) {
                    Debug.Log("AuthManager: Authentication successful.");
                    return true;
                }
            }
            Debug.Log("AuthManager: Authentication failed.");
            return false;
        }
        else {
            Debug.Log("AuthManager: User list is null.");
            return false;
        }
    }

    #region private methods

    private void GetAuthorizedUsers() {
        string filename = "Assets/Auth/auth.json";
        string jsonString = File.ReadAllText(filename);
        userCollection = JsonUtility.FromJson<UserCollection>(jsonString);
    }


    #endregion
}
