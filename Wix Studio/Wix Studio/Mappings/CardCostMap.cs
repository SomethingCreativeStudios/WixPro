using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace Wix_Studio.NHibernate.Mappings
{
    public class CardCostMap : ClassMap<CardCost>
    {
        public CardCostMap()
        {
            Id(x => x.Id);
            Map(x => x.numberPerColor);
            Map(x => x.color);
            References(x => x.wixCard);
        }
    }
}
