using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SemicsVNC.Example
{
    public partial class Form1 : Form
    {
        private Label label2;
        private TextBox textBox_name;
        private TextBox textBox_pass;
        private Button button1;
        private Button button2;
        private Label label1;



        public Form1()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.textBox_pass = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 60);
            this.label1.Name = "lb_id";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "아이디";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 100);
            this.label2.Name = "lb_pw";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "비밀번호";
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(91, 57);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(107, 21);
            this.textBox_name.TabIndex = 2;
            // 
            // textBox_pass
            // 
            this.textBox_pass.Location = new System.Drawing.Point(91, 97);
            this.textBox_pass.Name = "textBox_pass";
            this.textBox_pass.PasswordChar = '*';
            this.textBox_pass.Size = new System.Drawing.Size(107, 21);
            this.textBox_pass.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(30, 173);
            this.button1.Name = "btn_login";
            this.button1.Size = new System.Drawing.Size(83, 32);
            this.button1.TabIndex = 4;
            this.button1.Text = "로그인";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(141, 173);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 32);
            this.button2.TabIndex = 5;
            this.button2.Text = "취소";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox_pass);
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            //php에서 로그인하기위해서 사용하는 코드

            StringBuilder postParams = new StringBuilder();
            //postParams.Append("id=" + textID.Text);
            //postParams.Append("&pw=" + textPW.Text);

            Encoding encoding = Encoding.UTF8;
            byte[] result = encoding.GetBytes(postParams.ToString());

            string id = textBox_name.Text;
            string pw = textBox_pass.Text;



            // 타겟이 되는 웹페이지 URL
            string Url = "http://3men.pe.kr/outidtest3.php?userid="+id+"&password="+pw;
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
            int resul_leng =resultPost.Length;




            //로그인(아이디 ,비밀번호 검사)
            if (resul_leng>=20)
            {
                //문자열 자르기 코드 주석
                //string[] spl_result = resultPost.Split(new char[] { ':' });
                //MessageBox.Show(resultPost);
                this.Hide();
                MainForm newForm = new MainForm(textBox_name.Text);
                    MessageBox.Show("Semics 원격제어 프로그램에 오신것을 환영합니다.");
                    newForm.ShowDialog();
                 

                this.Close();
                //   Application.EnableVisualStyles();
                //   Application.Run(new MainForm());



            }
            else
            {
                MessageBox.Show("아이디와 비밀번호를 확인해주세요");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

               Close();
        }
    }
}
