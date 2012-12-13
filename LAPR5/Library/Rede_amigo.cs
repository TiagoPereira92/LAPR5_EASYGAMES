using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Library
{
    public class Rede_amigo : ActiveRecord
    {

        private string _tagRelacao;
        private int _forcaLigacao;
        private int _codUtilizador;
        private int _codAmigo;
        private int i = 0;

        public Rede_amigo()
        {

        }

        public Rede_amigo(string tag, int forca, int cod_util, int cod_amigo)
        {
            this.TAGRELACAO = tag;
            this.FORCALIGACAO = forca;
            this.CODUTILIZADOR = cod_util;
            this.CODAMIGO = cod_amigo;

        }

        public string TAGRELACAO
        {
            get { return _tagRelacao; }
            set { this._tagRelacao = value; }
        }
        public int FORCALIGACAO
        {
            get { return _forcaLigacao; }
            set { this._forcaLigacao = value; }
        }
        public int CODUTILIZADOR
        {
            get { return _codUtilizador; }
            set { this._codUtilizador = value; }
        }
          public int CODAMIGO
        {
            get { return _codAmigo; }
            set { this._codAmigo = value; }
        }


          protected Rede_amigo(DataRow row)
        {
            this._tagRelacao = (string)row["Tag_relacao"];
            this._forcaLigacao = (int)row["Forca_ligacao"];
            this._codUtilizador = (int)row["Cod_utilizador"];
            this._codAmigo = (int)row["Cod_amigo"];
        }

        public static Rede_amigo LoadById(int codUtil)
        {
            DataSet ds = ExecuteQuery("select * from Rede_amigo where Cod_utilizador=" + codUtil, getConnection(false));
            if (ds.Tables[0].Rows.Count != 1)
            {
                return null;
            }
            else
            {
                return new Rede_amigo(ds.Tables[0].Rows[0]);
            }
        }

        public static IList LoadAll()
        {
            try
            {

                DataSet ds = ExecuteQuery("Select * From Rede_amigo", getConnection(true));
                IList list = new ArrayList();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Rede_amigo rede= new Rede_amigo(row);
                    list.Add(rede);
                }
                return list;

            }
            catch (Exception e)
            {
                throw new ApplicationException("ErroBD", e);
            }
        }



        public override void Save()
        {
            BeginTransaction();
            SqlCommand sqlRede_amigo = new SqlCommand();

            if (i != 0)
            {
                sqlRede_amigo.CommandText = "UPDATE Rede_amigo SET Tag_relacao=@tr and Forca_ligacao=@fl and Cod_amigo=@codam WHERE Cod_utilizador=@codUtil";
            }
            else
            {
                sqlRede_amigo.CommandText = "INSERT INTO Rede_amigo(Tag_relacao,Forca_ligacao,Cod_utilizador,Cod_amigo) VALUES (@tr,@fl,@codam,@codUtil)";
                i = 1;
            }
            sqlRede_amigo.Transaction = CurrentTransaction;

            SqlParameter tagR = new SqlParameter("@tr", SqlDbType.NVarChar);
            tagR.Value = _tagRelacao;
            sqlRede_amigo.Parameters.Add(tagR);

            SqlParameter forcaL = new SqlParameter("@fl", SqlDbType.Int);
            forcaL.Value = _forcaLigacao;
            sqlRede_amigo.Parameters.Add(forcaL);

            SqlParameter codAm = new SqlParameter("@codam", SqlDbType.Int);
            codAm.Value = _codAmigo;
            sqlRede_amigo.Parameters.Add(codAm);

            SqlParameter codUtil = new SqlParameter("@codUtil", SqlDbType.Int);
            codUtil.Value = _codUtilizador;
            sqlRede_amigo.Parameters.Add(codUtil);


            int id = ExecuteTransactedNonQuery(sqlRede_amigo);
            CommitTransaction();

        }


        public void delete()
        {
            BeginTransaction();
            SqlCommand sqlRede_amigo = new SqlCommand();

            sqlRede_amigo.CommandText = "delete from Rede_amigo where Cod_utilizador=@codUtil";
            sqlRede_amigo.Transaction = CurrentTransaction;

            SqlParameter codUtilizador = new SqlParameter("@codUtil", SqlDbType.Int);
            codUtilizador.Value = _codUtilizador;
            sqlRede_amigo.Parameters.Add(codUtilizador);

            int id = ExecuteTransactedNonQuery(sqlRede_amigo);
            CommitTransaction();

        }


    }
}