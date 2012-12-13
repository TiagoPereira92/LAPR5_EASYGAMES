using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Library
{
    public class Tag : ActiveRecord
    {

        private int _codTag;
        private string _designacao;

        public Tag()
        {

        }

        public Tag(int cod_tag, string designacao)
        {
            this.CODTAG= cod_tag;
            this.DESIGNACAO = designacao;

        }

        public int CODTAG
        {
            get { return _codTag; }
            set { this._codTag= value; }
        }
        public string DESIGNACAO
        {
            get { return _designacao; }
            set { this._designacao = value; }
        }



        protected Tag(DataRow row)
        {
            this._codTag = (int)row["Cod_tag"];
            this._designacao = (string)row["Designacao"];
        }

        public static Tag LoadById(int tagID)
        {
            DataSet ds = ExecuteQuery("select * from Tag where Cod_tag=" + tagID, getConnection(false));
            if (ds.Tables[0].Rows.Count != 1)
            {
                return null;
            }
            else
            {
                return new Tag(ds.Tables[0].Rows[0]);
            }
        }

        public static IList LoadAll()
        {
            try
            {

                DataSet ds = ExecuteQuery("Select * From Tag", getConnection(true));
                IList list = new ArrayList();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Tag t = new Tag(row);
                    list.Add(t);
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

                DataSet ds = ExecuteQuery("Select t.* from Tage where t.Cod_tag like (select MAX(Cod_tag) from Tag)", getConnection(true));
                DataRow row = ds.Tables[0].Rows[0];
                Tag t= new Tag(row);
                return t.CODTAG + 1;
            }
            catch (Exception e)
            {
                throw new ApplicationException("ErroBD", e);
            }
        }



        public override void Save()
        {
            BeginTransaction();
            SqlCommand sqlTag = new SqlCommand();

            if (_codTag != 0)
            {
                sqlTag.CommandText = "UPDATE Tag SET Designacao=@des WHERE Cod_tag=@codT";
            }
            else
            {
                _codTag = getProxID();
                sqlTag.CommandText = "INSERT INTO Tag(Cod_tag,Designacao) VALUES (@codT,@designacao)";
            }
            sqlTag.Transaction = CurrentTransaction;

            SqlParameter codT = new SqlParameter("@codT", SqlDbType.Int);
            codT.Value = _codTag;
            sqlTag.Parameters.Add(codT);

            SqlParameter design = new SqlParameter("@designacao", SqlDbType.NVarChar);
            design.Value = _designacao;
            sqlTag.Parameters.Add(design);

            int id = ExecuteTransactedNonQuery(sqlTag);
            CommitTransaction();

        }


        public void delete()
        {
            BeginTransaction();
            SqlCommand sqlTag = new SqlCommand();

            sqlTag.CommandText = "delete from Tagwhere Cod_tag=@codT";
            sqlTag.Transaction = CurrentTransaction;

            SqlParameter codT = new SqlParameter("@codT", SqlDbType.Int);
            codT.Value = _codTag;
            sqlTag.Parameters.Add(codT);

            int id = ExecuteTransactedNonQuery(sqlTag);
            CommitTransaction();

        }


    }
}