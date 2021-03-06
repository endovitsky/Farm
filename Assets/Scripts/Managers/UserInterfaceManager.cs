﻿using GameObjects.Cells;
using GameObjects.Items;
using GameObjects.Utils;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.Menu;
using Utils;

/*
Игра должна включать в себя простейшую графическую реализацию:
    отображение объектов,
    индикаторы прогресса и корма,
    возможность совершить описанные выше действия и понять состояние объектов.
 */

namespace Managers
{
    public class UserInterfaceManager : MonoBehaviour, IInitializable
    {
        [SerializeField]
        private Canvas _canvas;

        [SerializeField]
        private UserInterface.UserInterface _userInterfacePrefab;

        [SerializeField]
        private ListMenuView _listMenuViewPrefab;

        [SerializeField]
        private ButtonView _buttonViewPrefab;

        private UserInterface.UserInterface _userInterfaceInstance;

        private ListMenuView _listMenuViewInstance;

        public void Initialize()
        {
            _userInterfaceInstance = InstantiateFullScreenWindow(_userInterfacePrefab);
        }

        public void ShowMenu(Cell cell)
        {
            _listMenuViewInstance = InstantiateFullScreenWindow(_listMenuViewPrefab, false);
            _listMenuViewInstance.BackgroundImageClicked += view =>
            {
                Destroy(view.gameObject);
                _listMenuViewInstance = null;
            };
            _listMenuViewInstance.Initialize(cell.gameObject.GetComponent<RectTransform>());

            AddButton<Wheat>(cell);
            AddButton<Chicken>(cell);
            AddButton<Cow>(cell);
        }

        private void AddButton<T>(Cell cell) where T : IBuyable, IPlaceable, IProgressive, new()
        {
            var button = _listMenuViewInstance.AddButton(_buttonViewPrefab.GetComponent<Button>(), () =>
            {
                var value = GameManager.Instance.TradeService.TryBuy<T>();
                if (value == null)
                {
                    return;
                }

                cell.SetContent(value);
                value.ProgressChanged += f =>
                {
                    cell.SetProgressBarValue(f);
                };

                Destroy(_listMenuViewInstance.gameObject);
            });

            var buttonView = button.GetComponent<ButtonView>();
            if (buttonView != null)
            {
                buttonView.SetContentImage(GameManager.Instance.ImageManager.GetImage(typeof(T).Name)); 
            }
        }

        private T InstantiateFullScreenWindow<T>(T prefab, bool asFirstSibling = true) where T : MonoBehaviour, IInitializable
        {
            var instance = Instantiate(prefab);
            instance.gameObject.transform.SetParent(_canvas.gameObject.transform);
            if (asFirstSibling)
            {
                instance.gameObject.transform.SetAsFirstSibling();
            }
            else
            {
                instance.gameObject.transform.SetAsLastSibling();
            }
            instance.gameObject.GetComponent<RectTransform>()
                .SetHeight(_canvas.gameObject.GetComponent<RectTransform>().GetHeight());
            instance.gameObject.GetComponent<RectTransform>()
                .SetWidth(_canvas.gameObject.GetComponent<RectTransform>().GetWidth());
            instance.gameObject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

            instance.Initialize();

            return instance;
        }
    }
}
