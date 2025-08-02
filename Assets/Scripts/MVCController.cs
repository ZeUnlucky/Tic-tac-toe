using UnityEngine;

// MVC Model
// Controls the flow
public class MVCController : MonoBehaviour
{
    // when theres an input on a space logic, occupy it.
    public void OnInput(SpaceLogic logic)
    {
        if (GameManager.Instance.IsPlaying)
            logic.Occupy(GameManager.Instance.CurrentTeam);
    }

    // when a logic is supposed to go through an undo
    public void Undo(SpaceLogic logic)
    {
        logic.TeamOccupying = Team.None;
        GameManager.Instance.CurrentTeam = GameManager.Instance.CurrentTeam == Team.X ? Team.O : Team.X;    
    }

    // checks if an occupy took place, if so, goes to game manager to continue logic
    public void OnLogicOccupy(SpaceLogic logic, bool addToHistory = true)
    {
        GameManager.Instance.DoTeamTurn(logic, addToHistory);
    }

    // checks for button click on undo button, if so, execute undo logic
    public void UndoPressed()
    {
        RoundHistory rh = GameManager.Instance.History.Undo();
        if (rh != null)
        {
            SpaceLogic logic = GameManager.Instance.GetLogicFromPosition(rh.GetPosition());
            logic.Occupy(Team.None, true);
        }
    }

    // checks for button click on redo button, if so, execute redo logic
    public void RedoPressed()
    {
        RoundHistory rh = GameManager.Instance.History.Redo();
        if (rh != null)
        {
            SpaceLogic logic = GameManager.Instance.GetLogicFromPosition(rh.GetPosition());
            logic.Occupy(rh.GetTeam(), true);
        }
    }
}
