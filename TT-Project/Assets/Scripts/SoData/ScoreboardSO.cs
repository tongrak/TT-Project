using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScoreboardSO : ScriptableObject
{
    [SerializeField] public string scoreboardHeader;
    [SerializeField] public string gameTypeTable;
    [SerializeField] public IntSO bestScoreSO;
}
