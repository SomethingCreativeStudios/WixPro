using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace Wix_Studio.NHibernate.Mappings
{
    public class WixossCardMap : ClassMap<WixossCard>
    {
        public WixossCardMap()
        {
            Id(x => x.Id);
            Map(x => x.CardEffect);
            Map(x => x.CardName);
            Map(x => x.CardUrl);
            Map(x => x.Guard);
            Map(x => x.ImageUrl);
            Map(x => x.Level);
            Map(x => x.LifeBurst);
            Map(x => x.LevelLimit);
            Map(x => x.LimitingCondition);
            Map(x => x.MultiEner);
            Map(x => x.Power);
            Map(x => x.Type);

            HasMany(x => x.Timing).Table("CardTiming").Element("CardTiming").Cascade.All().Not.LazyLoad();
            HasMany(x => x.Cost).Cascade.All().Not.LazyLoad();

            HasMany(x => x.Color).Table("CardColor").Element("CardColor").Cascade.All().Not.LazyLoad();
            HasMany(x => x.CardSets).Table("CardSet").Element("CardSet").Cascade.All().Not.LazyLoad(); ;
            HasMany(x => x.Class).Table("CardClass").Element("CardClass").Cascade.All().Not.LazyLoad();
        }
    }
}
