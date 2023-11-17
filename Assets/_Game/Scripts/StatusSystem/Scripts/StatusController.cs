using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;


namespace StatusSystem
{

    public class StatusController : MonoBehaviour
    {
        [ReadOnly] public List<Status> StatusList=new List<Status>();
        [ReadOnly] public List<Status> UniqueStatusList=new List<Status>();
        public Action<Status> OnStatusAdded;
        public Action<Status> OnStatusRemoved;


        private void Update()
        {
            foreach (var status in UniqueStatusList)
            {
                status.OnUpdate();
            }
        }

        public virtual void AddStatus(Status status)
        {
            Status uniqueStatus = GetUniqueStatus(status); 
            if (uniqueStatus==null)
            {
                uniqueStatus = Instantiate(status);
                //uniqueStatus.Owner = this;
                uniqueStatus.Initialize(this);
                UniqueStatusList.Add(uniqueStatus);
                uniqueStatus.ActivateStatus();
            }
            else
            {
                uniqueStatus.ReActivateStatus();
            }
            OnStatusAdded?.Invoke(status);
            StatusList.Add(status);
            uniqueStatus.EffectCount++;
        }

        public virtual void AddStatus(Status status, float duration)
        {
            AddStatus(status);
            StartCoroutine(IEAddStatus(status, duration));
        }

        IEnumerator IEAddStatus(Status status, float duration)
        {
            yield return new WaitForSeconds(duration);
            RemoveStatus(status);
        }

        public virtual void RemoveStatus(Status status)
        {
            Status uniqueStatus = GetUniqueStatus(status);
            if (uniqueStatus != null)
            {
                StatusList.Remove(status);
                uniqueStatus.EffectCount--;
                if (!StatusList.Contains(status))
                {
                    OnStatusRemoved?.Invoke(status);
                    uniqueStatus.DeActivateStatus();
                    uniqueStatus.Owner = null;
                    UniqueStatusList.Remove(uniqueStatus);
                    Destroy(uniqueStatus);
                }
            }
        }

        public void RemoveAllStatusWithType(Status status)
        {
            var count = status.EffectCount;
            for (int i = 0; i < count; i++)
            {
                RemoveStatus(status);
            }
            //var statuses = UniqueStatusList.FindAll(s => s.GetType() == status.GetType());
        }

        public void BreakStatus(Status status,float breakTime)
        {
            Debug.Log("Inside break status");
            Status uniqueStatus = GetUniqueStatus(status);
            if (uniqueStatus != null)
            {
                    uniqueStatus.GiveABreakAsync(breakTime);
            }
        }

        Status GetUniqueStatus(Status status)
        {
            return UniqueStatusList.Find(s => s.GetType() == status.GetType());
        }

        public bool DoesStatusExist(Status status)
        {
            return UniqueStatusList.Find(s => s.GetType() == status.GetType());
        }

        public int GetNumberOfStatus(Status status)
        {
            var list = StatusList.Where(s => s.GetType() == status.GetType()).ToList();
            return list.Count;
        }
    }
}
