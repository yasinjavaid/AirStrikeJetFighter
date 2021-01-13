using UnityEngine;
using System.Collections;
using System;
using SimpleJSON;


public class CALogManager
{
    private static CALogManager _instance;
    private bool logEnabled = false;
	private bool isDebug = false;

	public enum LogType
	{
		INFO = 1,
		WARNING = 2,
		ERROR = 3,
		DEV = 4
	}
    //------------------------------------------------------------------------------
    private CALogManager() { }
    //------------------------------------------------------------------------------
    public static CALogManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CALogManager();
            }
            return _instance;
        }
    }

    public void EnableLog(bool value)
    {
        logEnabled = value;
    }


    public bool IsLogEnabled()
    {
		return logEnabled;
    }

    public void Log(LogType type, string className, string methodName, string message, string value="")
    {
		if (IsLogEnabled())
		{
			switch (type)
			{
				case LogType.INFO:
					Debug.Log(this.GetType().Name+ " - " + className+ "(" + methodName+ "): " + message+ " , " + value);
					break;
				case LogType.WARNING:
					Debug.LogWarning(this.GetType().Name + " - " + className + "(" + methodName + "): " + message + " , " + value);
					break;
				case LogType.ERROR:
					Debug.LogError(this.GetType().Name + " - " + className + "(" + methodName + "): " + message + " , " + value);
					break;
				case LogType.DEV:
					if (isDebug)
					{
						Debug.Log(this.GetType().Name + " - " + className + "(" + methodName + "): " + message + " , " + value);
					}
					break;
			}
        }
    }



}

