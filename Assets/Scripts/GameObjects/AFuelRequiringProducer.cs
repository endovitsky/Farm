﻿using GameObjects.Utils;
using Managers;
using UnityEngine;

namespace GameObjects
{
    public abstract class AFuelRequiringProducer<T1, T2> : AProducer<T1>, IFeedable where T1 : class, IProduction, new() where T2 : IFood
    {
        public int HaveFuelForSecondsCount { get; protected set; }

        public override void TryProduceProduct()
        {
            if (HaveFuelForSecondsCount > 0)
            {
                base.TryProduceProduct();

                HaveFuelForSecondsCount--;
            }
        }

        public void Feed(IFood food)
        {
            Debug.Log(string.Format("Feeding a {0} with {1}", this.GetType().Name, food.GetType().Name));

            HaveFuelForSecondsCount +=
                GameManager.Instance.SatietyDurationDictionaryService.GetSatietyForProduction(this.GetType(),
                    food.GetType());
        }
    }
}
