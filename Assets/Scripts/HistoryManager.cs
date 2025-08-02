
using System.Collections.Generic;

public class HistoryManager
{
    public readonly Stack<RoundHistory> HistoryUndo = new Stack<RoundHistory>();
    public Stack<RoundHistory> HistoryRedo = new Stack<RoundHistory>();

    // adds to the undo history, if a new move has been done, and deletes the redo history to reflect new move
    public void AddHistory(RoundHistory rh)
    {
        HistoryUndo.Push(rh);
        HistoryRedo = new Stack<RoundHistory>();
    }

    // pushes to redo history, pops from undo
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

    // pushes to undo history, pops from redo
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
