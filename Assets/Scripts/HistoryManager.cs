
using System.Collections.Generic;
using UnityEngine;

public class HistoryManager
{
    public readonly Stack<RoundHistory> HistoryUndo = new Stack<RoundHistory>();
    public Stack<RoundHistory> HistoryRedo = new Stack<RoundHistory>();

    public void AddHistory(RoundHistory rh)
    {
        HistoryUndo.Push(rh);
        HistoryRedo = new Stack<RoundHistory>();
    }

    public RoundHistory Undo()
    {
        if (HistoryUndo.Count > 0)
        {
            RoundHistory rh = HistoryUndo.Pop();
            HistoryRedo.Push(rh);
            return rh;
        }
        return null;
    }

    public RoundHistory Redo()
    {
        if (HistoryRedo.Count > 0)
        {
            RoundHistory rh = HistoryRedo.Pop();
            HistoryUndo.Push(rh);
            return rh;
        }
        return null;
    }
}
