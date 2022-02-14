using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using ConfigFileHandlerDll;

namespace ContinentalExam.DA
{
    public class CBase
    {
        protected readonly string cadenaConexion;
        public CBase()
        {
            ConfigFileHandler Config = new ConfigFileHandler("./Configs/", "Configuration", "cfg", '|');

            List<ConfigVar> Configurations = Config.GetConfigsRow(new string[] { "Provider", "Source"});

            if (Configurations.Count > 0)
                cadenaConexion = string.Format("Provider={0};Data Source={1};Persist Security Info=False;"
                    , (from conf in Configurations where conf.VarName == "Provider" select conf).First<ConfigVar>().Value
                    , (from conf in Configurations where conf.VarName == "Source" select conf).First<ConfigVar>().Value);
        }

        public CBase(string connectionString)
        {
            cadenaConexion = connectionString;
        }

        private static void CierraConexion(OleDbConnection conn)
        {
            if (conn.State != ConnectionState.Closed)
                conn.Close();
            conn.Dispose();
        }

        public DataTable consultarDT(string procedimientoAlmacenado, OleDbParameter[] parametros)
        {
            DataTable tabla = null;
            using (OleDbConnection conexion = new OleDbConnection(cadenaConexion))
            {
                try
                {

                    conexion.Open();
                    OleDbCommand comando = new OleDbCommand();
                    comando.Connection = conexion;
                    comando.CommandTimeout = 0;
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = procedimientoAlmacenado;

                    if (parametros != null)
                    {
                        for (int indice = 0; indice < parametros.Length; indice++)
                            comando.Parameters.AddWithValue(parametros[indice].ParameterName, parametros[indice].Value);
                    }

                    OleDbDataAdapter adaptador = new OleDbDataAdapter();
                    adaptador.SelectCommand = comando;
                    DataSet ds = new DataSet();
                    adaptador.Fill(ds, "Tabla");

                    tabla = ds.Tables["Tabla"];


                }
                catch (Exception ex)
                {
                    //Log.addErrorLog(ex.Message);
                    tabla = null;
                }
                finally
                {
                    CierraConexion(conexion);

                }
            }
            return tabla;

        }

        public DataTable consultarDT(OleDbParameterCollection parametros, string procedimientoAlmacenado)
        {
            DataTable tabla = null;
            using (OleDbConnection conexion = new OleDbConnection(cadenaConexion))
            {
                try
                {

                    conexion.Open();
                    OleDbCommand comando = new OleDbCommand();
                    comando.Connection = conexion;
                    comando.CommandTimeout = 0; // Opción para que no caduque la conexion a la DB
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = procedimientoAlmacenado;

                    if (parametros != null)
                    {
                        for (int indice = 0; indice < parametros.Count; indice++)
                            comando.Parameters.AddWithValue(parametros[indice].ParameterName, parametros[indice].Value);
                    }

                    OleDbDataAdapter adaptador = new OleDbDataAdapter();
                    adaptador.SelectCommand = comando;
                    DataSet ds = new DataSet();
                    adaptador.Fill(ds, "Tabla");

                    tabla = ds.Tables["Tabla"];
                }
                catch (Exception ex)
                {
                    //Log.addErrorLog(ex.Message);
                    tabla = null;
                }
                finally
                {
                    CierraConexion(conexion);
                }
            }
            return tabla;
        }

        public DataTable RecuperaTabla(string cmdText)
        {
            DataTable tabla = null;
            OleDbConnection conn = new OleDbConnection(cadenaConexion);
            try
            {
                DataSet ds = new DataSet();
                OleDbDataAdapter adapter = new OleDbDataAdapter();
                adapter.SelectCommand = new OleDbCommand(cmdText, conn);
                adapter.Fill(ds);
                tabla = ds.Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                CierraConexion(conn);
            }
            return tabla;
        }
        public bool insertarDT(string procedimientoAlmacenado, OleDbParameter[] parametros)
        {
            bool insertar = true;
            using (OleDbConnection conexion = new OleDbConnection(cadenaConexion))
            {
                try
                {


                    conexion.Open();
                    OleDbCommand comando = new OleDbCommand();
                    comando.Connection = conexion;
                    comando.CommandTimeout = 0;
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = procedimientoAlmacenado;

                    if (parametros != null)
                    {
                        for (int indice = 0; indice < parametros.Length; indice++)
                            comando.Parameters.AddWithValue(parametros[indice].ParameterName, parametros[indice].Value);
                    }

                    OleDbDataAdapter adaptador = new OleDbDataAdapter();
                    adaptador.SelectCommand = comando;
                    DataSet ds = new DataSet();
                    adaptador.Fill(ds, "Tabla");

                }
                catch (Exception ex)
                {
                    //Log.addErrorLog(ex.Message);
                    insertar = false;
                }
                finally
                {
                    CierraConexion(conexion);
                }
            }
            return insertar;
        }

