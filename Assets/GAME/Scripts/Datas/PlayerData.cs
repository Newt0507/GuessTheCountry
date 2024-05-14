using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

public static class PlayerData
{
    //Stage+Level
    private const string LEVEL = "Level";
    private const string STAGE = "Stage";
    private const string LAST_STAGE = "LastStage";
    private const string PASSED_STAGE = "PassedStage";
    private const string STAGE_DATA = "StageData";

    //Rank
    private const string RANK = "Rank";
    private const int DEFAULT_RANK = 80000;
        
    //Camera
    private const string CAMERA_POSITION = "CameraPosition";
    private const string CAMERA_SIZE = "CameraSize";
    
    //Score+Hint
    private const string SCORE = "Score";
    private const string HINT = "Hint";

    //Setting
    private const string VIBRATION = "Vibration";
    private const string SOUND = "Sound";

    #region ResetData

    public static void ResetData()
    {
        PlayerPrefs.SetInt(LEVEL, 0);
        PlayerPrefs.SetInt(STAGE, 0);
        PlayerPrefs.SetInt(LAST_STAGE, -1);
        PlayerPrefs.SetInt(PASSED_STAGE, 0);
        PlayerPrefs.SetInt(SCORE, 0);
        PlayerPrefs.SetInt(HINT, 3);
        PlayerPrefs.SetInt(RANK, DEFAULT_RANK);
        SetDefaultCamera();
        ResetPassedStage();
        PlayerPrefs.Save();
    }

    #endregion
    
    #region Stage+Level

    public static void UpdateStage()
    {
        var currentStage = GetStage();
        var currentLevel = GetLevel();

        SetLastStage(currentStage);
        
        if (currentStage < currentLevel)
        {
            var currentStageIndex = -1;

            for (var i = 0; i < GetStageData().Count; i++)
            {
                if (currentStage == GetStageData()[i])
                    currentStageIndex = i;
            }

            var nextStage = GetStageData()[currentStageIndex + 1];
            SetStage(nextStage);
            SetPassedStage();
        }
        else
        {
            var nextLevel = currentLevel + 1;

            if (nextLevel + 1 > 100)
            {
                nextLevel = 0;
                SetDefaultCamera();
            }
            ResetPassedStage();
            SetLevel(nextLevel);
        }

        if (GetLevel() != 0)
        {
            SaveCameraPosition(CameraController.Instance.TargetTransform.position);
            SaveCameraSize(CameraController.Instance.CamSizeBeforeChoose);
        }
        
        PlayerPrefs.Save();
    }

    public static int GetLevel()
    {
        return PlayerPrefs.GetInt(LEVEL);
    }

    private static void SetLevel(int level)
    {
        PlayerPrefs.SetInt(LEVEL, level);
        SetStageData(level);
        PlayerPrefs.Save();
    }

    public static int GetLastStage()
    {
        return PlayerPrefs.GetInt(LAST_STAGE, -1);
    }
    
    private static void SetLastStage(int lastStage)
    {
        PlayerPrefs.SetInt(LAST_STAGE, lastStage);
        PlayerPrefs.Save();
    }
    
    public static int GetStage()
    {
        return PlayerPrefs.GetInt(STAGE);
    }

    private static void SetStage(int nextStage)
    {
        PlayerPrefs.SetInt(STAGE, nextStage);
        PlayerPrefs.Save();
    }

    public static void ResetStageOfCurrentLevel()
    {
        var nextStage = GetStageData()[0];
        SetStage(nextStage);
        SetPassedStage();
    }
    
    public static void ResetPassedStage()
    {
        PlayerPrefs.SetInt(PASSED_STAGE, 0);
        PlayerPrefs.Save();
    }

    public static int GetPassedStage()
    {
        return PlayerPrefs.GetInt(PASSED_STAGE, 0);
    }

    private static void SetPassedStage()
    {
        var passedStage = GetPassedStage();
        passedStage++;

        PlayerPrefs.SetInt(PASSED_STAGE, passedStage);
        PlayerPrefs.Save();
    }

