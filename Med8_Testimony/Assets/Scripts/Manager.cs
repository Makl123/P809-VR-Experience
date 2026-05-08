using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // For NPC_Animation Script
    public List<NPC_Animation> npcs = new List<NPC_Animation>();
    public bool npcIsWalking;
    public bool npcWatch;

    // For EventTrigger Script
    public List<EventTrigger> events = new List<EventTrigger>();

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {  
        //foreach (var npc in npcs)
        //{
            //npc.isWalking = npcIsWalking;
            //npc.watched = npcWatch;
        //}
        CheckEventTriggers();
    }

    void CheckEventTriggers()
    {
        // Example: Check if the first event trigger is active
        if (events.Count > 0 && events[0].Event)
        {
            // Do something with a specific NPC, e.g., the first one
            if (npcs.Count > 0)
            {
                // Example action: make NPC start walking
                npcs[0].watched = true;
                npcs[1].watched = true;
                npcs[2].watched = true;
            }
        }
        else
        {
            // If the event is not active, make the NPC stop walking
            if (npcs.Count > 0)
            {
                npcs[0].watched = false;
                npcs[1].watched = false;
                npcs[2].watched = false;
            }
        }

        if (events.Count > 1 && events[1].Event)
        {
            if (npcs.Count > 0)
            {
                npcs[3].Animation = 3;
                npcs[3].isWalking = true;
                npcs[3].watched = true;
                PlayNpcAudio(3);
            }
        }

        if (events.Count > 2 && events[2].Event)
        {
            if (npcs.Count > 0)
            {
                npcs[3].Animation = 4;
                npcs[3].isWalking = false;
                PlayNpcAudio(5);
            }
        }

        if (events.Count > 3 && events[3].Event)
        {
            if (npcs.Count > 0)
            {
                npcs[4].isWalking = true;
                SetNpcActive(4, true);
            }
        }

        if (events.Count > 4 && events[4].Event)
        {
            if (npcs.Count > 0)
            {
                npcs[6].isWalking = false;
                npcs[6].Animation = 0;
            }
        }
    }

    public void SetNpcActive(int index, bool active)
    {
        if (index < 0 || index >= npcs.Count) return;
        var npc = npcs[index];
        if (npc == null) return;

        npc.gameObject.SetActive(active);
    }

    public void PlayNpcAudio(int index)
    {
        if (index < 0 || index >= npcs.Count) return;
        var npc = npcs[index];
        if (npc == null) return;

        var audio = npc.GetComponent<AudioSource>();
        if (audio == null) return;

        // Ensure component is enabled and play
        if (!audio.enabled) audio.enabled = true;
        // LSLMarkerSender.Instance.SendMarker($"Playing audio for NPC {index}");
        // for later    
    }

    public void StopNpcAudio(int index)
    {
        if (index < 0 || index >= npcs.Count) return;
        var npc = npcs[index];
        if (npc == null) return;

        var audio = npc.GetComponent<AudioSource>();
        if (audio == null) return;

        audio.Stop();
    }
}
