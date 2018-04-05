using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstract_Factory
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationClass mssql = new ApplicationClass(new MSSQLFactory());
            mssql.Connect();
            mssql.Execute();
            mssql.Disconnect();

            Console.WriteLine("-----");

            ApplicationClass oracle = new ApplicationClass(new OracleFactory());
            oracle.Connect();
            oracle.Execute();
            oracle.Disconnect();

            Console.Read();

        }

       

        public interface IConnection
        {
            void Connect();
            void Disconnect();
        }

        public class MSSQLConnection : IConnection
        {
            public void Connect()
            {
                Console.WriteLine("MSSQL Bağlantısı Oluşturuldu");
            }

            public void Disconnect()
            {
                Console.WriteLine("MSSQL Bağlantısı Sonlandırıldı");
            }
        }

        public class OracleConnection : IConnection
        {
            public void Connect()
            {
                Console.WriteLine("Oracle Bağlantısı oluşturuldu");
            }

            public void Disconnect()
            {
                Console.WriteLine("Oracle Bağlantısı Sonlandırıldı");
            }
        }

        

        public interface ICommand
        {
            void Execute();
        }

        public class MSSQLCommand : ICommand
        {
            public void Execute()
            {
                Console.WriteLine("MSSQL sorgusu çalıştırılıyor");
            }
        }

        public class OracleCommand : ICommand
        {
            public void Execute()
            {
                Console.WriteLine("Oracle sorgusu çalıştırılıyor");
            }
        }

        

        public abstract class DatabaseFactory
        {
            public abstract IConnection CreateConnection();
            public abstract ICommand CreateCommand();
        }


       

        public class MSSQLFactory : DatabaseFactory
        {
            public override ICommand CreateCommand()
            {
                return (new MSSQLCommand());
            }

            public override IConnection CreateConnection()
            {
                return (new MSSQLConnection());
            }
        }

        

        public class OracleFactory : DatabaseFactory
        {
            public override ICommand CreateCommand()
            {
                return (new OracleCommand());
            }

            public override IConnection CreateConnection()
            {
                return (new OracleConnection());
            }
        }


        /*   Application Class   */
        public class ApplicationClass
        {
            private DatabaseFactory databaseFactory;
            private IConnection connection;
            private ICommand command;

            public ApplicationClass(DatabaseFactory factory) // ctor
            {
                databaseFactory = factory;
                connection = factory.CreateConnection();
                command = factory.CreateCommand();
            }

            public void Connect()
            {
                connection.Connect();
            }

            public void Disconnect()
            {
                connection.Disconnect();
            }

            public void Execute()
            {
                command.Execute();
            }
        }

    }
}

