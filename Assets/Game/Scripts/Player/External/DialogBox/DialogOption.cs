using System;
using System.Collections.Generic;

namespace Game.Scripts.Player.External.DialogBox
{
    public readonly struct DialogOption
    {
        public readonly string Text;
        public readonly Action OnSelected;
        public readonly List<DialogOption> NextDialogOptions;

        public DialogOption(string text, Action onSelected, List<DialogOption> nextDialogOptions = null)
        {
            Text = text;
            OnSelected = onSelected;
            NextDialogOptions = nextDialogOptions;
        }
    }
}

