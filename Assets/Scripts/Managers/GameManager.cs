﻿using Services;
using UnityEngine;
using Utils;

namespace Managers
{
    public class GameManager : MonoBehaviour, IInitializable
    {
        // static instance of GameManager which allows it to be accessed by any other script 
        public static GameManager Instance;

        public UserInterfaceManager UserInterfaceManager
        {
            get { return this.gameObject.GetComponent<UserInterfaceManager>(); }
        }

        public GameObjectsManager GameObjectsManager
        {
            get { return this.gameObject.GetComponent<GameObjectsManager>(); }
        }

        public ImageManager ImageManager
        {
            get { return this.gameObject.GetComponent<ImageManager>(); }
        }

        public TimeManager TimeManager
        {
            get { return this.gameObject.GetComponent<TimeManager>(); }
        }

        public ConfigurationService ConfigurationService;

        public PriceService PriceService;

        public TradeService TradeService;

        public MoneyService MoneyService;

        public ProductionDurationService ProductionDurationService;

        public SatietyDurationService SatietyDurationService;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;

                DontDestroyOnLoad(gameObject); // sets this to not be destroyed when reloading scene 
            }
            else
            {
                if (Instance != this)
                {
                    // this enforces our singleton pattern, meaning there can only ever be one instance of a GameManager 
                    Destroy(gameObject);
                }
            }

            Initialize();
        }

        public void Initialize()
        {
            UserInterfaceManager.Initialize();
            GameObjectsManager.Initialize();
            ImageManager.Initialize();
            TimeManager.Initialize();

            ConfigurationService = new ConfigurationService();
            ConfigurationService.Initialize();
            PriceService = new PriceService();
            PriceService.Initialize();
            //before trade need to know a price
            TradeService = new TradeService();
            TradeService.Initialize();
            MoneyService = new MoneyService();
            MoneyService.Initialize();
            ProductionDurationService = new ProductionDurationService();
            ProductionDurationService.Initialize();
            SatietyDurationService = new SatietyDurationService();
            SatietyDurationService.Initialize();
        }
    }
}
