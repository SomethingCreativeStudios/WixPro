using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wix_Studio.WixCardFiles
{
    public class WixCardSearchModel
    {
        public int MinPower { get; set; }
        public int MaxPower { get; set; }

        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }

        public CardColor? Color { get; set; }
        public CardType? Type { get; set; }
        public CardTiming? Timing { get; set; }
        public Boolean? Guard { get; set; }
        public Boolean? MultiEner { get; set; }
        public Boolean? LifeBurst { get; set; }
        public String cardEffect { get; set; }
        public String cardName { get; set; }

        public bool isEmpty()
        {
            return MinPower == 0 && MaxPower == 0 && MinLevel == 0 && MaxLevel == 0 && Color == CardColor.NoColor && Type == CardType.NoType && Timing == CardTiming.NoTiming;
        }
    }
}
