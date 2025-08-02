using UnityEngine;

public class SpaceLogic : MonoBehaviour
{
    [HideInInspector] public Team TeamOccupying;
    [SerializeField] GamePrefabStore prefabs;
    [SerializeField] MVCController controller;
    private GameObject occupyingGO;

    // checks if the current occupier is empty, if it is, occupy it and create a game object, if it isn't, it checks
    // if new occupant is supposed to make it empty, if so, undo
    public void Occupy(Team team, bool isRedo = false)
    {
        if (TeamOccupying == Team.None)
        {
            TeamOccupying = team;
            occupyingGO = Instantiate(TeamOccupying == Team.X ? prefabs.xObject : prefabs.oObject, transform.position, Quaternion.identity);  
            controller.OnLogicOccupy(this, !isRedo);
        }
        else if (team == Team.None)
            Undo();
    }

    //goes to the controller and does undo, and removes the gameobject on this space, if there is one
    private void Undo()
    {
        controller.Undo(this);
        if (occupyingGO != null)
            Destroy(occupyingGO);
    }

    //handles mouse click on spot
    private void OnMouseDown()
    {
        controller.OnInput(this);
    }
}
