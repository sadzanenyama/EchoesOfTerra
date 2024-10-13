using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueLevelXX", menuName = "Dialogue")]
public class LevelDialogueSO : ScriptableObject
{
    public Message[] messages;
}

[System.Serializable]
public class Message
{
    public string messengerName;
    public Sprite messengerSprite;

    [TextArea(3, 5)]
    public string[] dialogue;
}
