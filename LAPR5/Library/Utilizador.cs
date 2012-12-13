using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Library
{
    public class Utilizador : ActiveRecord
    {

        private int _codUtilizador;
        private string _nome;
        private DateTime _dataNascimento;
        private int _numeroTelefone;
        private string _email;
        private string _username;
        private string _password;
        private float _coordenadaX;
        private float _coordenadaY;
        private int _codEstado;

        public Utilizador()
        {

        }

        public Utilizador(int cod_util, string nome,DateTime dataNascimento,int numeroTelefone, string email, string username, string password, float cX, float cY,int codEstado)
        {
            this.CODUTILIZADOR = cod_util;
            this.NOME = nome;
            this.DATANASCIMENTO=dataNascimento;
            this.NUMEROTELEFONE=numeroTelefone;
            this.EMAIL=email;
            this.USERNAME=username;
            this.PASSWORD=password;
            this.COORDENADAX=cX;
            this.COORDENADAY=cY;
            this.CODESTADO=codEstado;
        }

        public int CODUTILIZADOR
        {
            get { return _codUtilizador; }
            set { this._codUtilizador = value; }
        }
        public string NOME
        {
            get { return _nome; }
            set { this._nome = value; }
        }
        public DateTime DATANASCIMENTO
        {
            get { return _dataNascimento; }
            set { this._dataNascimento = value; }
        }
        public int NUMEROTELEFONE
        {
            get { return _numeroTelefone; }
            set { this._numeroTelefone = value; }
        }
        public string EMAIL
        {
            get { return _email; }
            set { this._email = value; }
        }
        public string USERNAME
        {
            get { return _username; }
            set { this._username = value; }
        }
        public string PASSWORD
        {
            get { return _password; }
            set { this._password = value; }
        }
        public float COORDENADAX
        {
            get { return _coordenadaX; }
            set { this._coordenadaX = value; }
        }
        public float COORDENADAY
        {
            get { return _coordenadaY; }
            set { this._coordenadaY  = value; }
        }
        public int CODESTADO
        {
            get { return _codEstado; }
            set { this._codEstado  = value; }
        }

        protected Utilizador(DataRow row)
        {
            this._codUtilizador = (int)row["Cod_utilizador"];
            this._nome = (string)row["Nome"];
            this._dataNascimento = (DateTime)row["Data_Nascimento"];
            this._numeroTelefone = (int)row["Numero_Telefone"];
            this._email = (string)row["Email"];
            this._username = (string)row["Username"];
            this._password = (string)row["Password"];
            this._coordenadaX = (float)row["CoordenadaX"];
            this._coordenadaY = (float)row["CoordenadaY"];
            this._codEstado = (int)row["Cod_estado"];
        }

        public static Utilizador LoadById(int utilID)
        {
            DataSet ds = ExecuteQuery("select * from Utilizador where Cod_utilizador=" + utilID, getConnection(false));
            if (ds.Tables[0].Rows.Count != 1)
            {
                return null;
            }
            else
            {
                return new Utilizador(ds.Tables[0].Rows[0]);
            }
        }

        public static IList LoadAll()
        {
            try
            {

                DataSet ds = ExecuteQuery("Select * From Utilizador", getConnection(true));
                IList list = new ArrayList();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Utilizador util = new Utilizador(row);
                    list.Add(util);
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

                DataSet ds = ExecuteQuery("Select u.* from Utilizador u where u.Cod_utilizador like (select MAX(Cod_utilizador) from Utilizador)", getConnection(true));
                DataRow row = ds.Tables[0].Rows[0];
                Utilizador util = new Utilizador(row);
                return util.CODUTILIZADOR+ 1;
            }
            catch (Exception e)
            {
                throw new ApplicationException("ErroBD", e);
            }
        }



        public override void Save()
        {
            BeginTransaction();
            SqlCommand sqlUtilizador = new SqlCommand();

            if (_codUtilizador != 0)
            {
                sqlUtilizador.CommandText = "UPDATE Utilizador SET Nome=@nome,Data_Nascimento=@dtNascimento,Numero_Telefone=@numTelef,Email=@email,Username=@user,Password=@pass,CoordenadaX=@cx,CoordenadaY=@cy,Cod_estado=@c_estado WHERE Cod_utilizador=@codutil";
            }
            else
            {
                _codUtilizador = getProxID();
             
                sqlUtilizador.CommandText = "INSERT INTO Utilizador(Cod_utilizador,Nome,Data_Nascimento,Numero_Telefone,Email,Username,Password,CoordenadaX,CoordenadaY,Cod_estado) VALUES (@codutil,@nome,@dtNascimento,@numTelef,@email,@user,@pass,@cx,@cy,@c_estado)";
            }
            sqlUtilizador.Transaction = CurrentTransaction;

            
            SqlParameter codUtilizador = new SqlParameter("@codutil", SqlDbType.Int);
            codUtilizador.Value = _codUtilizador;
            sqlUtilizador.Parameters.Add(codUtilizador);

            SqlParameter nome = new SqlParameter("@nome", SqlDbType.NVarChar);
            nome.Value = _nome;
            sqlUtilizador.Parameters.Add(nome);

            SqlParameter data = new SqlParameter("@dtNascimento", SqlDbType.Date);
            data.Value = _dataNascimento;
            sqlUtilizador.Parameters.Add(data);

            SqlParameter numeroT= new SqlParameter("@numTelef", SqlDbType.Int);
            numeroT.Value = _numeroTelefone;
            sqlUtilizador.Parameters.Add(numeroT);

            SqlParameter email = new SqlParameter("@email", SqlDbType.NVarChar);
            email.Value = _email;
            sqlUtilizador.Parameters.Add(email);
            
            SqlParameter username = new SqlParameter("@user", SqlDbType.NVarChar);
            username.Value = _username;
            sqlUtilizador.Parameters.Add(username);

            SqlParameter password = new SqlParameter("@pass", SqlDbType.NVarChar);
            password.Value = _password;
            sqlUtilizador.Parameters.Add(password);

            SqlParameter cx = new SqlParameter("@cx", SqlDbType.Float);
            cx.Value = _coordenadaX;
            sqlUtilizador.Parameters.Add(cx);

            SqlParameter cy = new SqlParameter("@cxy", SqlDbType.Float);
            cy.Value = _coordenadaY;
            sqlUtilizador.Parameters.Add(cy);

            SqlParameter estado = new SqlParameter("@c_estado", SqlDbType.Int);
            estado.Value = _codEstado;
            sqlUtilizador.Parameters.Add(estado);

            int id = ExecuteTransactedNonQuery(sqlUtilizador);
            CommitTransaction();

        }


        public void delete()
        {
            BeginTransaction();
            SqlCommand sqlUtilizador = new SqlCommand();

            sqlUtilizador.CommandText = "delete from Utilizador where Cod_utilizador=@codUtil";
            sqlUtilizador.Transaction = CurrentTransaction;

            SqlParameter codU = new SqlParameter("@codUtil", SqlDbType.Int);
            codU.Value = _codUtilizador;
            sqlUtilizador.Parameters.Add(codU);

            int id = ExecuteTransactedNonQuery(sqlUtilizador);
            CommitTransaction();

        }


    }
}