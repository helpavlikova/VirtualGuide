using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings  {
    private static string path;

    public static string Path
    {
        get
        {
            return path;
        }
        set
        {
            path = value;
        }
    }
}
