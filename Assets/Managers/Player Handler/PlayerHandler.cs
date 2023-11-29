using System;
using Common.CommonScripts.Constants;
using UnityEngine;

namespace Managers.Player_Handler
{
    public class PlayerHandler : MonoBehaviour
    {
        public string CurrentPlayerName { get; private set; }

        public void SetCurrentPlayerName(string name)
        {
            CurrentPlayerName = name ?? throw new ArgumentNullException();

            if (name == string.Empty)
            {
                CurrentPlayerName = DefaultPlayerName.DefaultName;
                return;
            }

            CurrentPlayerName = name;
        }
    }
}