        public bool consultarIUD(string procedimientoAlmacenado, OleDbParameter[] parametros)
        {
            bool resp;
            using (OleDbConnection conexion = new OleDbConnection(cadenaConexion))
            {
                conexion.Open();
                OleDbCommand comando = new OleDbCommand();
                comando.Connection = conexion;
                comando.CommandTimeout = 0;
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = procedimientoAlmacenado;

                if (parametros != null)
                {
                    for (int indice = 0; indice < parametros.Length; indice++)
                    {
                        //comando.Parameters.Add(parametros[indice]);
                        comando.Parameters.AddWithValue(parametros[indice].ParameterName, parametros[indice].Value);
                    }
                }

                OleDbTransaction trans = null;

                try
                {
                    trans = conexion.BeginTransaction();
                    comando.Transaction = trans;
                    comando.ExecuteNonQuery();
                    trans.Commit();

                    resp = true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    resp = false;
                }
                finally
                {
                    CierraConexion(conexion);
                }
            }
            return resp;
        }

        public bool consultarIUDVB(string procedimientoAlmacenado, OleDbParameter[] parametros)
        {
            bool resp;
            using (OleDbConnection conexion = new OleDbConnection(cadenaConexion))
            {
                conexion.Open();
                OleDbCommand comando = new OleDbCommand();
                comando.Connection = conexion;
                comando.CommandTimeout = 0;
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = procedimientoAlmacenado;

                if (parametros != null)
                {
                    for (int indice = 0; indice < parametros.Length; indice++)
                    {
                        //comando.Parameters.Add(parametros[indice]);
                        comando.Parameters.Add(parametros[indice].ParameterName, OleDbType.VarBinary);
                        comando.Parameters[indice].Value = parametros[indice].Value;
                    }
                }

                OleDbTransaction trans = null;

                try
                {
                    trans = conexion.BeginTransaction();
                    comando.Transaction = trans;
                    comando.ExecuteNonQuery();
                    trans.Commit();

                    resp = true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    resp = false;
                }
                finally
                {
                    CierraConexion(conexion);
                }
            }
            return resp;
        }

        public bool consultarIUD(string procedimientoAlmacenado, OleDbParameter[] parametros, ref string errorMsg)
        {
            bool resp;
            using (OleDbConnection conexion = new OleDbConnection(cadenaConexion))
            {
                conexion.Open();
                OleDbCommand comando = new OleDbCommand();
                comando.Connection = conexion;
                comando.CommandTimeout = 0;
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = procedimientoAlmacenado;

                if (parametros != null)
                {
                    for (int indice = 0; indice < parametros.Length; indice++)
                    {
                        //comando.Parameters.Add(parametros[indice]);
                        comando.Parameters.AddWithValue(parametros[indice].ParameterName, parametros[indice].Value);
                    }
                }

                OleDbTransaction trans = null;

                try
                {
                    trans = conexion.BeginTransaction();
                    comando.Transaction = trans;
                    comando.ExecuteNonQuery();
                    trans.Commit();

                    resp = true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    errorMsg = ex.Message;
                    resp = false;
                }
                finally
                {
                    CierraConexion(conexion);
                }
            }
            return resp;
        }

