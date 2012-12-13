using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Library
{
    public abstract class ActiveRecord
    {
        private const string connSTR = "Server=gandalf.dei.isep.ipp.pt;Database=ARQSI34;User Id=ARQSI34;Password=easygames;";

        public ActiveRecord()
        {

        }
        public abstract void Save();

        private static SqlTransaction myTx;

        protected static SqlTransaction CurrentTransaction
        {
            get { return myTx; }
        }

        protected static SqlConnection getConnection(bool open)
        {

            SqlConnection conn = new SqlConnection(connSTR);
            if (open)
                conn.Open();
            return conn;
        }

        protected static DataSet ExecuteQuery(string query, SqlConnection conn)
        {
            try
            {

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(ds);
                return ds;

            }
            catch (Exception e)
            {
                throw new ApplicationException("Erro BD", e);
            }
        }


        protected static int ExecuteNonQuery(string query, SqlConnection conn)
        {

            try
            {
                BeginTransaction();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.Connection.Open();
                return cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw new ApplicationException("Erro BD", e);
            }

        }

        protected static void BeginTransaction()
        {
            try
            {

                myTx = getConnection(true).BeginTransaction();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Erro BD", ex);
            }
        }

        protected static int ExecuteTransactedNonQuery(SqlCommand cmd)
        {
            try
            {
                cmd.Transaction = CurrentTransaction;
                cmd.Connection = CurrentTransaction.Connection;
                return cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                // debug purposes only!!!
                throw ex;
            }
        }

        protected static void CommitTransaction()
        {
            if (myTx != null)
            {
                SqlConnection cnx = myTx.Connection;
                myTx.Commit();
                cnx.Close();
            }
        }

    }



}