﻿using GameObjects.Utils;
using UnityEngine;

namespace GameObjects
{
    /*
        • Пшеница вырастает за 10 сек, после чего можно собрать урожай (1 единица урожая с одной клетки), затем рост начинается заново;
        • Пшеницей можно покормить курицу или корову;
     */
    public class Wheat : MonoBehaviour, IBuyable
    {
        public int BuyPrice { get; }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
