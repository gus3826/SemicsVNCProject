using SemicsVNC.Vnc;
using SemicsVNC.Vnc.Server;
using SemicsVNC.Windows.Forms.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SemicsVNC.ServerExample
{
    public partial class Form1 : Form
    {
        private TextBox tb_code;
        private Label lb_code;
        private static TcpListener listener = new TcpListener(IPAddress.Any, 5900);
        private static string password = "";
        private static VncServerSession session;
        private Button button1;
        private Button button2;
        private static ListView listView1;
        private ColumnHeader UserIP;
        private ColumnHeader UserID;
        static StringBuilder postParams;
        public static string ip;
        public static List<string> userIDlist=new List<string>();
        public static string userID;
        private static TcpClient client;




        private static void HandleConnected(object sender, EventArgs e)
        {
            //접속했을때 서버 접속자 리스트뷰에 추가
            ListViewItem lvi = new ListViewItem();
            lvi.Text = ip;
            lvi.SubItems.Add(userID);
            listView1.Items.Add(lvi);

            Console.WriteLine("Connected");
        }

        private static void HandleConnectionFailed(object sender, EventArgs e)
        {
            Console.WriteLine("Connection Failed");
        }

        private static void HandleClosed(object sender, EventArgs e)
        {
            //접속종료했을때 서버 접속자 리스트뷰에서 삭제

            //0703 접속한 ip따오는 
            Socket c = client.Client;
            IPEndPoint ip_point = (IPEndPoint)c.RemoteEndPoint;
            ip = ip_point.Address.ToString();
            for (int i = 0; i < userIDlist.Count; i++)
            {
                userIDlist[i] = ip;
            }

            for (int i=0;i<listView1.Items.Count; i++)
            {
             //   MessageBox.Show(i.ToString());
             //   MessageBox.Show(listView1.Items.Count.ToString());
                if (listView1.Items[i].SubItems[1].Text == userIDlist[i])
                {
             //       MessageBox.Show("삭제됨{0}",i.ToString());
                    listView1.Items.RemoveAt(i);
                    userIDlist.RemoveAt(i);
                   
                }
            }

            Console.WriteLine("Closed");
        }

        private static void HandlePasswordProvided(object sender, PasswordProvidedEventArgs e)
        {
            e.Accept(password.ToCharArray());
        }
        public static void ListenerThread()
        {

            while (true)
            {
                // Wait for a connection.
                 client = listener.AcceptTcpClient();   //AcceptTcpClient 접속하는 클라이언트에 대해 TCPClient 객체 생성

                // Set up a framebuffer and options.
                var options = new VncServerSessionOptions();
                options.AuthenticationMethod = AuthenticationMethod.Password;

                // Virtual mouse
                var mouse = new VncMouse();

                // Virtual keyboard
                var keyboard = new VncKeyboard();

                // Create a session.
                session = new VncServerSession();
                session.Connected += HandleConnected;
                session.ConnectionFailed += HandleConnectionFailed;
                session.Closed += HandleClosed;
                session.PasswordProvided += HandlePasswordProvided;
                session.SetFramebufferSource(new VncScreenFramebufferSource("Hello World", Screen.PrimaryScreen));
                session.PointerChanged += mouse.OnMouseUpdate;
                session.KeyChanged += keyboard.OnKeyboardUpdate;
                session.Connect(client.GetStream(), options);

                //0703 접속한 ip따오는 
                Socket c = client.Client;
                IPEndPoint ip_point = (IPEndPoint)c.RemoteEndPoint;
                ip = ip_point.Address.ToString();
              //  MessageBox.Show(ip);
                userID = session.UserData;
                userIDlist.Add(userID);
              //  MessageBox.Show(userID);




                // Let's go.
                //Application.Run();
            }
        }
        // 로컬 IP 주소 받아오기
        private static string GetLocalIP()
        {
            string myIP = string.Empty;
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    myIP = ip.ToString();
                }
            }

            return myIP;
        }

        // Code 난수 생성 함수
        private static string GetRandomCode(int len)
        {
            Random rand = new Random();
            string input = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var chars = Enumerable.Range(0, len)
                .Select(x => input[rand.Next(0, input.Length)]);
            return new string(chars.ToArray());
        }
        //Post 방식 php연결 함수
        public static string PhpConnect(string php)
        {
            HttpWebRequest wReq;
            Stream postDataStream;
            Stream respPostStream;
            StreamReader readerPost;
            HttpWebResponse wResp;

            Encoding encoding = Encoding.UTF8;
            byte[] result = encoding.GetBytes(postParams.ToString());

            wReq = (HttpWebRequest)WebRequest.Create(php);
            wReq.Method = "POST";
            wReq.ContentType = "application/x-www-form-urlencoded";
            wReq.ContentLength = result.Length;

            postDataStream = wReq.GetRequestStream();
            postDataStream.Write(result, 0, result.Length);
            postDataStream.Close();

            wResp = (HttpWebResponse)wReq.GetResponse();
            respPostStream = wResp.GetResponseStream();
            readerPost = new StreamReader(respPostStream, Encoding.Default);

            return readerPost.ReadToEnd();
        }

        //코드 Update 함수
        public static void UpdateServerIP(string serverIP, string code)
        {
            try
            {
                postParams = new StringBuilder();
                postParams.Append("ServerIp=" + serverIP);
                postParams.Append("&Code=" + code);

                string resultPost = PhpConnect("http://3men.pe.kr/updateServerIP.php");
            }
            catch (Exception e)
            {
                Console.WriteLine("예외발생 : " + e.Message);
            }
        }




        public Form1()
        {
            InitializeComponent();

            //리스트뷰 옵션
            listView1.View = View.Details;
            listView1.GridLines = true;
            // listView1.FullRowSelect = true;

            //





            string serverIP = GetLocalIP();
            string code = GetRandomCode(7);
            password = code;
            tb_code.Text = code;

            UpdateServerIP(serverIP, code);  //코드 Update 함수 호출

            listener.Start();

 

            Thread thr = new Thread(new ThreadStart(ListenerThread));
            thr.IsBackground = true;  //데몬 쓰레드로 변경시켜서  부모 프로그램이 죽으면 모두 죽는다.
            thr.Start();

        }

        private void InitializeComponent()
        {
            this.lb_code = new System.Windows.Forms.Label();
            this.tb_code = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            listView1 = new System.Windows.Forms.ListView();
            this.UserIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UserID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lb_code
            // 
            this.lb_code.AutoSize = true;
            this.lb_code.Location = new System.Drawing.Point(250, 115);
            this.lb_code.Name = "lb_code";
            this.lb_code.Size = new System.Drawing.Size(81, 12);
            this.lb_code.TabIndex = 0;
            this.lb_code.Text = "원격제어 코드";
            // 
            // tb_code
            // 
            this.tb_code.Location = new System.Drawing.Point(235, 149);
            this.tb_code.Name = "tb_code";
            this.tb_code.ReadOnly = true;
            this.tb_code.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_code.Size = new System.Drawing.Size(110, 21);
            this.tb_code.TabIndex = 1;
            this.tb_code.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 281);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 39);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(125, 281);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(70, 39);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.UserIP,
            this.UserID});
            listView1.GridLines = true;
            listView1.Location = new System.Drawing.Point(12, 12);
            listView1.Name = "listView1";
            listView1.Size = new System.Drawing.Size(183, 239);
            listView1.TabIndex = 5;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // UserIP
            // 
            this.UserIP.Text = "접속자IP";
            this.UserIP.Width = 100;
            // 
            // UserID
            // 
            this.UserID.Text = "접속자아이디";
            this.UserID.Width = 85;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 332);
            this.Controls.Add(listView1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tb_code);
            this.Controls.Add(this.lb_code);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = "asd";
            lvi.SubItems.Add("abc");
            listView1.Items.Add(lvi);
        }
    }
}
