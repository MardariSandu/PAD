using Common;

namespace Broker
{
     static class ConnectionsStorage
     {
          private static List<ConnectionInfo> _connections;
          private static object _locker;

          static ConnectionsStorage()
          {
               _connections = new List<ConnectionInfo>();
               _locker = new object();
          }

          public static void Add(ConnectionInfo connection)
          {
               lock (_locker)
               {
                    _connections.Add(connection);
               }
          }

          public static void Remove(string address)
          {
               lock (_locker) 
               {
                    _connections.RemoveAll(x => x.Address == address);
               }
          }

          public static List<ConnectionInfo> GetConnectionsByTopic(string topic) 
          {
               List<ConnectionInfo> selectedConnections;

               lock (_locker)
               {
                    selectedConnections = _connections.Where(x => x.Topic == topic).ToList();
               }

               return selectedConnections;
          }
     }
}
