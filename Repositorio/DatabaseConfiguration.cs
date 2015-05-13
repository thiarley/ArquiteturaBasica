using System.Data;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using Repository.Mapping;

namespace ArquiteturaPadrao.Repositorio
{
    public sealed class DatabaseConfiguration
    {
        private static Configuration _configuration;
        private static ISessionFactory _sessionFactory;
        private static IDbConnection _dbConnection;
        private static SchemaExport export;

        public static void Inicialize(Configuration cgf)
        {
            _configuration = cgf;
            //ValidateAndCreate();
        }

        /// <summary>
        /// Creates a database in current configuration
        /// </summary>
        public static void ValidateAndCreate()
        {
            try
            {
                SchemaExport ex = new SchemaExport(_configuration);
                ex.SetDelimiter(";");
                ex.SetOutputFile(@"DabiDatabaseCreation.sql");
                ex.Execute(true, false,false);
            }
            catch (Exception)
            {
                //log
            }
        }             
    }
}