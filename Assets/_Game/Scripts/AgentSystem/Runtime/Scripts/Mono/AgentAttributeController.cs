// using AgentSystem;
// using ReplenishableSystem.HealthSystem;
//
// public class AgentAttributeController
// {
//     Agent agent;
//     public Health Health { get; private set; }
//
//     public AgentAttributeController(Agent agent)
//     {
//         if (this.agent != null)
//             RemoveRobotListeners();
//         this.agent = agent;
//         if(Health==null)
//             Health = agent.gameObject.AddComponent<Health>();
//         AddRobotListeners();
//         SetHealth(agent.CurrentAgentAttributes.Health);
//     }
//
//     void AddRobotListeners()
//     {
//         agent.OnAgentBaseHealthChanged += SetHealth;
//     }
//
//     void RemoveRobotListeners()
//     {
//         agent.OnAgentBaseHealthChanged -= SetHealth;
//     }
//
//     private void SetHealth(float health)
//     {
//         Health.SetMaxValue(health);
//         Health.SetReplenishableValue(health);
//     }
// }