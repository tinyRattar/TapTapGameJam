using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.UI
{
    public abstract class AbstructUIGroup : MonoBehaviour
    {
        protected List<AbstractUIUnit> units = new List<AbstractUIUnit>();
        public AbstractUIUnit mainUnit;

        protected void AddUnit(AbstractUIUnit unit)
        {
            units.Add(unit);
            unit.SetGroup(this);
        }

        protected void Init()
        {
            AbstractUIUnit[] components = GetComponentsInChildren<AbstractUIUnit>();
            foreach (var unit in components)
            {
                AddUnit(unit);
            }
            foreach (var unit in units)
            {
                unit.Init();
            }
            SelectUnit(units[0]);
        }

        public virtual void Execute()
        {

        }

        //public void SetUnitsActive(bool active,bool init)
        //{
        //    foreach (var unit in units)
        //    {
        //        unit.SetActive(active);
        //        if (init)
        //        {
        //            unit.Init();
        //        }
        //    }
        //}

        public void SelectUnit(AbstractUIUnit unit)
        {
            if (mainUnit != null)
            {
                if (mainUnit.Equals(unit))
                    return;
            }
            mainUnit = unit;
        }
    }

    public abstract class AbstractUIUnit : MonoBehaviour
    {
        protected AbstructUIGroup group;

        //public void SetActive(bool active)
        //{
        //    this.enabled = active;
        //}

        public void SetGroup(AbstructUIGroup group)
        {
            this.group = group;
        }

        public virtual void Init()
        {

        }
        public virtual void Execute()
        {

        }
    }
}