    public static List<int> GetStageData()
    {
        if (GetLevel() == 0) return null;

        var stringData = PlayerPrefs.GetString(STAGE_DATA);
        var dataArray = stringData.Split(',');
        var stageData = new List<int>();

        foreach (var data in dataArray)
        {
            if (int.TryParse(data, out var intValue))
                stageData.Add(intValue);
        }

        return stageData;
        //return JsonUtility.FromJson<List<int>>(stageData);
    }

    private static void SetStageData(int level)
    {
        var data = new List<int>();

        var maxRange = level - 1;
        if ((level + 1) % 10 == 0)
        {
            int firstStage, secondStage;
            var minRange = level - 9;
            do
            {
                firstStage = Random.Range(minRange, maxRange);
                secondStage = Random.Range(minRange, maxRange);
            } 
            while (firstStage > secondStage);
            
            data.Add(firstStage);
            data.Add(secondStage);
        }
        else if((level + 1) % 5 == 0)
        {
            var minRange = level - 4;
            var stage = Random.Range(minRange, maxRange);
            data.Add(stage);
        }
        
        data.Add(level);
        SetStage(data[0]);
        
        var stageData = string.Join(",", data);
        
        PlayerPrefs.SetString(STAGE_DATA, stageData);

        //var stageData = JsonUtility.ToJson(stages);
        //PlayerPrefs.SetString(STAGE_DATA, stageData);
        PlayerPrefs.Save();
    }
    
    #endregion

    #region Rank
    
    public static int GetRank()
    {
        return PlayerPrefs.GetInt(RANK, DEFAULT_RANK);
    }

    public static void SetRank()
    {
        int rank = GetRank() - 7 * (GetLevel() + 1);
        
        PlayerPrefs.SetInt(RANK, rank);
        PlayerPrefs.Save();
    }

    #endregion
    
    #region Camera

    public static void SetDefaultCamera()
    {
        SaveCameraPosition(new Vector3(0, 0, -10));
        SaveCameraSize(10);
        PlayerPrefs.Save();
    }
    
    public static float GetCameraSize()
    {
        if (GetLevel() == 0 && GetLastStage() + 1 == 1) return 4;
        return PlayerPrefs.GetFloat(CAMERA_SIZE);
    }

    private static void SaveCameraSize(float size)
    {
        PlayerPrefs.SetFloat(CAMERA_SIZE, size);
        PlayerPrefs.Save();
    }

    public static Vector3 GetCameraPosition()
    {
        if (GetLevel() == 0) return new Vector3(0, 0, -10);

        var posData = PlayerPrefs.GetString(CAMERA_POSITION);
        return JsonUtility.FromJson<Vector3>(posData);
    }

    private static void SaveCameraPosition(Vector3 position)
    {
        var posData = JsonUtility.ToJson(position);
        PlayerPrefs.SetString(CAMERA_POSITION, posData);
        PlayerPrefs.Save();
    }

    #endregion

    #region Score+Hint

    public static int GetScore()
    {
        return PlayerPrefs.GetInt(SCORE);
    }

    public static void SetScore()
    {
        int score = GetScore() + 1;
        if (score >= 9999) score = 9999;
        
        PlayerPrefs.SetInt(SCORE, score);
        //SetRank();        
        PlayerPrefs.Save();
    }

    public static int GetHint()
    {
        return PlayerPrefs.GetInt(HINT, 3);
    }

    public static void SetHint(int number)
    {
        int hint = GetHint() + number;

        if (hint >= 99) hint = 99;
        if (hint <= 0) hint = 0;

        PlayerPrefs.SetInt(HINT, hint);
        PlayerPrefs.Save();
    }

    #endregion

    #region Setting
    
    public static bool IsVibrationMute()
    {
        return PlayerPrefs.GetInt(VIBRATION, 0) != 0;
    }

    public static void SetVibration(bool on)
    {
        PlayerPrefs.SetInt(VIBRATION, (on ? 1 : 0));
        PlayerPrefs.Save();
    }

    public static bool IsSoundMuted()
    {
        return PlayerPrefs.GetInt(SOUND, 0) != 0;
    }

    public static void SetSound(bool on)
    {
        PlayerPrefs.SetInt(SOUND, (on ? 1 : 0));
        PlayerPrefs.Save();
    }

    #endregion
}