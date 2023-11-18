using AgentSystem;
using StateSystem;
using UnityEngine;

public class PlayerTransitionState : State
{
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        
        RaycastHit hitInfo = new RaycastHit();

        var cam = CameraManager.Instance.Camera;
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        // Debug.DrawRay(ray.origin, (ray.direction) * 50f, Color.red, 5f);
        if (Physics.Raycast(ray, out hitInfo, 100f, LayerMask.GetMask("Agent")) && 
            hitInfo.transform.TryGetComponent<AgentController>(out var agentController))
        {
            PlayerManager.Instance.SetPlayerTarget(agentController);
        }
        else if (Physics.Raycast(ray, out hitInfo, 100f, LayerMask.GetMask("MysteryBox")) && 
                 hitInfo.transform.TryGetComponent<MysteryBox>(out var mysteryBox))
        {
            PlayerManager.Instance.SetPlayerTarget(mysteryBox.OpenBox().GetComponent<AgentController>());
        }
        else
        {
            GameStateManager.Instance.StateMachine.SetNewState(nameof(GameplayState));
        }
    }
}