using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Tool.hbm2ddl;
using Wix_Studio.NHibernate.Mappings;
using Wix_Studio.WixCardFiles;

namespace Wix_Studio
{
    public class WixCardService
    {
        public static WixossCard Create(WixossCard wixossCard)
        {
            WixossCard wixCard = null;
            var sessionFactory = CreateSessionFactory();

            using ( var session = sessionFactory.OpenSession() )
            {
                using ( var transaction = session.BeginTransaction() )
                {
                    session.SaveOrUpdate(wixossCard);
                    transaction.Commit();
                }

                // retreive all stores and display them
                using ( session.BeginTransaction() )
                {
                    var stores = session.CreateCriteria(typeof(WixossCard))
                      .List<WixossCard>();

                    /*var subCriteria = DetachedCriteria.For<WixossCard>(); // subquery
                    subCriteria.Add(Expression.Eq("Color", house)); // where clause in subquery
                    subCriteria.SetProjection(Projections.Id()); // DetachedCriteria needs to have a projection, id of Room is projected here

                    var criteria = session.CreateCriteria<Person>();
                    criteria.Add(Subqueries.PropertyIn("Room" , subCriteria)); // in operator to search in detached criteria
                    var result = criteria.List<Person>();*/


                    foreach ( var store in stores )
                    {
                        string test = "";
                    }
                }
            }

           
        

            return wixossCard;
        }

        public static List<WixossCard> Search(WixCardSearchModel searchCard, SortBy sortBy, SortOrder sortOrder)
        {
            CardCollection cardCollection = new CardCollection();
            List<WixossCard> resultCards = new List<WixossCard>();
            List<WixossCard> totalCards = ( searchCard.setName != "" ? cardCollection.GetCardsInSets(searchCard.setName) : CardCollection.cardCollection.Values.ToList() );
            foreach ( var wixCard in totalCards)
            {
                Boolean addCard = true;// searchCard.isEmpty();
                if ( addCard )
                {
                    if ( !FallsInRange(searchCard.MinPower , searchCard.MaxPower , wixCard.Power) )
                        addCard = false;

                    if ( !FallsInRange(searchCard.MinLevel , searchCard.MaxLevel , wixCard.Level) )
                        addCard = false;

                    if ( !CheckBoolean(searchCard.Guard , wixCard.Guard) )
                        addCard = false;

                    if ( !CheckBoolean(searchCard.LifeBurst , wixCard.LifeBurst) )
                        addCard = false;

                    if ( !CheckBoolean(searchCard.MultiEner , wixCard.MultiEner) )
                        addCard = false;

                    if ( !CheckEnum<CardColor>(searchCard.Color , wixCard.Color.ToList()) )
                        addCard = false;

                    if ( !CheckEnum<CardTiming>(searchCard.Timing , wixCard.Timing.ToList()) )
                        addCard = false;

                    if ( (searchCard.Type != null && searchCard.Type != CardType.NoType) && searchCard.Type.Value != wixCard.Type )
                        addCard = false;

                    if ( searchCard.cardEffect != "" && !wixCard.CardEffect.ToLower().Contains(searchCard.cardEffect.ToLower()) )
                        addCard = false;

                    if ( searchCard.cardName != "" && !wixCard.CardName.ToLower().Contains(searchCard.cardName.ToLower()) )
                        addCard = false;
                }

                if ( addCard )
                    resultCards.Add(wixCard);
            }

            return resultCards;
        }

        public static Boolean CheckEnum<T>(Enum searchEnum, List<T> cardEnum)
        {
            bool cardMatches = true;

            if ( !searchEnum.ToString().StartsWith("No") )
            {
                cardMatches = false;
                foreach ( var enumValue in cardEnum )
                {
                    if ( searchEnum.ToString().Equals(enumValue.ToString()) )
                    {
                        cardMatches = true;
                        break;
                    }
                }
            }

            return cardMatches;
        }

        public static Boolean CheckBoolean(Boolean? searchBool, Boolean cardBool)
        {
            bool cardMatches = true;

            if(searchBool != null )
            {
                cardMatches = searchBool == cardBool;
            }

            return cardMatches;
        }
        public static Boolean FallsInRange(int min , int max , int target)
        {
            bool inRange = false;
            if ( min == 0 && max == 0 )
                inRange = true;
            else if ( min == 0 && target <= max )
                inRange = true;
            else if ( max == 0 && target >= min )
                inRange = true;
            else if ( target >= min && target <= max )
                inRange = true;

            return inRange;
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure().Database(SQLiteConfiguration.Standard.UsingFile("firstProject.db"))
              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<WixCardService>())
              .ExposeConfiguration(BuildSchema)
              .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            if ( !File.Exists("firstProject.db") )
            {
                //File.Delete("firstProject.db");

                // this NHibernate tool takes a configuration (with mapping info in)
                // and exports a database schema from it
                new SchemaExport(config).Create(false , true);
            }
        }
    }
}