        public bool consultarIUD(string procedimientoAlmacenado, OleDbParameterCollection parametros, ref string errorMsg)
        {
            bool resp;
            using (OleDbConnection conexion = new OleDbConnection(cadenaConexion))
            {
                conexion.Open();
                OleDbCommand comando = new OleDbCommand();
                comando.Connection = conexion;
                comando.CommandTimeout = 0;
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = procedimientoAlmacenado;

                if (parametros != null)
                {
                    for (int indice = 0; indice < parametros.Count; indice++)
                    {
                        //comando.Parameters.Add(parametros[indice]);
                        comando.Parameters.AddWithValue(parametros[indice].ParameterName, parametros[indice].Value);
                    }
                }

                OleDbTransaction trans = null;

                try
                {
                    trans = conexion.BeginTransaction();
                    comando.Transaction = trans;
                    comando.ExecuteNonQuery();
                    trans.Commit();

                    resp = true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    errorMsg = ex.Message;
                    resp = false;
                }
                finally
                {
                    CierraConexion(conexion);
                }
            }
            return resp;
        }

        public bool consultarMultiIUD(string procedimientoAlmacenado, List<OleDbParameterCollection> parametros, ref string errorMsg)
        {
            bool resp;
            using (OleDbConnection conexion = new OleDbConnection(cadenaConexion))
            {
                conexion.Open();
                OleDbTransaction trans = null;
                OleDbCommand comando = null;
                try
                {
                    comando = new OleDbCommand();
                    trans = conexion.BeginTransaction();
                    comando.Transaction = trans;
                    comando.Connection = conexion;
                    comando.CommandTimeout = 0;
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = procedimientoAlmacenado;
                    foreach (OleDbParameterCollection parametro in parametros)
                    {
                        comando.Parameters.Clear();
                        if (parametro != null)
                            for (int indice = 0; indice < parametro.Count; indice++)
                                comando.Parameters.AddWithValue(parametro[indice].ParameterName, parametro[indice].Value);

                        comando.ExecuteNonQuery();
                    }
                    resp = true;
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    errorMsg = ex.Message;
                    trans.Rollback();
                    resp = false;
                }
            }
            return resp;
        }

        public DataTable consultarDT(string procedimientoAlmacenado, OleDbParameter[] parametros, out string Err)
        {
            Err = "";
            DataTable tabla = null;
            using (OleDbConnection conexion = new OleDbConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();
                    OleDbCommand comando = new OleDbCommand();
                    comando.Connection = conexion;
                    comando.CommandTimeout = 0;
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = procedimientoAlmacenado;

                    if (parametros != null)
                    {
                        for (int indice = 0; indice < parametros.Length; indice++)
                            comando.Parameters.AddWithValue(parametros[indice].ParameterName, parametros[indice].Value);
                    }

                    OleDbDataAdapter adaptador = new OleDbDataAdapter();
                    adaptador.SelectCommand = comando;
                    DataSet ds = new DataSet();
                    adaptador.Fill(ds, "Tabla");

                    tabla = ds.Tables["Tabla"];
                }
                catch (Exception ex)
                {
                    Err = ex.Message;
                    tabla = null;
                }
                finally
                {
                    CierraConexion(conexion);
                }
            }
            return tabla;
        }

        public bool _consultarIUD(string procedimientoAlmacenado, OleDbParameterCollection parametros, ref string errorMsg)
        {
            bool resp;
            using (OleDbConnection conexion = new OleDbConnection(cadenaConexion))
            {
                conexion.Open();
                OleDbCommand comando = new OleDbCommand();
                comando.Connection = conexion;
                comando.CommandTimeout = 0;
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = procedimientoAlmacenado;

                if (parametros != null)
                {
                    for (int indice = 0; indice < parametros.Count; indice++)
                    {
                        //comando.Parameters.Add(parametros[indice]);
                        comando.Parameters.AddWithValue(parametros[indice].ParameterName, parametros[indice].Value);
                    }
                }

                OleDbTransaction trans = null;

                try
                {
                    trans = conexion.BeginTransaction();
                    comando.Transaction = trans;
                    comando.ExecuteNonQuery();
                    trans.Commit();

                    resp = true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    errorMsg = ex.Message;
                    resp = false;
                }
                finally
                { CierraConexion(conexion); }
            }
            return resp;
        }
    }
}
