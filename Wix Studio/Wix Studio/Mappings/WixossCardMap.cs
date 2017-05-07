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
            Map(x => x.CardNumberInSet);
            Map(x => x.CardSet);
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

            HasMany(x => x.Timing).Inverse().Cascade.All().Table("CardTiming").Element("CardTiming");
            HasMany(x => x.Cost).Inverse().Cascade.All();

            HasMany(x => x.Color).Table("CardColor").Element("CardColor").Cascade.All();


            //HasMany(x => x.Color).Inverse().Cascade.All().Table("CardColor").Element("CardColor");
            HasMany(x => x.Class).Inverse().Cascade.All().Table("CardClass").Element("CardClass");
        }
    }
}
