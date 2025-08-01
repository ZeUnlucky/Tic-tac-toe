using UnityEngine;

public class UndoManager : MonoBehaviour
{
    [SerializeField] MVCController controller;
    public void PressUndo()
    {
        controller.UndoPressed();
    }

    public void PressRedo()
    {
        controller.RedoPressed();
    }
}
