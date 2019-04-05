﻿using GameObjects.Utils;
using Managers;

namespace GameObjects
{
    // Пшеницей можно покормить курицу или корову;
    public class Wheat : AProducer, IBuyable, IPlaceable, IFood
    {
        public override void ResetWillProduceAfterSecondsCount()
        {
            WillProduceAfterSecondsCount = GameManager.Instance.ProductionDurationDictionaryService
                .GetProductionDurationForProducer<Cow>();
        }
    }
}
