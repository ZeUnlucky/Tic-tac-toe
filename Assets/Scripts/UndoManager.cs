using UnityEngine;

public class UndoManager : MonoBehaviour
{
    [SerializeField] MVCController controller;

    //goes to the controller and does undo logic from undo manager side
    public void PressUndo()
    {
        controller.UndoPressed();
    }
    //goes to the controller and does redo logic from undo manager side
    public void PressRedo()
    {
        controller.RedoPressed();
    }
}
