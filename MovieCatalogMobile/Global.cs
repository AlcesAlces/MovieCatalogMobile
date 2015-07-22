using System;
using SocketIOClient;

namespace MovieCatalogMobile
{
    public static class Global
    {
        public static string userName = null;
        public static string uid = null;
        public static Client socket;
        public static int serverTimeout = 10000;
        public static string connectionString = "http://23.96.28.16:80/";
    }
}

