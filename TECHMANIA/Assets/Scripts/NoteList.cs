using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A custom data structure for storing notes in Game.
// - Elements are set once, and then never removed.
// - Each element can be active or inactive. Initially all elements
//   are active. Removing an element sets it as inactive, instead
//   of actually removing, this way we can easily reset the NoteList
//   to the initial state.
// - A cursor always points to the first active element.
public class NoteList
{
    private Dictionary<int, NoteObject> list;
    private Dictionary<int, bool> active;
    private int first;
    private int count;
    public int Count { get { return count; } }

    #region Initialize
    public NoteList()
    {
        list = new Dictionary<int, NoteObject>();
        active = new Dictionary<int, bool>();
        count = 0;
        first = 0;
    }

    public void Add(NoteObject n)
    {
        list.Add(list.Count, n);
        active.Add(active.Count, true);
        count++;
    }

    // After calling this, we assume notes are sorted by pulse.
    public void Reverse()
    {
        Dictionary<int, NoteObject> newList = new Dictionary<int, NoteObject>();
        for (int i = 0; i < count; i++)
        {
            newList.Add(i, list[count - i - 1]);
        }
        list = newList;
    }
    #endregion

    public void Remove(NoteObject n)
    {
        KeyValuePair<int, NoteObject> match = list.Where(e => e.Value == n).FirstOrDefault();
        if (null != match.Value)
        {
            int k = match.Key;
            active[match.Key] = false;
            count--;
            if (k == first)
            {
                do
                    { first++; }
                while (first < list.Count && !active[first]);
            }
        }
    }

    // For notes whose pulse is equal to pulseThreshold, they will
    // be removed if end-of-scan, and vice versa.
    public void RemoveUpTo(int pulseThreshold)
    {
        for (int i = 0; i < list.Count; i++)
        {
            bool beforeThreshold;
            if (list[i].note.pulse < pulseThreshold)
            {
                beforeThreshold = true;
            }
            else if (list[i].note.pulse == pulseThreshold)
            {
                beforeThreshold = list[i].note.endOfScan;
            }
            else
            {
                beforeThreshold = false;
            }

            if (beforeThreshold && active[i])
            {
                active[i] = false;
                count--;
            }
            if (!beforeThreshold)
            {
                first = i;
                return;
            }
        }
        first = list.Count;
    }

    public NoteObject First()
    {
        try
        {
            return list[first];
        }
        catch (KeyNotFoundException)
        {
            throw new System.Exception("Attempting to get first element from an empty NoteList.");
        }
    }

    public void Reset()
    {
        count = list.Count;
        for (int i = 0; i < Count; i++) active[i] = true;
    }

    // This applies to inactive elements too.
    public void ForEach(Action<NoteObject> action)
    {
        foreach (NoteObject e in list.Values)
        {
            action(e);
        }
    }
}
