﻿using System.IO;
using System;
using Newtonsoft.Json;
using System.Threading;

namespace StackExchange.Redis.Tests
{
    public static class TestConfig
    {
        private const string FileName = "TestConfig.json";

        public static Config Current { get; }

        private static int _db = 17;
        public static int GetDedicatedDB(IConnectionMultiplexer conn = null)
        {
            int db = Interlocked.Increment(ref _db);
            if (conn != null) Skip.IfMissingDatabase(conn, db);
            return db;
        }

        static TestConfig()
        {
            Current = new Config();
            try
            {
                using (var stream = typeof(TestConfig).Assembly.GetManifestResourceStream("StackExchange.Redis.Tests." + FileName))
                {
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            Current = JsonConvert.DeserializeObject<Config>(reader.ReadToEnd());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Deserializing TestConfig.json: " + ex);
            }
        }

        public class Config
        {
            public bool UseSharedConnection { get; set; } = true;
            public bool RunLongRunning { get; set; }
            public bool LogToConsole { get; set; }

            public string MasterServer { get; set; } = "127.0.0.1";
            public int MasterPort { get; set; } = 6379;
            public string MasterServerAndPort => MasterServer + ":" + MasterPort.ToString();

            public string SlaveServer { get; set; } = "127.0.0.1";
            public int SlavePort { get; set; } = 6380;
            public string SlaveServerAndPort => SlaveServer + ":" + SlavePort.ToString();

            public string SecureServer { get; set; } = "127.0.0.1";
            public int SecurePort { get; set; } = 6381;
            public string SecurePassword { get; set; } = "changeme";
            public string SecureServerAndPort => SecureServer + ":" + SecurePort.ToString();

            // Separate servers for failover tests, so they don't wreak havoc on all others
            public string FailoverMasterServer { get; set; } = "127.0.0.1";
            public int FailoverMasterPort { get; set; } = 6382;
            public string FailoverMasterServerAndPort => FailoverMasterServer + ":" + FailoverMasterPort.ToString();

            public string FailoverSlaveServer { get; set; } = "127.0.0.1";
            public int FailoverSlavePort { get; set; } = 6383;
            public string FailoverSlaveServerAndPort => FailoverSlaveServer + ":" + FailoverSlavePort.ToString();

            public string IPv4Server { get; set; } = "127.0.0.1";
            public int IPv4Port { get; set; } = 6379;
            public string IPv6Server { get; set; } = "::1";
            public int IPv6Port { get; set; } = 6379;

            public string RemoteServer { get; set; } = "127.0.0.1";
            public int RemotePort { get; set; } = 6379;
            public string RemoteServerAndPort => RemoteServer + ":" + RemotePort.ToString();

            public string SentinelServer { get; set; } = "127.0.0.1";
            public int SentinelPort { get; set; } = 26379;
            public int SentinelPort1 { get; set; } = 26380;
            public int SentinelPort2 { get; set; } = 26381;
            public string SentinelSeviceName { get; set; } = "mymaster";

            public string ClusterServer { get; set; } = "127.0.0.1";
            public int ClusterStartPort { get; set; } = 7000;
            public int ClusterServerCount { get; set; } = 6;

            public string SslServer { get; set; }
            public int SslPort { get; set; }

            public string RedisLabsSslServer { get; set; }
            public int RedisLabsSslPort { get; set; } = 6379;
            public string RedisLabsPfxPath { get; set; }

            public string AzureCacheServer { get; set; }
            public string AzureCachePassword { get; set; }

            public string SSDBServer { get; set; }
            public int SSDBPort { get; set; } = 8888;
        }
    }
}
