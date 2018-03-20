using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardEncoderLib
{
    public class SL600MCReaderAdapter : ReaderAdapter
    {
        private CardReader cardReader;

        public CardReader GetCardReader()
        {
            cardReader = new SL600MCReader();

            return cardReader;
        }
    }
}