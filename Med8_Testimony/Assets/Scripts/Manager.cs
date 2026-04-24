using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public List<NPC_Animation> npcs = new List<NPC_Animation>();
    public bool npcIsWalking;
    public bool npcWatch;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {  
        foreach (var npc in npcs)
        {
            npc.isWalking = npcIsWalking;
            npc.watched = npcWatch;
        }
    }
}
