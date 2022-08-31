/*Thuy Giang Nguyen*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;

namespace HouseCleaningThuyGiangNguyen
{
    internal class DB
    {
        private const string CONNECTION_STRING = @"Server=.\SQLEXPRESS;Database=HouseCleaning;Trusted_Connection=True;";
        const string INSERT_COMMAND = "INSERT INTO customers (name, postalCode, phone) VALUES (@Name, @PostalCode, @Phone); "
                                         + "SELECT @@IDENTITY; "
                                         + "INSERT INTO appointments (serviceDate, houseSize, customerId, cleanerTypeId) "
                                         + "VALUES (@ServiceDate, @HouseSize, @@IDENTITY, @CleanerTypeId);";
        private const string INSERT_APPOINTMENT_COMMAND = "INSERT INTO appointments (serviceDate, houseSize, customerId, cleanerTypeId) VALUES (@ServiceDate, @HouseSize, @CustomerID, @CleanerTypeId)";
        private const string UPDATE_APPOINTMENT_COMMAND = "UPDATE appointments SET serviceDate = @ServiceDate, houseSize = @HouseSize, cleanerTypeId = @CleanerTypeId WHERE id = @AppointmentID;";
        private const string SELECT_COMMAND = "SELECT c.id CustomerID, name, postalCode, phone, AppointmentID, "
                                                + "serviceDate, houseSize, cleanerTypeId, cost "
                                                + "FROM customers c "
                                                + "LEFT JOIN (SELECT a.id AppointmentID, serviceDate, houseSize, cleanerTypeId, customerId, "
                                                + "a.houseSize*cl.ratePerMeter AS cost "
                                                + "FROM appointments a "
                                                + "LEFT JOIN cleanerTypes cl "
                                                + "ON a.cleanerTypeId = cl.id) x "
                                                + "ON c.ID = x.customerId;";
        private const string DELETE_COMMAND = "DELETE FROM appointments WHERE id = @AppointmentID";
        private const string SELECT_CLEANER_TYPE_COMMAND = "SELECT * FROM cleanerTypes;";
        private const string SELECT_CUSTOMER_COMMAND = "SELECT * FROM customers;";

        private readonly SqlConnection conn;

        private static DB db;
        public static DB Instance { get { db ??= new DB(); return db; } }

        private DB()
        {
            conn = new SqlConnection(CONNECTION_STRING);
            conn.Open();
        }

        public bool Insert(CustomerAppointment customerAppointment)
        {
            using var cmd = new SqlCommand(INSERT_COMMAND, conn);
            cmd.Parameters.AddWithValue("@Name", customerAppointment.Name);
            cmd.Parameters.AddWithValue("@PostalCode", customerAppointment.PostalCode);
            cmd.Parameters.AddWithValue("@Phone", customerAppointment.Phone);
            cmd.Parameters.AddWithValue("@ServiceDate", customerAppointment.ServiceDate);
            cmd.Parameters.AddWithValue("@HouseSize", customerAppointment.HouseSize);
            cmd.Parameters.AddWithValue("@CleanerTypeID", customerAppointment.ThisCleanerType.ID);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Update(CustomerAppointment customerAppointment)
        {
            if (customerAppointment.AppointmentID == null)
            {
                using var cmd = new SqlCommand(INSERT_APPOINTMENT_COMMAND, conn);

                cmd.Parameters.AddWithValue("@ServiceDate", customerAppointment.ServiceDate);
                cmd.Parameters.AddWithValue("@HouseSize", customerAppointment.HouseSize);
                cmd.Parameters.AddWithValue("@CustomerID", customerAppointment.CustomerID);
                cmd.Parameters.AddWithValue("@CleanerTypeID", customerAppointment.ThisCleanerType.ID);

                return cmd.ExecuteNonQuery() > 0;
            }
            else
            {
                using var cmd = new SqlCommand(UPDATE_APPOINTMENT_COMMAND, conn);

                cmd.Parameters.AddWithValue("@AppointmentID", customerAppointment.AppointmentID);
                cmd.Parameters.AddWithValue("@ServiceDate", customerAppointment.ServiceDate);
                cmd.Parameters.AddWithValue("@HouseSize", customerAppointment.HouseSize);
                cmd.Parameters.AddWithValue("@CleanerTypeID", customerAppointment.ThisCleanerType.ID);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(CustomerAppointment customerAppointment)
        {
            if (customerAppointment.AppointmentID == null) { return false; }
            using var cmd = new SqlCommand(DELETE_COMMAND, conn);
            cmd.Parameters.AddWithValue("@AppointmentID", customerAppointment.AppointmentID);
            return cmd.ExecuteNonQuery() > 0;
        }

        public List<CustomerAppointment> Get()
        {
            var customerAppointmentList = new List<CustomerAppointment>();

            using var cmd = new SqlCommand(SELECT_COMMAND, conn);
            using var dr = cmd.ExecuteReader();

            while (dr.Read())
                customerAppointmentList.Add(new CustomerAppointment()
                {
                    CustomerID = dr.GetInt32(dr.GetOrdinal("CustomerID")),
                    Name = dr.GetString(dr.GetOrdinal("name")),
                    PostalCode = dr.GetString(dr.GetOrdinal("postalCode")),
                    Phone = dr.GetString(dr.GetOrdinal("phone")),
                    AppointmentID = dr.IsDBNull(dr.GetOrdinal("AppointmentID")) ? null : (int?)dr.GetInt32(dr.GetOrdinal("AppointmentID")),
                    ServiceDate = dr.IsDBNull(dr.GetOrdinal("serviceDate")) ? null : (DateTime?)dr.GetDateTime(dr.GetOrdinal("serviceDate")),
                    HouseSize = dr.IsDBNull(dr.GetOrdinal("houseSize")) ? null : (decimal?)dr.GetDecimal(dr.GetOrdinal("houseSize")),
                    CleanerTypeID = dr.IsDBNull(dr.GetOrdinal("serviceDate")) ? null : (int?)dr.GetInt32(dr.GetOrdinal("cleanerTypeId")),
                    Cost = dr.IsDBNull(dr.GetOrdinal("serviceDate")) ? null : (decimal?)dr.GetDecimal(dr.GetOrdinal("cost")),
                    IsNew = false
                });
            dr.Close();

            return customerAppointmentList;
        }

        public List<Customer> GetCustomer()
        {
            var customerList = new List<Customer>();

            using var cmd = new SqlCommand(SELECT_CUSTOMER_COMMAND, conn);
            using var dr = cmd.ExecuteReader();

            while (dr.Read())
                customerList.Add(new Customer
                {
                    CustomerID = dr.GetInt32(dr.GetOrdinal("id")),
                    Name = dr.GetString(dr.GetOrdinal("name")),
                    PostalCode = dr.GetString(dr.GetOrdinal("postalCode")),
                    Phone = dr.GetString(dr.GetOrdinal("phone")),
                    IsNew = false
                });
            dr.Close();

            return customerList;
        }

        public BindingList<CleanerType> GetCleanerTypeList()
        {
            var cleanerTypeList = new BindingList<CleanerType>();

            using var cmd = new SqlCommand(SELECT_CLEANER_TYPE_COMMAND, conn);
            using var dr = cmd.ExecuteReader();

            while (dr.Read())
                cleanerTypeList.Add(new CleanerType
                {
                    ID = dr.GetInt32(dr.GetOrdinal("id")),
                    Description = dr.GetString(dr.GetOrdinal("description")),
                    RatePerMeter = dr.GetDecimal(dr.GetOrdinal("ratePerMeter"))
                });
            dr.Close();

            return cleanerTypeList;
        }

    }

}
