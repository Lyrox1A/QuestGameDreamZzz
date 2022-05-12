using System;

using UnityEngine;

public class QuestEntry : MonoBehaviour
{
    [SerializeField] private GameObject checkmark;

    private void Awake()
    {
        SetQuestStatus(false);
    }

    public void SetQuestStatus(bool fulfilled)
    {
        checkmark.SetActive(fulfilled);
    }
}
