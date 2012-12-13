using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Library
{
    public class Perfil_Externo : ActiveRecord
    {

        private int _codPerfilExt;
        private string _link;
        private int _codUtilizador;

        public Perfil_Externo ()
        {

        }

        public Perfil_Externo (int cod_perfilExt, string link,int cod_util)
        {
            this.CODPERFILEXT= cod_perfilExt;
            this.LINK = link;
            this.CODUTILIZADOR=cod_util;

        }

        public int CODPERFILEXT
        {
            get { return _codPerfilExt; }
            set { this._codPerfilExt = value; }
        }
        public string LINK
        {
            get { return _link; }
            set { this._link = value; }
        }
        public int CODUTILIZADOR
        {
            get { return _codUtilizador; }
            set { this._codUtilizador = value; }
        }



        protected Perfil_Externo(DataRow row)
        {
            this._codPerfilExt = (int)row["Cod_perfilExt"];
            this._link = (string)row["Link"];
            this._codUtilizador = (int)row["Cod_utilizador"];
        }

        public static Perfil_Externo LoadById(int perfilExtID)
        {
            DataSet ds = ExecuteQuery("select * from Perfil_Externo where Cod_perfilExt=" + perfilExtID, getConnection(false));
            if (ds.Tables[0].Rows.Count != 1)
            {
                return null;
            }
            else
            {
                return new Perfil_Externo(ds.Tables[0].Rows[0]);
            }
        }

        public static IList LoadAll()
        {
            try
            {

                DataSet ds = ExecuteQuery("Select * From Perfil_Externo", getConnection(true));
                IList list = new ArrayList();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Perfil_Externo estado = new Perfil_Externo(row);
                    list.Add(estado);
                }
                return list;

            }
            catch (Exception e)
            {
                throw new ApplicationException("ErroBD", e);
            }
        }



        public static int getProxID()
        {
            try
            {

                DataSet ds = ExecuteQuery("Select p.* from Perfil_Externo p where p.Cod_perfilExt like (select MAX(Cod_perfilExt) from Perfil_Externo)", getConnection(true));
                DataRow row = ds.Tables[0].Rows[0];
                Perfil_Externo perfil = new Perfil_Externo(row);
                return perfil.CODPERFILEXT + 1;
            }
            catch (Exception e)
            {
                throw new ApplicationException("ErroBD", e);
            }
        }



        public override void Save()
        {
            BeginTransaction();
            SqlCommand sqlPerfil_Externo= new SqlCommand();

            if (_codPerfilExt != 0)
            {
                sqlPerfil_Externo.CommandText = "UPDATE Perfil_Externo SET Link=@l and Cod_utilizador=@cod_util WHERE Cod_perfilExt=@cod_perfilExt";
            }
            else
            {
                _codPerfilExt = getProxID();
                sqlPerfil_Externo.CommandText = "INSERT INTO Perfil_Externo(Cod_perfilExt,Link,Cod_utilizador) VALUES (@cod_perfilExt,@l,@cod_util)";
            }
            sqlPerfil_Externo.Transaction = CurrentTransaction;

            SqlParameter codPExt = new SqlParameter("@cod_perfilExt", SqlDbType.Int);
            codPExt.Value = _codPerfilExt;
            sqlPerfil_Externo.Parameters.Add(codPExt);

            SqlParameter lk = new SqlParameter("@l", SqlDbType.NVarChar);
            lk.Value = _link;
            sqlPerfil_Externo.Parameters.Add(lk);

            SqlParameter codUtil= new SqlParameter("@cod_perfilExt", SqlDbType.Int);
            codUtil.Value = _codUtilizador;
            sqlPerfil_Externo.Parameters.Add(codUtil);

            int id = ExecuteTransactedNonQuery(sqlPerfil_Externo);
            CommitTransaction();

        }


        public void delete()
        {
            BeginTransaction();
            SqlCommand sqlPerfil_Externo = new SqlCommand();

            sqlPerfil_Externo.CommandText = "delete from Perfil_Externo where Cod_perfilExt=@cod_perfilExt";
            sqlPerfil_Externo.Transaction = CurrentTransaction;

            SqlParameter codPerfilExt= new SqlParameter("@cod_perfilExt", SqlDbType.Int);
            codPerfilExt.Value = _codPerfilExt;
            sqlPerfil_Externo.Parameters.Add(codPerfilExt);

            int id = ExecuteTransactedNonQuery(sqlPerfil_Externo);
            CommitTransaction();

        }


    }
}