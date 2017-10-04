using FluentNHibernate.Mapping;

namespace Wix_Studio.NHibernate.Mappings
{
    public class CardCostMap : ClassMap<WixossCard.CardCost>
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
