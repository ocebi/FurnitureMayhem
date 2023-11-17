using Cysharp.Threading.Tasks;
using Unity.Collections;
using UnityEngine;

namespace StatusSystem
{
    public abstract class Status : ScriptableObject
    {
        public StatusController Owner { get; set; }
        [ReadOnly] public int EffectCount;
        public abstract void ActivateStatus();
        public virtual void ReActivateStatus() { }
        public abstract void DeActivateStatus();
        public virtual void Initialize(StatusController statusController) { Owner = statusController; }
        bool isAtBreak;

        public Sprite StatusImage;

        public async void GiveABreakAsync(float time)
        {
            await GiveABreakTask(time);
        }

        async UniTask GiveABreakTask(float time)
        {
            if (isAtBreak) return;
            DeActivateStatus();
            isAtBreak = true;
            await UniTask.Delay((int)(time * 1000));
            if (Owner == null)
                Debug.Log("No Owner");
            else
                ActivateStatus();
            isAtBreak = false;

        }

        public virtual void OnUpdate(){}
    }
}