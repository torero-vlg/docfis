using System;
using Db.DataAccess;
using Db.Mapping;
using Db.Tools;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;

namespace Db
{
    public class NhDbFactory : AbstractDbFactory
    {
        private readonly ISessionFactory _sessionFactory;
        
        public NhDbFactory(string connectionString)
        {
//            _sessionFactory = CreatePostgreSessionFactory(connectionString);
            _sessionFactory = CreateSqLiteSessionFactory(connectionString);
        }

        public NhDbFactory()
        {
            _sessionFactory = CreateSqLiteInMemorySessionFactory();
        }


        public override IBaseDb CreateBaseDb()
        {
            return new NhBaseMessageDb(_sessionFactory);
        }

        private ISessionFactory CreatePostgreSessionFactory(string connectionString)
        {
            
            try
            {
                var factory = Fluently.Configure()
                    .Database(PostgreSQLConfiguration.PostgreSQL82
                        .ConnectionString(connectionString))
                    .ExposeConfiguration(c => c.Properties.Add("current_session_context_class",
                        typeof (CallSessionContext).FullName))
                    .Mappings(x => x.FluentMappings.AddFromAssemblyOf<NewsMap>())
                    .BuildSessionFactory();
                return factory;
            }
            catch (Exception ex)
            {
                var msg = string.Format("{0} [InnerException]: {1}", ex.Message, ex.InnerException);
                MonitorLog.WriteLog(msg, MonitorLog.typelog.Error, true);
                throw new Exception();
            }
        }

        private ISessionFactory CreateSqLiteSessionFactory(string connectionString)
        {
            try
            {
                var str = string.Format("Data Source={0}{1};Version=3;", AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/"), connectionString);
                var factory = Fluently.Configure()
                    .Database(SQLiteConfiguration.Standard.ConnectionString(str))
                    .ExposeConfiguration(c => c.Properties.Add("current_session_context_class",
                        typeof(CallSessionContext).FullName))
                    .Mappings(x => x.FluentMappings.AddFromAssemblyOf<NewsMap>())
                    .BuildSessionFactory();

                return factory;
            }
            catch (Exception ex)
            {
                var msg = string.Format("{0} [InnerException]: {1}", ex.Message, ex.InnerException);
                MonitorLog.WriteLog(msg, MonitorLog.typelog.Error, true);
                return null;
            }
        }

        public ISessionFactory CreateSqLiteInMemorySessionFactory() 
        {
            try
            {
                var fluentConfiguration = Fluently.Configure()
                    .Database(SQLiteConfiguration.Standard.InMemory()
                    .ConnectionString("data source=:memory:"))
                    .Mappings(x => x.FluentMappings.AddFromAssemblyOf<NewsMap>());

                var cfg = fluentConfiguration.BuildConfiguration();
                var t = new SchemaUpdate(cfg);
                t.Execute(false,true);

                var factory = cfg.BuildSessionFactory();

                return factory;
            }
            catch (Exception ex)
            {
                return null;
            }
        } 
    }
}
