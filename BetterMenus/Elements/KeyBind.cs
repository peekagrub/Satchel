using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Modding;
using Modding.Menu;
using Satchel.BetterMenus.Config;
using UnityEngine;
using UnityEngine.UI;

namespace Satchel.BetterMenus
{
    /// <summary>
    /// A KeyBind.
    /// </summary>
    public class KeyBind : Element
    {
        /// <summary>
        /// The PlayerAction connected to this bind.
        /// </summary>
        public InControl.PlayerAction PlayerAction;

        private SelectableArea SelectableArea = SelectableArea.Full;

        /// <summary>
        /// Creates a new KeyBind.
        /// </summary>
        /// <param name="name">The name to be displayed.</param>
        /// <param name="playerAction">The PlayerAction connected to this bind.</param>
        /// <param name="Id">The Id of this Element.</param>
        public KeyBind(string name,InControl.PlayerAction playerAction,string Id = "__UseName") : base(Id,name)
        {
            Name = name;
            PlayerAction = playerAction;
        }
        internal KeyBind(string name,InControl.PlayerAction playerAction,string Id = "__UseName", SelectableArea selectableArea = SelectableArea.Full) : base(Id,name)
        {
            Name = name;
            PlayerAction = playerAction;
            SelectableArea = selectableArea;
        }

        /// <summary>
        /// Creates a GameObjectRow based on the current variables.
        /// </summary>
        /// <param name="c">The ContentArea on which the ButtonBind is created.</param>
        /// <param name="modlistMenu">The previous MenuScreen.</param>
        /// <param name="Instance">The current Menu instance.</param>
        /// <param name="AddToList">Should this element be added to the MenuOrder (All non IShadowElements).</param>
        /// <returns>The created GameObjectRow which can be used to add to the corresponding Lists.</returns>
        public override GameObjectRow Create(ContentArea c, MenuScreen modlistMenu, Menu Instance, bool AddToList = true)
        {
            
            _ = Name ?? throw new ArgumentNullException(nameof(Name), "Name cannot be null");
            _ = PlayerAction ?? throw new ArgumentNullException(nameof(PlayerAction), "PlayerAction cannot be null");
            //todo add KeybindStyle support
            c.AddCustomKeybind(
                Name,
                PlayerAction,
                new KeybindConfig
                {
                    CancelAction = _ => UIManager.instance.UIGoToDynamicMenu(modlistMenu),
                    Label = Name,
                    selectableArea = SelectableArea
                }, out var option);
            if (AddToList)
            {
                Instance.MenuOrder.Add(new GameObjectRow(option.gameObject));
            }

            gameObject = option.gameObject;
            
            ((IContainer) Parent).OnBuilt += (_, element) =>
            {
                //UpdatePosValues();
            };

            return new GameObjectRow(option.gameObject);
        }



        public override void Update()
        {
            var mappableControllerButton = gameObject.GetComponent<MappableKey>();
            mappableControllerButton.InitCustomActions(PlayerAction.Owner, PlayerAction);

            gameObject.transform.Find("Text").GetComponent<Text>().text = Name;
        }
    }
}