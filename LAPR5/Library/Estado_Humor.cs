using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Library
{
    public class Estado_Humor : ActiveRecord
    {

        private int _codEstado;
        private string _designacao;
  
        public Estado_Humor()
        {

        }

        public Estado_Humor(int cod_estado, string designacao)
        {
            this.CODESTADO = cod_estado;
            this.DESIGNACAO = designacao;
            
        }

        public int CODESTADO
        {
            get { return _codEstado; }
            set { this._codEstado = value; }
        }
        public string DESIGNACAO
        {
            get { return _designacao; }
            set { this._designacao = value; }
        }
       


        protected Estado_Humor(DataRow row)
        {
            this._codEstado = (int)row["Cod_estado"];
            this._designacao = (string)row["Designacao"];
        }

        public static Estado_Humor LoadById(int estadoID)
        {
            DataSet ds = ExecuteQuery("select * from Estado_Humor where Cod_estado=" + estadoID, getConnection(false));
            if (ds.Tables[0].Rows.Count != 1)
            {
                return null;
            }
            else
            {
                return new Estado_Humor(ds.Tables[0].Rows[0]);
            }
        }

        public static IList LoadAll()
        {
            try
            {

                DataSet ds = ExecuteQuery("Select * From Estado_Humor", getConnection(true));
                IList list = new ArrayList();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Estado_Humor estado = new Estado_Humor(row);
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

                DataSet ds = ExecuteQuery("Select e.* from Estado_Humor e where e.Cod_estado like (select MAX(Cod_estado) from Estado_Humor)", getConnection(true));
                DataRow row = ds.Tables[0].Rows[0];
                Estado_Humor estado = new Estado_Humor(row);
                return estado.CODESTADO + 1;
            }
            catch (Exception e)
            {
                throw new ApplicationException("ErroBD", e);
            }
        }



        public override void Save()
        {
            BeginTransaction();
            SqlCommand sqlEstado_Humor = new SqlCommand();

            if (_codEstado != 0)
            {
                sqlEstado_Humor.CommandText = "UPDATE Estado_Humor SET Designacao=@des WHERE Cod_estado=@codEst";
            }
            else
            {
                _codEstado= getProxID();
                sqlEstado_Humor.CommandText = "INSERT INTO Estado_Humor(Cod_estado,Designacao) VALUES (@codEst,@designacao)";
            }
            sqlEstado_Humor.Transaction = CurrentTransaction;

            SqlParameter codEstado = new SqlParameter("@codEst", SqlDbType.Int);
            codEstado.Value = _codEstado;
            sqlEstado_Humor.Parameters.Add(codEstado);

            SqlParameter design = new SqlParameter("@designacao", SqlDbType.NVarChar);
            design.Value = _designacao;
            sqlEstado_Humor.Parameters.Add(design);

            int id = ExecuteTransactedNonQuery(sqlEstado_Humor);
            CommitTransaction();

        }


        public void delete()
        {
            BeginTransaction();
            SqlCommand sqlEstado_Humor = new SqlCommand();

            sqlEstado_Humor.CommandText = "delete from Estado_Humor where Cod_estado=@codEst";
            sqlEstado_Humor.Transaction = CurrentTransaction;

            SqlParameter codEstado = new SqlParameter("@codEst", SqlDbType.Int);
            codEstado.Value = _codEstado;
            sqlEstado_Humor.Parameters.Add(codEstado);

            int id = ExecuteTransactedNonQuery(sqlEstado_Humor);
            CommitTransaction();

        }


    }
}