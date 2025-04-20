using UnityEngine;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    private Dictionary<string, bool> flags = new Dictionary<string, bool>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetFlag(string key, bool value) => flags[key] = value;

    public bool CheckFlag(string key)
    {
        return flags.TryGetValue(key, out bool value) && value;
    }
}