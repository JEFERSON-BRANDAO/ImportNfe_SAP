using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SAP.Middleware.Connector;
using System.Data.OleDb;
using System.Globalization;  // SAP .NET Connector

// ===============================
// AUTHOR       : JEFFERSON BRANDÃO DA COSTA - ANALISTA/PROGRAMADOR
// CREATE DATE  : 06/07/2017  DD/MM/YYYY
// DESCRIPTION  : Importar dados referente as NOTAS FISCAIS do SAP para as tabelas [R_TO_HEAD,  R_DN_DETAIL, R_TO_DETAIL] para tornar disponível no SFC
// SPECIAL NOTES: altear leitura das conexoe datareader para dataset 
// ===============================
// Change History: version 1.0.3
// Date: 16/03/2018  DD/MM/YYYY
//==================================

namespace ImportNfe_SAP
{
    public partial class DownLoadNfe : Form
    {
        int statusConexao = 0;
        int segundos = 0;
        int checkPlanta = 1;

        Conexao stringConexao_ = new Conexao();
        string connectionString_ = string.Empty;

        //
        public DownLoadNfe()
        {
            InitializeComponent();

            connectionString_ = stringConexao_.Caminho();//string de conexão

            //this.Controls.Add(pictureBoxCarregando);
            //this.Controls.Add(timerCount); 
            //btnConectar.Location = new Point(175, 20);
            //Button1.Text = "Stretch";
            //btnConectar.Click += new EventHandler(btnConectar_Click);

            pictureBoxCarregando.Visible = false;
            lbAviso.Visible = false;
            //
            #region RODAPÉ

            int anoCriacao = 2017;
            int anoAtual = DateTime.Now.Year;
            string texto = anoCriacao == anoAtual ? " Foxconn CNSBG All Rights Reserved." : "-" + anoAtual + " Foxconn CNSBG All Rights Reserved.";
            //
            lbRodape.Text = "Copyright © " + anoCriacao + texto;

            #endregion
            //
            txtDescricao.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            lbStatus.Text = "DESCONECTADO";
            lbStatus.ForeColor = System.Drawing.Color.Red;
            //
            timerCount.Interval = 1000;
            timerCount.Start();

        }

