using System.Data;
using System;
using System.Collections.Generic;
using Sap.Data.Hana;
//using Microsoft.Extensions.Logging;
using Utils.Helper;
using System.Windows;
using Microsoft.Extensions.Logging;

namespace SAPBo.B1HanaQuery
{

    public class HanaRequest
    {
        public delegate T RowMapper<T>(HanaDataReader dataReader) where T : class;

        private static HanaConnection OpenConnection(ILogger logger)
        {
            try
            {
                string connectionString = AppDomain.CurrentDomain.GetData("SBOConnectionString")!.ToString()!;
                logger.LogInformation($"{connectionString}", "OpenConnection");
                HanaConnection connection = new HanaConnection();
                connection.ConnectionString = connectionString;
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "OpenConnection");
                throw;
            }
        }

        private static void CloseConnection(HanaConnection connection, ILogger logger)
        {
            try
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "CloseConnection");
                throw;
            }

        }

        private static void CloseCommand(HanaCommand command, ILogger logger)
        {
            try
            {
                if (command != null)
                {
                    command.Dispose();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "CloseCommand");

                throw;
            }

        }

        private static void CloseDataReader(HanaDataReader dataReader, ILogger logger)
        {
            try
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "CloseDataReader");
                throw;
            }

        }

        //private static HanaCommand PrepareCommandSQL(string commandText, List<HanaParameter> parameters, HanaConnection connection)
        //{
        //    HanaCommand command = new HanaCommand();
        //    command.Connection = connection;
        //    command.CommandType = CommandType.Text;
        //    command.CommandText = commandText;
        //    foreach (HanaParameter parameter in parameters)
        //    {
        //        command.Parameters.Add(parameter);
        //    }
        //    return command;
        //}

        private static HanaCommand PrepareCommand(string commandText, List<HanaParameter> parameters, HanaConnection connection, ILogger logger)
        {
            try
            {
                HanaCommand command = new HanaCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = commandText;
                foreach (HanaParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }

                return command;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "PrepareCommand");
                throw;
            }

        }

        public static Task<List<T>> ExecuteToListAsync<T>(string commandText, RowMapper<T> rowMapper, ILogger logger) where T : class
        {
            try
            {
                List<HanaParameter> parameters = new List<HanaParameter>();
                return ExecuteToListAsync<T>(commandText, rowMapper, parameters, logger);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "ExecuteToListAsync");
                throw;
            }
        }

        public static Task<List<T>> ExecuteToListAsync<T>(string commandText, RowMapper<T> rowMapper, HanaParameter parameter, ILogger logger) where T : class
        {
            try
            {
                List<HanaParameter> parameters = new List<HanaParameter>();
                parameters.Add(parameter);
                return ExecuteToListAsync<T>(commandText, rowMapper, parameters, logger);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "ExecuteToListAsync");
                throw;
            }

        }

        public static Task<T> ExecuteToEntityAsync<T>(string commandText, RowMapper<T> rowMapper, HanaParameter parameter, ILogger logger) where T : class
        {
            try
            {
                List<HanaParameter> parameters = new List<HanaParameter>();
                parameters.Add(parameter);
                return ExecuteToEntityAsync<T>(commandText, rowMapper, parameters, logger);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "ExecuteToEntityAsync");
                throw;
            }

        }

        public static async Task<T> ExecuteToEntityAsync<T>(string commandText, RowMapper<T> rowMapper, List<HanaParameter> parameters, ILogger logger) where T : class
        {
            HanaConnection? connection = null;
            HanaCommand? command = null;

            T? entity = null;
            try
            {
                logger.LogInformation(message: $"Iniciando {commandText}", "ExecuteToEntityAsync");
                connection = OpenConnection(logger);
                command = PrepareCommand(commandText, parameters, connection, logger);
                logger.LogInformation(message: $"Preparando {commandText}", "ExecuteToEntityAsync");

                using (HanaDataReader dataReader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                {
                    logger.LogInformation(message: $"Leyendo {commandText}", "ExecuteToEntityAsync");
                    while (await dataReader.ReadAsync().ConfigureAwait(false))
                    {
                        entity = rowMapper(dataReader);
                    }
                }
                logger.LogInformation(message: $"Finalizando {commandText}", "ExecuteToEntityAsync");

            }
            catch (HanaException ex)
            {
                logger.LogError(ex, "ExecuteToEntityAsync");
                throw;
            }
            finally
            {
                CloseCommand(command!, logger);
                CloseConnection(connection!, logger);
            }

            return entity!;
        }

        public static async Task<List<T>> ExecuteToListAsync<T>(string commandText, RowMapper<T> rowMapper, List<HanaParameter> parameters, ILogger logger) where T : class
        {
            HanaConnection? connection = null;
            HanaCommand? command = null;

            List<T> listEntity = new List<T>();
            try
            {
                logger.LogInformation(message: $"Iniciando {commandText}", "ExecuteToListAsync");
                connection = OpenConnection(logger);
                command = PrepareCommand(commandText, parameters, connection, logger);
                logger.LogInformation(message: $"Preparando {commandText}", "ExecuteToListAsync");

                using (HanaDataReader dataReader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                {
                    logger.LogInformation(message: $"Leyendo {commandText}", "ExecuteToListAsync");
                    while (await dataReader.ReadAsync().ConfigureAwait(false))
                    {
                        T entity = rowMapper(dataReader);
                        listEntity.Add(entity);
                    }
                }
                logger.LogInformation(message: $"Finalizando {commandText}", "ExecuteToListAsync");

            }
            catch (HanaException ex)
            {
                logger.LogError(ex, "ExecuteToEntityAsync");
                throw;
            }
            finally
            {
                CloseCommand(command!, logger);
                CloseConnection(connection!, logger);
            }

            return listEntity;
        }

        //public static void Execute(string commandText, HanaParameter parameter)
        //{
        //    List<HanaParameter> parameters = new List<HanaParameter>();
        //    parameters.Add(parameter);

        //    Execute(commandText, parameters);
        //}

        //public static void Execute(string commandText, List<HanaParameter> parameters)
        //{
        //    MySqlConnection connection = null;
        //    MySqlCommand command = null;

        //    try
        //    {
        //        connection = OpenConnection();
        //        command = PrepareCommand(commandText, parameters, connection);
        //        command.ExecuteNonQuery();
        //    }
        //    catch (MySqlException ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        CloseCommand(command);
        //        CloseConnection(connection);
        //    }
        //}

        //public static async Task<Object> ExecuteAsync(string commandText, HanaParameter parameter, string parameterOutput)
        //{
        //    List<HanaParameter> parameters = new List<HanaParameter>();
        //    parameters.Add(parameter);

        //    return await ExecuteAsync(commandText, parameter, parameterOutput);
        //}

        //public static async Task<Object> ExecuteAsync(string commandText, List<HanaParameter> parameters, string parameterOutput)
        //{
        //    HanaConnection? connection = null;
        //    HanaCommand? command = null;
        //    object parameterOutputValue;
        //    try
        //    {
        //        connection = OpenConnection();
        //        command = PrepareCommand(commandText, parameters, connection);
        //        await command.ExecuteNonQueryAsync();
        //        parameterOutputValue = command.Parameters[parameterOutput].Value;
        //    }
        //    catch (HanaException ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        CloseCommand(command!);
        //        CloseConnection(connection!);
        //    }
        //    return parameterOutputValue;

        //}


    }

}
