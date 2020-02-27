using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExchangeRate
{
    public partial class ExchangeRate : Form
    {
        public DateTime gDt = DateTime.Now;
        //public DateTime gDt = DateTime.Now.AddDays(5);

        public ExchangeRate()
        {
            InitializeComponent();
        }

        private void ExchangeRate_Load(object sender, EventArgs e)
        {

            SetupDataGridView();


            using (System.Net.WebClient wc = new System.Net.WebClient())
            {
                wc.Encoding = System.Text.Encoding.UTF8;


                //gDt = DateTime.Now.AddDays(-2);

                bool xExe = true;
                string jsonURL = "";
                string json = "";
                JArray array = new JArray();
                while (xExe)
                {
                    jsonURL = "https://www.koreaexim.go.kr/site/program/financial/exchangeJSON?authkey=YSpdSDfWSuga85SwTalCACE2j6SGyRZ4&searchdate="
                                    + gDt.ToString("yyyyMMdd") + "&data=AP01";
                    json = wc.DownloadString(jsonURL); // API 사이트에서 json 받아옴
                    array = JArray.Parse(json);

                    if (array.Count() > 0)
                    {
                        xExe = false;
                    }
                    else
                    {
                        gDt = gDt.AddDays(-1);
                    }

                }

                string[] xRow = new string[10];

                foreach (JObject jobj in array)
                {
                    //if (jobj["result"].ToString() == "1")
                    //{
                        string xCurUnit = jobj["cur_unit"].ToString();
                        string xCurNm = jobj["cur_nm"].ToString();
                        string xTtb = jobj["ttb"].ToString();
                        string xTts = jobj["tts"].ToString();
                        string xDealBasR = jobj["deal_bas_r"].ToString();
                        string xBkpr = jobj["bkpr"].ToString();
                        string xYyEfeeR = jobj["yy_efee_r"].ToString();
                        string xTenDdEfeeR = jobj["ten_dd_efee_r"].ToString();
                        string xKftcDealBasR = jobj["kftc_deal_bas_r"].ToString();
                        string xKftcBkpr = jobj["kftc_bkpr"].ToString();

                        xRow = new string[] { xCurUnit, xCurNm, xTtb, xTts, xDealBasR, xBkpr, xYyEfeeR, xTenDdEfeeR, xKftcDealBasR, xKftcBkpr };

                        dataGridView1.Rows.Add(xRow);
                    //}

                    dataGridView1.Columns[0].DisplayIndex = 0;
                    dataGridView1.Columns[1].DisplayIndex = 1;
                    dataGridView1.Columns[2].DisplayIndex = 2;
                    dataGridView1.Columns[3].DisplayIndex = 3;
                    
                    //dataGridView1.AutoResizeColumns();


                }
            }

            lblDate.Text = gDt.ToString("yyyy.MM.dd");
        }

        private void SetupDataGridView()
        {
            DataGridView grd = dataGridView1;

            grd.ColumnCount = 10;

            grd.Columns[0].Name = "CUR_UNIT [통화코드]";
            grd.Columns[1].Name = "CUR_NM [국가/통화명]";
            grd.Columns[2].Name = "TTB [전신환(송금) - 받으실때]";
            grd.Columns[3].Name = "TTS [전신환(송금) - 보내실때]";
            grd.Columns[4].Name = "DEAL_BAS_R [매매 기준율]";
            grd.Columns[5].Name = "BKPR [장부가격]";
            grd.Columns[6].Name = "YY_EFEE_R [년환가료율]";
            grd.Columns[7].Name = "TEN_DD_EFEE_R [10일환가료율]";
            grd.Columns[8].Name = "KFTC_DEAL_BAS_R [서울외국환중계 - 매매기준율]";
            grd.Columns[9].Name = "KFTC_BKPR [서울외국환중계 - 장부가격]";

            grd.Columns[0].Width = 120;
            grd.Columns[1].Width = 140;
            grd.Columns[2].Width = 140;
            grd.Columns[3].Width = 140;
            grd.Columns[4].Width = 140;
            grd.Columns[5].Width = 120;
            grd.Columns[6].Width = 140;
            grd.Columns[7].Width = 140;
            grd.Columns[8].Width = 140;
            grd.Columns[9].Width = 140;
            



        }
    }
}