        public void ConectarSAP(string Planta)
        {
            #region DADOS arquivo.txt
            //
            string IpServer = string.Empty;
            string Number = string.Empty;
            string User = string.Empty;
            string Password = string.Empty;
            string Client = string.Empty;
            string Cust = string.Empty;
            string Bu = string.Empty;
            //
            int ERRO = 0;
            string msgERRO = string.Empty;
            //
            try
            {
                string caminho = AppDomain.CurrentDomain.BaseDirectory + @"\CONFIG\configSAP.txt";
                string linha;
                int row = 0;
                //
                if (System.IO.File.Exists(caminho))
                {
                    System.IO.StreamReader arqTXT = new System.IO.StreamReader(caminho);
                    //
                    while ((linha = arqTXT.ReadLine()) != null)
                    {
                        if (row == 0)//primeira linha do .txt
                        {
                            for (int indice = 0; indice < linha.Length; indice++)
                            {
                                if (indice > 9)//IP_SERVER:xx.xx.xxx.xxx pega string a partir da décima posicao
                                {
                                    IpServer += linha[indice];
                                }

                            }
                        }
                        //
                        if (row == 1)//segunda linha do .txt
                        {
                            for (int indice = 0; indice < linha.Length; indice++)
                            {
                                if (indice > 6)
                                {
                                    Number += linha[indice];
                                }

                            }
                        }
                        //
                        if (row == 2)//terceira linha do .txt
                        {
                            for (int indice = 0; indice < linha.Length; indice++)
                            {
                                if (indice > 4)
                                {
                                    User += linha[indice];
                                }

                            }
                        }
                        //
                        if (row == 3)//quarta linha do .txt
                        {
                            for (int indice = 0; indice < linha.Length; indice++)
                            {
                                if (indice > 8)
                                {
                                    Password += linha[indice];
                                }

                            }
                        }
                        //
                        if (row == 4)//quinta linha do .txt
                        {
                            for (int indice = 0; indice < linha.Length; indice++)
                            {
                                if (indice > 6)
                                {
                                    Client += linha[indice];
                                }

                            }
                        }
                        //
                        if (row == 5)//sexta linha do .txt
                        {
                            for (int indice = 0; indice < linha.Length; indice++)
                            {
                                if (indice > 4)
                                {
                                    Cust += linha[indice];
                                }

                            }
                        }
                        //
                        if (row == 6)//sétima linha do .txt
                        {
                            for (int indice = 0; indice < linha.Length; indice++)
                            {
                                if (indice > 2)
                                {
                                    Bu += linha[indice];
                                }

                            }
                        }
                        //
                        row++;
                    }
                    //
                    arqTXT.Close();
                }
                else
                {
                    msgERRO = @"Arquivo \CONFIG\nomeBase.txt  não existe. Favor criar.";
                    ERRO++;
                }
            }
            catch (Exception erro)
            {
                msgERRO = erro.Message;
                ERRO++;
            }

            #endregion
            //
            if (ERRO == 0)
            {
                string date = txtDescricao.Text;
                DateTimeFormatInfo ukDtfi = new CultureInfo("pt-BR", false).DateTimeFormat;
                DateTime SqlDataInicio = new DateTime();
                DateTime.TryParse(date, ukDtfi, DateTimeStyles.None, out SqlDataInicio);
                //
                if (Convert.ToString(SqlDataInicio) == "1/1/0001 00:00:00")
                {
                    ERRO++;
                    msgERRO = "ERRO:: Data inválida!";
                }
                //
                if (ERRO == 0)
                {
                    try
                    {
                        #region CONEXÃO COM SAP

                        RfcConfigParameters parms = new RfcConfigParameters();
                        parms.Add(RfcConfigParameters.Name, "R/3");
                        parms.Add(RfcConfigParameters.AppServerHost, IpServer.Trim()); //The SAP host IP
                        parms.Add(RfcConfigParameters.SystemNumber, Number.Trim()); //The SAP instance
                        parms.Add(RfcConfigParameters.User, User.Trim()); //User name
                        parms.Add(RfcConfigParameters.Password, Password.Trim()); //Cipher
                        parms.Add(RfcConfigParameters.Client, Client.Trim()); // Client
                        parms.Add(RfcConfigParameters.Language, "EN"); //Logon language
                        parms.Add(RfcConfigParameters.PoolSize, "5");
                        //parms.Add(RfcConfigParameters.MaxPoolSize, "10");
                        parms.Add(RfcConfigParameters.IdleTimeout, "60");
                        //
                        RfcDestination prd = RfcDestinationManager.GetDestination(parms);
                        RfcRepository repo = prd.Repository;
                        IRfcFunction fn = repo.CreateFunction("ZRFC_SFC_NSG_0003E");
                        //
                        fn.SetValue("CREATE_DATE", SqlDataInicio.ToString("yyyy-MM-dd").Replace("-", ""));
                        fn.SetValue("TONO", "");
                        //
                        //string planta = string.Empty;
                        //
                        //if (rbt451F.Checked)
                        //{
                        //    Planta = "451F";
                        //}
                        //else if (rbt451T.Checked)
                        //{
                        //    Planta = "451T";
                        //}
                        //
                        fn.SetValue("PLANT", Planta);
                        fn.SetValue("CUST", Cust);
                        fn.SetValue("BU", Bu);
                        fn.Invoke(prd);

                        #endregion
                        //
                        IRfcTable SD_TO_HEAD = fn.GetTable("SD_TO_HEAD");
                        IRfcTableView view = (SD_TO_HEAD as ISupportTableView).DefaultView;
                        //this.dataGridView1.DataSource = view;

                        IRfcTable SD_DN_DETAIL = fn.GetTable("SD_DN_DETAIL");
                        IRfcTableView view2 = (SD_DN_DETAIL as ISupportTableView).DefaultView;
                        this.dataGridView1.DataSource = view2;

                        List<string> DN_list = new List<string>();
                        string dn = string.Empty;
                        //
                        for (int linhaLista = 0; linhaLista < this.dataGridView1.Rows.Count - 1; linhaLista++)
                        {
                            dn = this.dataGridView1.Rows[linhaLista].Cells["VBELN"].Value.ToString();
                            if (!DN_list.Contains(dn))//nao permite add dn duplicada na lista
                                DN_list.Add(dn);
                        }

                        int linhaTabela = view.Table.RowCount;
                        //
                        if (linhaTabela > 0)//se existe registro
                        {
                            int index = 0;
                            int notaExistente = 0;
                            int Naoinseriu_R_TO_DETAIL = 0;
                            string inseriu = string.Empty;
                            //
                            foreach (IRfcStructure row in SD_TO_HEAD)
                            {
                                string to_no = row.GetValue("TKNUM").ToString();
                                string to_create = string.IsNullOrEmpty(row.GetValue("ERDAT").ToString()) ? "" : Convert.ToDateTime(row.GetValue("ERDAT").ToString()).ToString("yyyy-MM-dd");
                                string containerNo = row.GetValue("SIGNI").ToString();
                                string vehicleNo = row.GetValue("EXTI2").ToString();
                                string extenalNo = row.GetValue("EXTI1").ToString();
                                string shipType = row.GetValue("SHTYP").ToString();
                                int dropFlag = 0;
                                string plant = row.GetValue("WERKS").ToString();
                                string dn_customer = row.GetValue("KUNAG").ToString();

                                //
                                inseriu = insertR_TO_HEAD(to_no, to_create, containerNo, vehicleNo, extenalNo, shipType, dropFlag, plant).ToString();
                                //
                                if (inseriu == "1")
                                {
                                    string dn_no = string.Empty;
                                    string to_item_no = string.Empty;
                                    string so_no = string.Empty;
                                    //
                                    dn_no = DN_list[index];
                                    to_item_no = "0001";

                                    if (insertR_TO_DETAIL(to_no, to_item_no, dn_no, dn_customer) == 1)
                                    {
                                        //ok
                                        Naoinseriu_R_TO_DETAIL = 0;
                                    }
                                    else
                                    {
                                        Naoinseriu_R_TO_DETAIL++;// caso ocorra algum problema na insercao  nao permitir salvar na tabela [R_DN_DETAIL]
                                    }
                                    //
                                    notaExistente = 0;

                                }
                                else
                                {
                                    notaExistente++;
                                }
                                //
                                index++;

                            }
                            //
                            if (notaExistente == 0)
                            {
                                if (Naoinseriu_R_TO_DETAIL == 0)//permite salvar pois nao houve nenhum problema na insercao na tabela anterior [R_TO_DETAIL]
                                {
                                    #region insert R_DN_DETAIL

                                    int Naoinseriu_R_DN_DETAIL = 0;
                                    int inseriu_R_DN_DETAIL = 0;
                                    //                                  
                                    foreach (IRfcStructure row in SD_DN_DETAIL)
                                    { 
                                        string dn_No = row.GetValue("VBELN").ToString();
                                        string to_item_No = row.GetValue("POSNR").ToString();
                                        string p_no = row.GetValue("MATNR").ToString();
                                        string p_no_desc = row.GetValue("ARKTX").ToString();
                                        string wherehouse = row.GetValue("LGORT").ToString();
                                        string net_weight = row.GetValue("NTGEW").ToString().Replace(",", ".");//
                                        string gross_weight = row.GetValue("BRGEW").ToString().Replace(",", ".");//
                                        string volume = row.GetValue("VOLUM").ToString().Replace(",", ".");//;
                                        string price = row.GetValue("KBETR").ToString().Replace(",", ".");//
                                        string p_no_qty = row.GetValue("LFIMG").ToString().Replace(",", ".");//
                                        string so_no = row.GetValue("VGBEL").ToString();
                                        string so_item_no = row.GetValue("VGPOS").ToString();
                                        string p_no2 = row.GetValue("BSTKD").ToString();
                                        string unit = row.GetValue("GEWEI").ToString();
                                        string SHIPTYPE = "TL";
                                        //
                                        #region insert R_DN_DETAIL

                                        if (string.IsNullOrEmpty(existeDadoR_DN_DETAIL(dn_No, to_item_No)))
                                        {
                                            if (insertR_DN_DETAIL(dn_No, to_item_No, p_no, p_no_desc, wherehouse, net_weight, gross_weight, volume, price, p_no_qty, so_no, so_item_no, p_no2, unit, SHIPTYPE) == 1)
                                            {
                                                //ok
                                                inseriu_R_DN_DETAIL++;
                                            }
                                            else
                                            {
                                                Naoinseriu_R_DN_DETAIL++;
                                            }
                                        }
                                        else
                                        {
                                            Naoinseriu_R_DN_DETAIL++;
                                        }

                                        #endregion

                                    }

                                    #endregion
                                    //
                                    lbAviso.Visible = true;
                                    lbAviso.Text = (inseriu_R_DN_DETAIL + " Item(ns) Nota(s) fora(m) baixada(s) com sucesso!  Total de item(ns) " + Naoinseriu_R_DN_DETAIL + " com problema para ser baixado(s");
                                    //MessageBox.Show(inseriu_R_DN_DETAIL + " Item(ns) Nota(s) fora(m) baixada(s) com sucesso!  Total de item(ns) " + Naoinseriu_R_DN_DETAIL + " com problema para ser baixado(s");

                                }
                            }
                            else
                            {
                                //Desconectado();
                                //
                                if (inseriu != "12154") //(inseriu != "ERRO:ORA-12154: TNS:could not resolve service name")
                                {
                                    lbAviso.Visible = true;
                                    lbAviso.Text = ("AVISO:: A(s) " + notaExistente + " Nota(s) referente à esta data, já fora(m) baixada(s) do SAP");
                                    //MessageBox.Show("AVISO:: A(s) " + notaExistente + " Nota(s) referente à esta data, já fora(m) baixada(s) do SAP", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                }
                            }
                        }
                        else
                        {
                            //Desconectado();
                            //
                            lbAviso.Visible = true;
                            lbAviso.Text = ("AVISO:: Não há nenhum registro encontrado nesta data!");
                            // MessageBox.Show("AVISO:: Não há nenhum registro encontrado nesta data!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    catch (Exception erro)
                    {
                        //Desconectado();
                        //
                        lbAviso.Visible = true;
                        lbAviso.Text = ("ERRO:: " + erro.Message);
                        //MessageBox.Show("ERRO:: " + erro.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    txtDescricao.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");//atualiza data para dia seguinte

                }
                else
                {
                    //Desconectado();
                    //
                    lbAviso.Visible = true;
                    lbAviso.Text = ("ERRO:: " + msgERRO);
                    //MessageBox.Show("ERRO:: " + msgERRO, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                //Desconectado();
                //
                lbAviso.Visible = true;
                lbAviso.Text = ("ERRO:: " + msgERRO);
                //MessageBox.Show("ERRO:: " + msgERRO, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public int insertR_TO_HEAD(string to_no, string to_createTime, string container_no, string vehicle_no, string extenal_no, string ship_type, int drop_flag, string plant)
        {
            string msg = string.Empty;
            string resultado = string.Empty;
            int status = 0;
            //
            #region Verifica se ja existe TO_NO na tabela R_TO_HEAD

            string sql = "select to_no from r_to_head  where to_no = '" + to_no + "'";
            // 
            OleDbConnection con = new OleDbConnection(connectionString_);
            DataSet oDs = new DataSet();
            //
            try
            {
                //
                con.Open();
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                //OleDbDataReader reader;
                OleDbDataAdapter oDA = new OleDbDataAdapter(cmd);
                //DataSet oDs = new DataSet();
                oDA.Fill(oDs);
                //
                if (oDs.Tables[0].Rows.Count > 0)
                {
                    resultado = oDs.Tables[0].Rows[0]["to_no"].ToString();
                    msg = resultado;
                }
                else
                {
                    //permitido salvar, pois não existe
                    resultado = string.Empty;
                    msg = resultado;
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                status = 12154;//Problema com conexao

                lbAviso.Visible = true;
                lbAviso.Text = ("ERRO:" + msg);
                //MessageBox.Show("ERRO:" + msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                con.Close();
                oDs.Clear();
            }

            #endregion
            //
            if (string.IsNullOrEmpty(resultado))
            {
                #region insert R_TO_HEAD

                string sql1 = @"insert into r_to_head(TO_NO, TO_CREATETIME, CONTAINER_NO, VEHICLE_NO, EXTERNAL_NO, SHIP_TYPE, DROP_FLAG, PLANT)
                                              values('" + to_no.Trim() + "','" + to_createTime + "','" + container_no + "','" + vehicle_no + "','" + extenal_no + "','" + ship_type + "', '" + drop_flag + "', '" + plant.Trim() + "')";
                //
                OleDbConnection con1 = new OleDbConnection(connectionString_);
                int inseriu = 0;
                //
                try
                {

                    con1.Open();
                    OleDbCommand cmd1 = new OleDbCommand(sql1, con1);
                    cmd1.CommandType = CommandType.Text;
                    //
                    inseriu = cmd1.ExecuteNonQuery();
                    //
                    if (inseriu > 0)
                    {
                        status = 1;
                    }
                    else
                    {
                        //Desconectado();
                        //
                        msg = inseriu.ToString();

                        lbAviso.Visible = true;
                        lbAviso.Text = ("ERRO:" + msg);

                        //MessageBox.Show("ERRO:" + msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    //Desconectado();
                    //
                    msg = ex.Message;

                    lbAviso.Visible = true;
                    lbAviso.Text = ("ERRO:" + msg);

                    //MessageBox.Show("ERRO:" + msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                finally
                {
                    con1.Close();
                }

                #endregion
            }
            else
            {
                lbAviso.Visible = true;
                lbAviso.Text = (to_no + " Já existe!");
                // MessageBox.Show(to_no + " Já existe!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return status;

        }

        public string existeItens(string dn_no)
        {
            #region Verifica se ja existe TO_NO na tabela R_TO_DETAIL

            //Conexao stringConexao = new Conexao();
            //string connectionString = stringConexao.Conectar();
            //
            string sql = "select dn_no from R_TO_DETAIL  where  dn_no = '" + dn_no + "'";

            string msg = string.Empty;
            string resultado = string.Empty;
            //
            OleDbConnection con = new OleDbConnection(connectionString_);
            DataSet oDs = new DataSet();
            //
            try
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                //OleDbDataReader reader;
                OleDbDataAdapter oDA = new OleDbDataAdapter(cmd);
                //
                oDA.Fill(oDs);
                //
                if (oDs.Tables[0].Rows.Count > 0)
                {
                    resultado = oDs.Tables[0].Rows[0]["dn_no"].ToString();
                    msg = resultado;
                }
                else
                {
                    //permitido salvar, pois não existe
                    resultado = string.Empty;
                    msg = resultado;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                lbAviso.Visible = true;
                lbAviso.Text = ("ERRO:" + msg);
                //MessageBox.Show("ERRO:" + msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                con.Close();
                oDs.Clear();
            }

            return msg;

            #endregion
        }

        public int insertR_TO_DETAIL(string to_no, string to_item_no, string dn_no, string dn_customer)
        {
            #region insert R_TO_DETAIL

            string msg = string.Empty;
            string sql = string.Empty;

            sql = @"insert into R_TO_DETAIL (TO_NO,  TO_ITEM_NO,  DN_NO,  DN_CUSTOMER)
                                              values('" + to_no.Trim() + "','" + to_item_no.Trim() + "','" + dn_no.Trim() + "','" + dn_customer + "')";

            //
            OleDbConnection con = new OleDbConnection(connectionString_);
            //
            int inseriu = 0;
            int status = 0;
            //
            try
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                //
                inseriu = cmd.ExecuteNonQuery();
                //
                if (inseriu > 0)
                {
                    status = 1;
                }
                else
                {
                    msg = inseriu.ToString();
                    //
                    lbAviso.Visible = true;
                    lbAviso.Text = ("ERRO:" + msg);
                    //MessageBox.Show("ERRO:" + msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                //
                lbAviso.Visible = true;
                lbAviso.Text = ("ERRO:" + msg);
                //MessageBox.Show("ERRO:" + msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }

            return status;

            #endregion
        }

        public string existeDadoR_DN_DETAIL(string dn_no, string dn_item_no)
        {
            #region Verifica se ja existe dados na tabela R_DN_DETAIL
            //
            string sql = "select DN_NO from R_DN_DETAIL  where DN_NO = '" + dn_no + "'  and DN_ITEM_NO ='" + dn_item_no + "'";

            string msg = string.Empty;
            string resultado = string.Empty;
            //
            OleDbConnection con = new OleDbConnection(connectionString_);
            DataSet oDs = new DataSet();
            //
            try
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                OleDbDataAdapter oDA = new OleDbDataAdapter(cmd);
                //
                oDA.Fill(oDs);
                //
                if (oDs.Tables[0].Rows.Count > 0)
                {
                    resultado = oDs.Tables[0].Rows[0]["DN_NO"].ToString();
                    msg = resultado;
                }
                else
                {
                    //permitido salvar, pois não existe
                    resultado = string.Empty;
                    msg = resultado;
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                //
                lbAviso.Visible = true;
                lbAviso.Text = ("ERRO:" + msg);
                //MessageBox.Show("ERRO:" + msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
                oDs.Clear();
            }

            return msg;

            #endregion
        }

        public int insertR_DN_DETAIL(string dn_no, string dn_item_no, string p_no, string p_no_desc, string warehouse, string net_weight, string gross_weight, string volume, string price, string p_no_qty, string so_no, string so_item_no, string p_no2, string unit, string shiptype)
        {
            #region insert R_DN_DETAIL

            string msg = string.Empty;
            string sql = @"insert into R_DN_DETAIL (DN_NO,  
                                                    DN_ITEM_NO, 
                                                    P_NO, 
                                                    P_NO_DESC, 
                                                    WAREHOUSE, 
                                                    NET_WEIGHT, 
                                                    GROSS_WEIGHT, 
                                                    VOLUME, 
                                                    PRICE, 
                                                    P_NO_QTY, 
                                                    SO_NO,
                                                    SO_ITEM_NO, 
                                                    PO_NO,
                                                    UNIT, 
                                                    SHIPTYPE)
                                             values('" + dn_no.Trim() + "','"
                                                       + dn_item_no.Trim() + "','"
                                                       + p_no.Trim() + "','"
                                                       + p_no_desc.Trim() + "','"
                                                       + warehouse.Trim() + "','"
                                                       + net_weight.Trim() + "','"
                                                       + gross_weight.Trim() + "','"
                                                       + volume.Trim() + "','"
                                                       + price.Trim() + "','"
                                                       + p_no_qty.Trim() + "','"
                                                       + so_no.Trim() + "','"
                                                       + so_item_no.Trim() + "','"
                                                       + p_no2.Trim() + "','"
                                                       + unit.Trim() + "','"
                                                       + shiptype + "')";
            //
            OleDbConnection con = new OleDbConnection(connectionString_);
            //
            int inseriu = 0;
            int status = 0;
            //
            try
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                //
                inseriu = cmd.ExecuteNonQuery();
                //
                if (inseriu > 0)
                {
                    status = 1;
                }
                else
                {
                    msg = inseriu.ToString();
                    //
                    lbAviso.Visible = true;
                    lbAviso.Text = ("ERRO:" + msg);

                    //MessageBox.Show("ERRO:" + msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                //
                lbAviso.Visible = true;
                lbAviso.Text = ("ERRO:" + msg);
                //MessageBox.Show("ERRO:" + msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                con.Close();
            }

            return status;

            #endregion
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            pictureBoxCarregando.Visible = false;
            //
            lbStatus.Text = "AGUARDANDO CONEXÃO...";
            lbStatus.ForeColor = System.Drawing.Color.Red;
            //
            timerCount.Start();
            statusConexao = 1;
        }

        private void timerCount_Tick(object sender, EventArgs e)
        {
            timerCount.Stop();
            timerCount.Start();
            //
            string planta = string.Empty;

            if (statusConexao == 1)
            {
                lbtime.Text = segundos.ToString() + ":s";
                segundos = segundos + 1;
                //
                if (segundos == 60)// 60 segundo = 1 minuto
                {
                    timerCount.Stop();
                    pictureBoxCarregando.Visible = true;
                    lbStatus.Text = "CONECTADO";
                    lbStatus.ForeColor = System.Drawing.Color.Blue;
                    timerCount.Start();
                    //
                    if (checkPlanta == 1)
                    {
                        rbt451F.Checked = true;
                        planta = "451F";
                        //
                        checkPlanta = 2; //retorna para planta 451F"
                    }
                    else if (checkPlanta == 2)
                    {
                        rbt451T.Checked = true;
                        planta = "451T";
                        //
                        checkPlanta = 1;//retorna para planta 451F"
                    }
                    //
                    ////txtDescricao.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");//atualiza data para dia seguinte
                    //
                    #region CONEXAO COM SAP
                    //
                    ConectarSAP(planta);
                    #endregion
                }
                //
                if (segundos == 70)//Após 70 segundos, exibe resultado durante 10 segundos
                {
                    pictureBoxCarregando.Visible = true;
                    segundos = 0;
                    lbtime.Text = "0:s";
                    //
                    Desconectado();
                }

            }
            else
            {
                this.dataGridView1.DataSource = null;
                pictureBoxCarregando.Visible = false;
                //
                lbStatus.Text = "DESCONECTADO";
                lbStatus.ForeColor = System.Drawing.Color.Red;
                //
                statusConexao = 0;
                segundos = 0;
                lbtime.Text = "0:s";
            }
        }

        public void Desconectado()
        {
            lbAviso.Visible = false;
            //
            timerCount.Stop();
            this.dataGridView1.DataSource = null;
            pictureBoxCarregando.Visible = false;
            lbStatus.Text = "DESCONECTADO";
            lbStatus.ForeColor = System.Drawing.Color.Red;
            //
            lbtime.Text = "0:s";
            //statusConexao = 0;
            segundos = 0;
            timerCount.Start();
        }

        private void btnParar_Click(object sender, EventArgs e)
        {
            lbAviso.Visible = false;
            //
            timerCount.Stop();
            this.dataGridView1.DataSource = null;
            pictureBoxCarregando.Visible = false;
            lbStatus.Text = "PARADO";
            lbStatus.ForeColor = System.Drawing.Color.Red;
            //
            lbtime.Text = "0:s";          
            segundos = 0;
         
        }
    }
}
