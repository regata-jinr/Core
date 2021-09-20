/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2021, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using Regata.Core.DataBase;
using Regata.Core.DataBase.Models;
using Regata.Core.Settings;
using System.Linq;

namespace Regata.Core.Messages
{
    public class Message
    {
        public string Caption      { get; set; } // filled in report notify
        public string Head         { get; set; }
        public int    Code         { get; set; }
        public Status Status       { get; set; } 
        public string Text         { get; set; }
        public string DetailedText { get; set; } // filled in report notify
        public string Sender       { get; set; } // filled in report notify

        private MessageBase _mb;

        public Message(int code)
        {
            Code = code;
            Status = code == 0 ? Status.Error : (Status)(code / 1000);

            using (var rdbc = new RegataContext())
            {
                _mb = rdbc.MessageBases.Where(m => m.Code == Code && m.Language == GlobalSettings.CurrentLanguage.ToString()).FirstOrDefault();
            }

            if (_mb == null)
                _mb = new MessageBase();

            Head = _mb.Head;
            Text = _mb.Text;
        }

    } // public class Message
}     // namespace Regata.Core
