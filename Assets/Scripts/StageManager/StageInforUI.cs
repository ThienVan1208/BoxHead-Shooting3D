using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageInforUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _curScoreTxt, _curStageTxt;
    [SerializeField] private IntEventChannelSO _updateScoreEventSO, _updateStageEventSO;
    private void Awake() {
        _curScoreTxt.text = "0";
    }
    private void OnEnable() {
        _updateScoreEventSO.OnRaisedEvent += UpdateScore;
        _updateStageEventSO.OnRaisedEvent += UpdateStage;
    }
    private void OnDisable() {
        _updateScoreEventSO.OnRaisedEvent -= UpdateScore;
        _updateStageEventSO.OnRaisedEvent -= UpdateStage;
    }
    private void UpdateScore(int amount){
        _curScoreTxt.text = (int.Parse(_curScoreTxt.text) + amount).ToString();
    }
    private void UpdateStage(int amount){
        _curStageTxt.text = amount.ToString();
    }
}
