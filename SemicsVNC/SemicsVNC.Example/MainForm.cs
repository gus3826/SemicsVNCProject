#region License
/*
SemicsVNC VNC Client/Server Library for .NET
Copyright (c) 2013 James F. Bellinger <http://www.zer7.com/software/RemoteViewing>
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace SemicsVNC.Example
{
    /// <summary>
    /// The main form used in this sample application.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        /// 
        public MainForm(string myId)
        {
            this.InitializeComponent();
            //접속한 사람의 아이디를 다른클래스에서 사용하기위해서 프로퍼티사용
            this.vncControl.Client.UserID = myId;
        }

        private void OnClick(object sender, EventArgs e)
        {
            if (this.vncControl.Client.IsConnected)
            {
                this.vncControl.Client.Close();
            }
            else
            {
                //var hostname = this.txtHostname.Text.Trim();
                var hostname = "";
                //if (hostname == string.Empty)
                //{
                //    MessageBox.Show(
                //        this,
                //        "Hostname isn't set.",
                //        "Hostname",
                //        MessageBoxButtons.OK,
                //        MessageBoxIcon.Error);
                //    return;
                //}

                //int port;
                //if (!int.TryParse(this.txtPort.Text, out port) || port < 1 || port > 65535)
                int port=5900;
                if ( port < 1 || port > 65535)
                {
                    MessageBox.Show(
                        this,
                        "Port must be between 1 and 65535.",
                        "Port",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                var options = new Vnc.VncClientConnectOptions();
                if (this.txtPassword.Text != string.Empty)
                {


                    //php에서 로그인하기위해서 사용하는 코드

                    StringBuilder postParams = new StringBuilder();
                    //postParams.Append("id=" + textID.Text);
                    //postParams.Append("&pw=" + textPW.Text);

                    Encoding encoding = Encoding.UTF8;
                    byte[] result = encoding.GetBytes(postParams.ToString());


                    // 타겟이 되는 웹페이지 URL
                    string Url = "http://3men.pe.kr/InCodeOutIp.php?code=" + this.txtPassword.Text;
                    HttpWebRequest wReqFirst = (HttpWebRequest)WebRequest.Create(Url);

                    // HttpWebRequest 오브젝트 설정
                    wReqFirst.Method = "POST";
                    wReqFirst.ContentType = "application/x-www-form-urlencoded";
                    wReqFirst.ContentLength = result.Length;

                    Stream postDataStream = wReqFirst.GetRequestStream();
                    postDataStream.Write(result, 0, result.Length);
                    postDataStream.Close();

                    HttpWebResponse wRespFirst = (HttpWebResponse)wReqFirst.GetResponse();

                    // Response의 결과를 스트림을 생성합니다.
                    Stream respPostStream = wRespFirst.GetResponseStream();
                    StreamReader readerPost = new StreamReader(respPostStream, Encoding.Default);

                    // 생성한 스트림으로부터 string으로 변환합니다.
                    string resultPost = readerPost.ReadToEnd();
                    int resul_leng = resultPost.Length;
                    //MessageBox.Show(resultPost);
                    //
                    if (resultPost.Length >= 20)
                    {
                        string[] spl_result = resultPost.Split(new char[] { ':' });
                        string[] spl_result2 = spl_result[2].Split(new char[] { '"' });
                       // MessageBox.Show(spl_result2[1].Trim());
                        hostname = spl_result2[1].Trim();
                        options.Password = this.txtPassword.Text.ToCharArray();
                    }
                    else
                    {
                        MessageBox.Show("올바른 코드를 입력해주세요.");
                        return;
                    }
                }

                try
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        try
                        {
                            this.vncControl.Client.Connect(hostname, port, options);
                        }
                        finally
                        {
                            this.Cursor = Cursors.Default;
                        }
                    }
                    catch (Vnc.VncException ex)
                    {
                        MessageBox.Show(
                            this,
                            "Connection failed (" + ex.Reason.ToString() + ").",
                            "Connect",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                    catch (SocketException ex)
                    {
                        MessageBox.Show(
                            this,
                            "Connection failed (" + ex.SocketErrorCode.ToString() + ").",
                            "Connect",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    this.vncControl.Focus();
                }
                finally
                {
                    if (options.Password != null)
                    {
                        Array.Clear(options.Password, 0, options.Password.Length);
                    }
                }
            }
        }

        private void OnConnected(object sender, EventArgs e)
        {

            this.btnConnect.Text = "Close";
        }

        private void OnClosed(object sender, EventArgs e)
        {
            this.btnConnect.Text = "Connect";
            
        }

        private void OnConnectionFailed(object sender, EventArgs e)
        {
        }
    }
}
