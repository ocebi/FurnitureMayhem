// namespace AISystem
// {
//     public class AIInputListener : InputListener
//     {
//         public override void SetInputs(Controller controller)
//         {
//             var AI = (AI)controller;
//             AI.OnMoving += CallOnMove;
//             AI.OnMoveRelased += CallOnMoveReleased;
//             //AI.OnWeaponAiming += CallOnWeaponAiming;
//             //AI.OnWeaponReleased += CallOnWeaponReleased;
//             //AI.OnSkillUsed += CallOnSkillUsed;
//         }
//
//         public override void RemoveInputs(Controller controller)
//         {
//             var AI = (AI)controller;
//             AI.OnMoving -= CallOnMove;
//             AI.OnMoveRelased -= CallOnMoveReleased;
//             //AI.OnWeaponAiming -= CallOnWeaponAiming;
//             //AI.OnWeaponReleased -= CallOnWeaponReleased;
//             //AI.OnSkillUsed -= CallOnSkillUsed;
//         }
//     }
// }