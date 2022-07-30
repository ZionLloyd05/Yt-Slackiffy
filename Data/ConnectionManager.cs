using System;

using System.Collections.Generic;
using System.Linq;

namespace Slackiffy.Data
{
    public class ConnectionManager
    {
        private readonly Dictionary<string, HashSet<string>> _connections =
            new Dictionary<string, HashSet<string>>();

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(string conKey, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(conKey, out connections))
                {
                    connections = new HashSet<string>();
                    _connections.Add(conKey, connections);
                }

                lock (connections)
                {
                    connections.Add(connectionId);
                }
            }
        }

        public string GetConnection(string key)
        {
            var connection = _connections[key];
            if (connection != null)
            {
                return connection.ToString();
            }

            return String.Empty;
        }

        public Dictionary<string, HashSet<string>> GetUsers()
        {
            return _connections;
        }

        public IEnumerable<string> GetConnections(string conKey)
        {
            HashSet<string> connections;
            if (_connections.TryGetValue(conKey, out connections))
            {
                return connections;
            }

            return Enumerable.Empty<string>();
        }

        public void Remove(string conKey, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(conKey, out connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0)
                    {
                        _connections.Remove(conKey);
                    }
                }
            }
        }
    }
}
