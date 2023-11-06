using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class Lvl
{
    [Range(1, 11)]
    public int partCount = 11;

    [Range(0, 11)]
    public int deathPartCount = 1;
}

[CreateAssetMenu(fileName ="NewStage")]
public class StageController : ScriptableObject
{
    public Color stageBackgroundColor = Color.white;
    public Color stageLvlPartColor = Color.white;
    public Color stageBallColor = Color.white;
    public List<Lvl> lvl = new List<Lvl>();
}
