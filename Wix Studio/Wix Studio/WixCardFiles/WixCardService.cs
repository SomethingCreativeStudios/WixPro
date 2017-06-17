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
using NHibernate.Transform;
using Wix_Studio.NHibernate.Mappings;
using Wix_Studio.WixCardFiles;

namespace Wix_Studio
{
    public class WixCardService
    {
        public static String databaseName = "batorubase.db";
        public static WixossCard CreateOrUpdate(WixossCard wixossCard)
        {
            if ( !Exists(wixossCard.CardName) )
            {
                var sessionFactory = CreateSessionFactory();

                using ( var session = sessionFactory.OpenSession() )
                {
                    using ( var transaction = session.BeginTransaction() )
                    {
                        session.SaveOrUpdate(wixossCard);
                        transaction.Commit();
                    }
                }
            }
            return wixossCard;
        }

        public static List<String> GetSetNames()
        {
            List<String> setNames = new List<string>();
            var sessionFactory = CreateSessionFactory();

            using ( var session = sessionFactory.OpenSession() )
            {
                using ( var transaction = session.BeginTransaction() )
                {
                    var sql = String.Format("SELECT distinct CardSet FROM {0} ORDER BY CardSet" , "CardSet");
                    var query = session.CreateSQLQuery(sql);
                    var result = query.List<String>();

                    setNames = result.ToList<String>();
                }
            }

            return setNames;
        }

        public static List<String> GetAllClasses()
        {
            List<String> cardClasses = new List<string>();
            var sessionFactory = CreateSessionFactory();

            using ( var session = sessionFactory.OpenSession() )
            {
                using ( var transaction = session.BeginTransaction() )
                {
                    var sql = String.Format("SELECT distinct CardClass FROM {0} ORDER BY CardClass" , "CardClass");
                    var query = session.CreateSQLQuery(sql);
                    var result = query.List<String>();

                    cardClasses = result.ToList<String>();
                }
            }

            return cardClasses;
        }

        public static List<String> GetAllCardNames()
        {
            List<String> cardNames = new List<string>();
            var sessionFactory = CreateSessionFactory();

            using ( var session = sessionFactory.OpenSession() )
            {
                using ( var transaction = session.BeginTransaction() )
                {
                    var sql = String.Format("SELECT distinct CardName FROM {0} ORDER BY CardName" , "WixossCard");
                    var query = session.CreateSQLQuery(sql);
                    var result = query.List<String>();

                    cardNames = result.ToList<String>();
                }
            }

            return cardNames;
        }

        public static WixossCard FindById(int cardId)
        {
            WixossCard card = null;
            var sessionFactory = CreateSessionFactory();

            using ( var session = sessionFactory.OpenSession() )
            {
                using ( var transaction = session.BeginTransaction() )
                {
                    try
                    {
                        var criteria = session.CreateCriteria<WixossCard>();
                        criteria.Add(Expression.Eq("Id" , cardId)); // where clause in subquery
                        var result = criteria.List<WixossCard>();
                        card = result[0];
                    }
                    catch { }

                }
            }

            return card;
        }

        public static Boolean Exists(String cardName)
        {
            bool cardExists = false;
            var sessionFactory = CreateSessionFactory();

            using ( var session = sessionFactory.OpenSession() )
            {
                using ( var transaction = session.BeginTransaction() )
                {
                    var criteria = session.CreateCriteria<WixossCard>();
                    criteria.Add(Expression.Eq("CardName" , cardName)); // where clause in subquery
                    var result = criteria.List<WixossCard>();
                    cardExists = result.Count != 0;

                }
            }

            return cardExists;
        }

        public static List<WixossCard> getAllCards()
        {
            var allCards = new List<WixossCard>();
            var sessionFactory = CreateSessionFactory();

            using ( var session = sessionFactory.OpenSession() )
            {
                using ( session.BeginTransaction() )
                {
                     var cards = session.CreateCriteria(typeof(WixossCard)).List<WixossCard>();

                    foreach ( var card in cards )
                    {
                        allCards.Add(card);
                    }
                }
            }
            
            return allCards;
        }
        public static List<WixossCard> Search(WixCardSearchModel searchCard )
        {
            return Search(searchCard , SortBy.Color , SortOrder.ASC);
        }
        public static List<WixossCard> Search(WixCardSearchModel searchCard , SortBy sortBy , SortOrder sortOrder)
        {
            List<WixossCard> resultCards = new List<WixossCard>();
            var sessionFactory = CreateSessionFactory();

            if ( searchCard != null )
            {
                using ( var session = sessionFactory.OpenSession() )
                {
                    using ( session.BeginTransaction() )
                    {

                        var criteria = session.CreateCriteria<WixossCard>();

                        if ( searchCard.Color != null  && searchCard.Color != CardColor.NoColor)
                        {
                            criteria.CreateAlias("Color" , "color");
                            criteria.Add(Expression.Eq("color.elements" , searchCard.Color));
                        }

                        if ( searchCard.cardName != null && searchCard.cardName != "")
                        {
                            criteria.Add(Expression.Like("CardName" , "%" + searchCard.cardName + "%"));
                        }

                        if ( searchCard.cardEffect != null && searchCard.cardEffect != "")
                        {
                            criteria.Add(Expression.Like("CardEffect" , "%" + searchCard.cardEffect + "%"));
                        }

                        if ( searchCard.Guard != null )
                        {
                            criteria.Add(Expression.Eq("Guard" , searchCard.Guard));
                        }

                        if ( searchCard.MultiEner != null )
                        {
                            criteria.Add(Expression.Eq("MultiEner" , searchCard.MultiEner));
                        }

                        if ( searchCard.LifeBurst != null )
                        {
                            criteria.Add(Expression.Eq("LifeBurst" , searchCard.LifeBurst));
                        }

                        if ( searchCard.setName != null && searchCard.setName != "")
                        {
                            criteria.CreateAlias("CardSets" , "set");
                            criteria.Add(Expression.Like("set.elements" , "%" + searchCard.setName + "%"));
                        }

                        if ( searchCard.Type != null && searchCard.Type != CardType.NoType)
                        {
                            criteria.CreateAlias("Type" , "cardType");
                            criteria.Add(Expression.Eq("cardType.elements" , searchCard.Type));
                        }

                        if ( searchCard.MinPower != 0 || searchCard.MaxPower != 0 )
                        {
                            if ( searchCard.MinPower != 0 )
                                criteria.Add(Expression.Ge("Power" , searchCard.MinPower));

                            if ( searchCard.MaxPower != 0 )
                                criteria.Add(Expression.Le("Power" , searchCard.MaxPower));
                        }

                        if ( searchCard.MinLevel != 0 || searchCard.MaxLevel != 0 )
                        {
                            if ( searchCard.MinLevel != 0 )
                                criteria.Add(Expression.Ge("Level" , searchCard.MinLevel));

                            if ( searchCard.MaxLevel != 0 )
                                criteria.Add(Expression.Le("Level" , searchCard.MaxLevel));
                        }


                        criteria.SetResultTransformer(Transformers.DistinctRootEntity);
                        resultCards = criteria.List<WixossCard>().ToList();
                    }
                }
            }
            return resultCards;
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure().Database(SQLiteConfiguration.Standard.UsingFile(databaseName))
              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<WixCardService>())
              .ExposeConfiguration(BuildSchema)
              .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            //Allows us to log, and output sql commands
            config.SetInterceptor(new SqlStatementInterceptor());

            //Only create schema if file is not found
            if ( !File.Exists(databaseName) )
            {
                //File.Delete("firstProject.db");
                new SchemaExport(config).Create(false , true);
            }
        }
    }
}