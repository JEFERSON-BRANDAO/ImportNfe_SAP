using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImportNfe_SAP
{
    class Conexao
    {
        #region CRIPTOGRAFIA

        protected int Asc(string letra)
        {
            return (int)(Convert.ToChar(letra));
        }
        //
        protected char Chr(int codigo)
        {
            return (char)codigo;
        }
        //
        protected string EnCode(string pStr)
        {
            int j = pStr.Length;
            string lStr = string.Empty;
            char lChar;
            int lAsc;
            //
            for (int i = 0; i < j; i++)
            {
                lAsc = Convert.ToInt32(Asc(pStr.Substring(i, 1)));

                if (lAsc > 32 && lAsc <= 100)
                {
                    lAsc = lAsc + 22;
                }
                else if (lAsc > 100 && lAsc <= 122)
                {
                    lAsc = lAsc - 100 + 32;
                }
                //
                lChar = Chr(lAsc);
                lStr = lStr + lChar;
            }
            //
            return lStr;
        }

        protected string DeCode(string pStr)
        {
            int j = pStr.Length;
            string lStr = string.Empty;
            char lChar;
            int lAsc;
            //
            for (int i = 0; i < j; i++)
            {
                lAsc = Convert.ToInt32(Asc(pStr.Substring(i, 1)));

                if (lAsc > 54 && lAsc <= 122)
                {
                    lAsc = lAsc - 22;
                }
                else if (lAsc > 32 && lAsc <= 54)
                {
                    lAsc = 100 + lAsc - 32;//lAsc - 100 - 32;
                }
                //
                lChar = Chr(lAsc);
                lStr = lStr + lChar;
            }
            //
            return lStr;
        }

        #endregion
        //
        public string Caminho()
        {
            #region STRING DE CONEXÃO

            string dataBase = string.Empty;
            string usuario = string.Empty;
            string senha = string.Empty;
            //
            try
            {
                string caminho = AppDomain.CurrentDomain.BaseDirectory + @"\CONFIG\conexao.txt";
                string linha;
                int row = 0;
                //
                if (System.IO.File.Exists(caminho))
                {
                    System.IO.StreamReader arqTXT = new System.IO.StreamReader(caminho);
                    //
                    while ((linha = arqTXT.ReadLine()) != null)
                    {
                        #region DATABASE

                        if (row == 0)//1ra linha do .txt
                        {
                            for (int indice = 0; indice < linha.Length; indice++)
                            {
                                if (indice > 8)
                                {
                                    dataBase += linha[indice];
                                }
                            }
                        }

                        #endregion
                        //
                        #region USUÁRIO

                        if (row == 1)//2da linha do .txt
                        {
                            for (int indice = 0; indice < linha.Length; indice++)
                            {
                                if (indice > 4)
                                {
                                    usuario += linha[indice];
                                }
                            }
                        }

                        #endregion
                        //
                        #region SENHA

                        if (row == 2)//3ra linha do .txt
                        {
                            for (int indice = 0; indice < linha.Length; indice++)
                            {
                                if (indice > 3)
                                {
                                    senha += linha[indice];
                                }
                            }
                        }

                        #endregion
                        //
                        row++;
                    }
                    //
                    arqTXT.Close();
                }
            }
            catch
            {
                //
            }
            //            
            return "Provider=OraOLEDB.Oracle;Data Source=" + DeCode(dataBase.Trim()) + ";User Id=" + DeCode(usuario.Trim()) + ";Password=" + DeCode(senha.Trim()) + ";";

            #endregion
        }
    }
}
