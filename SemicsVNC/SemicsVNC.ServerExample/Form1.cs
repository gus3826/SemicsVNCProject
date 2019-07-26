using SemicsVNC.Vnc;
using SemicsVNC.Vnc.Server;
using SemicsVNC.Windows.Forms.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace SemicsVNC.ServerExample
{
    public struct NetWorkItem
    {
        public VncServerSession session;
        public TcpClient tcpClient;
    }

    public partial class Form1 : Form
    {
        private TextBox tb_code;
        private Label lb_code;
        private static TcpListener listener = new TcpListener(IPAddress.Any, 5900);
        private static string password = "";
        private static VncServerSession session;
        private static List<NetWorkItem> sessionList = new List<NetWorkItem>();  //세션을 저장하기위한 리스트
        private Button button1;
        private Button button2;
        private static ListView listView1;
        private ColumnHeader UserIP;   //서버 헤더 사용자 IP
        private ColumnHeader UserID;   //서버 헤더 사용자 ID
        static StringBuilder postParams;
        public static string clientIP;  //클라이언트 IP
        public static string clientID;   //클라이언트 ID

        private static string serverIP;      //서버 IP
        private static string code;      //서버 난수 생성코드
        private static string currentDate;  //현재시간
        private Button button3;
        private Button button4;
        private static System.Timers.Timer timer = new System.Timers.Timer();   //쓰레드 타이머
        private Thread thr;

        private static int count = 0;   //타이머 구별을 위한 카운터 변수

        //최상위 프로세스
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern Int32 GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);


        private static void HandleLogData(object sender, EventArgs e)   //로그데이터 데이터베이스에 삽입
        {
            currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            InsertLogData(currentDate, serverIP, clientIP, clientID, "접속"); //클라이언트 접속 시 로그데이터 insert
        }

        private static void HandleConnected(object sender, EventArgs e)
        {
            sessionList[count].session.timer = new System.Timers.Timer();   //세션리스트의 각각의 타이머 넣음
            sessionList[count].session.timer.Interval = 20000;              //20초마다
            sessionList[count].session.timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);     //함수 호출
            sessionList[count].session.timer.Start();                       //타이머실행

            count++;

            //접속했을때 서버 접속자 리스트뷰에 추가
            ListViewItem lvi = new ListViewItem();
            lvi.Text = clientIP;
            lvi.SubItems.Add(clientID);
            listView1.Items.Add(lvi);

            Console.WriteLine("Connected");
        }

        private static void HandleConnectionFailed(object sender, EventArgs e)
        {
            Console.WriteLine("Connection Failed");
        }

        private static void HandleClosed(object sender, EventArgs e)
        {
            //MessageBox.Show(((VncServerSession)sender).ip);   // 접속해제한 ip를 출력한다.

            //클라이언트 종료시 로그데이터 insert
            currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            for (int i = 0; i < sessionList.Count; i++)
            {
                if (sessionList[i].session.Equals((VncServerSession)sender))
                {
                    InsertLogData(currentDate, serverIP, sessionList[i].session.ip, sessionList[i].session.id, "종료");
                    --count;
                }
            }

            for (int i=0;i<listView1.Items.Count; i++)      // 리스트뷰아이템 
            {
               if(((VncServerSession)sender).ip == listView1.Items[i].SubItems[0].Text)
                {
                    listView1.Items.RemoveAt(i);
                    break;
                }
            }

            for(int i = 0; i < sessionList.Count; i++)       // sessionList 추방하기위해서 세션 리스트에서도 삭제
            {
                if(sessionList[i].session.Equals((VncServerSession)sender))
                {
                    sessionList.Remove(sessionList[i]);
                    break;
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
                TcpClient client = listener.AcceptTcpClient();   //AcceptTcpClient 접속하는 클라이언트에 대해 TCPClient 객체 생성

                // Set up a framebuffer and options.
                var options = new VncServerSessionOptions();
                options.AuthenticationMethod = AuthenticationMethod.Password;

                // Virtual mouse
                var mouse = new VncMouse();

                // Virtual keyboard
                var keyboard = new VncKeyboard();

                // Create a session.
                session = new VncServerSession();
                session.LogData += HandleLogData; //로그를 저장하기위해 새로추가
                session.Connected += HandleConnected;
                session.ConnectionFailed += HandleConnectionFailed;
                session.Closed += HandleClosed;
                session.PasswordProvided += HandlePasswordProvided;
                session.SetFramebufferSource(new VncScreenFramebufferSource("Hello World", Screen.PrimaryScreen));
                session.PointerChanged += mouse.OnMouseUpdate;
                session.KeyChanged += keyboard.OnKeyboardUpdate;
                session.Connect(client.GetStream(), options);


                //세션을 저장하는곳

                NetWorkItem item;
                item.session = session;
                item.tcpClient = client;
                sessionList.Add(item);

                // 접속한 ip따오는 
                Socket c = client.Client;
                IPEndPoint ip_point = (IPEndPoint)c.RemoteEndPoint;  //누가요청했는지 접근할수있게 IPEndPoint속성을 이용
                clientIP = ip_point.Address.ToString();

                session.ip = ip_point.Address.ToString();
                
                // MessageBox.Show(ip);
                clientID = session.UserData;
                session.id = clientID;
              // MessageBox.Show(userID);




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

        //로그 Insert 함수
        public static void InsertLogData(string currentDate, string serverIP, string clientIP, string clientID, string procname)
        {
            try
            {
                postParams = new StringBuilder();
                postParams.Append("CurrentDate=" + currentDate);    //현재시간
                postParams.Append("&ServerIp=" + serverIP);         //서버IP
                postParams.Append("&ClientIp=" + clientIP);         //클라이언트IP
                postParams.Append("&UserId=" + clientID);           //클라이언트ID
                postParams.Append("&ProcessState=" + procname);     //프로세스 상태

                string resultPost = PhpConnect("http://3men.pe.kr/insertLogData.php");
            }
            catch (Exception e)
            {
                Console.WriteLine("예외발생 : " + e.Message);
            }
        }

        //로그 Select 함수
        public static void SelectLogData(string selectip, string selectid)
        {
            try
            {
                postParams = new StringBuilder();
                postParams.Append("CurrentDate=" + currentDate);
                postParams.Append("&ServerIp=" + serverIP);

                string resultPost = PhpConnect("http://3men.pe.kr/insertLogData.php");
            }
            catch (Exception e)
            {
                Console.WriteLine("예외발생 : " + e.Message);
            }
        }

        //타이머간격마다 실행되는 함수
        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            IntPtr hwnd = GetForegroundWindow();    //최상위 실행중이 프로세스
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            Process curProc = Process.GetProcessById((int)pid);     //프로세스 객체
            string procname = curProc.ProcessName;  //프로세스 이름

            //타이머 간격마다 로그데이터 insert(현재 실행중인 최상위 프로세스)
            for (int i = 0; i < count; i++)
            {
                if (sessionList[i].session.timer.Equals(sender))
                {
                    InsertLogData(currentDate, serverIP, sessionList[i].session.ip, sessionList[i].session.id, procname);
                }
            }
        }

        public Form1()
        {
            InitializeComponent();

            //리스트뷰 옵션
            listView1.View = View.Details;
            listView1.GridLines = true;
            // listView1.FullRowSelect = true;

            serverIP = GetLocalIP();  //서버ip가져오기
            code = GetRandomCode(7);  //코드 난수생성
            password = code;
            tb_code.Text = code;

            UpdateServerIP(serverIP, code);  //코드 Update 함수 호출
            listener.Start();

             thr = new Thread(new ThreadStart(ListenerThread)); //쓰레드시작 1대N
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
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
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
            this.button1.Location = new System.Drawing.Point(12, 266);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 39);
            this.button1.TabIndex = 3;
            this.button1.Text = "강퇴";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(113, 266);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 39);
            this.button2.TabIndex = 4;
            this.button2.Text = " 전체로그  텍스트생성";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 322);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(82, 39);
            this.button3.TabIndex = 6;
            this.button3.Text = " 개별로그   텍스트생성";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(113, 322);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(82, 39);
            this.button4.TabIndex = 7;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 373);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
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

        private void button1_Click(object sender, EventArgs e)  //버튼클릭시 강퇴
        {
            for(int i = 0; i < sessionList.Count; i++)
            {
                if(listView1.FocusedItem.SubItems[0].Text == sessionList[i].session.ip)
                {
                    sessionList[i].tcpClient.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string savePath = @"C:\Users\bit\Desktop\log\ALLlogData.text";
            string resultPost = PhpConnect("http://3men.pe.kr/allselectlog.php");
            //string textValue = "텍스트파일입력";
            System.IO.File.WriteAllText(savePath, resultPost, Encoding.ASCII);


        }

        private void button3_Click(object sender, EventArgs e)
        {
            string savePath = @"C:\Users\bit\Desktop\log\logData.text";
            string selectip = (listView1.FocusedItem.SubItems[0].Text);
            string selectid = (listView1.FocusedItem.SubItems[1].Text);

            try
            {
                postParams = new StringBuilder();
                postParams.Append("ClientIp=" + selectip);
                postParams.Append("&UserId=" + selectid);

                string resultPost = PhpConnect("http://3men.pe.kr/Focuseselectlog.php");
                System.IO.File.WriteAllText(savePath, resultPost, Encoding.ASCII);
            }
            catch (Exception excep)
            {
                Console.WriteLine("예외발생 : " + excep.Message);
            }



        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
