using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;

namespace TestClient_LoadMeetingsForClient
{
    public interface IDataAccess
    {
        DataTable ExecuteDataTable(string query, Dictionary<string, object> parameters);
    }

    public class Main_form_for_Client
    {
        private IDataAccess dataAccess;
        public DataTable meetingsTable;

        public Main_form_for_Client(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public void LoadMeetingsForClient()
        {
            // ����� ��� ������ �������� �������������� �볺���
            string query = @"SELECT 
                    Meetings.ID_meeting, 
                    Meetings.Date, 
                    Meetings.Time, 
                    Meetings.Status, 
                    Meetings.Problem_description, 
                    Support_worker.Worker_name AS Assigned_Worker 
                FROM 
                    Meetings 
                LEFT JOIN 
                    Support_worker ON Meetings.ID_worker = Support_worker.ID_worker 
                WHERE 
                    Meetings.ID_client = @ID_client";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["@ID_client"] = "test_login";

            // �������� ����� � �������� ���������
            meetingsTable = dataAccess.ExecuteDataTable(query, parameters);
        }

    }

    [TestFixture]
    public class Tests
    {
        private class MockDataAccess : IDataAccess
        {
            public DataTable ExecuteDataTable(string query, Dictionary<string, object> parameters)
            {
                // ��������� ������� DataTable
                return new DataTable();
            }
        }

        private class MockDataAccessEmptyResult : IDataAccess
        {
            public DataTable ExecuteDataTable(string query, Dictionary<string, object> parameters)
            {
                // ��������� ������� DataTable
                return new DataTable();
            }
        }

        private Main_form_for_Client form;

        [SetUp]
        public void Setup()
        {
            // Arrange: ϳ�������� �������� �����
            IDataAccess dataAccess = new MockDataAccess();
            form = new Main_form_for_Client(dataAccess);
        }

        [Test]
        public void LoadMeetingsForClient_SuccessfullyLoadsMeetings()
        {
            // Act: ������ ������, ���� �������
            form.LoadMeetingsForClient();

            // Assert: ��������, �� ���������� �������� ���� ���������� ������
            Assert.IsNotNull(form.meetingsTable);
        }

        [Test]
        public void LoadMeetingsForClient_EmptyResult()
        {
            // Arrange: ϳ�������� �������� ����� � ������� ����������� ������
            IDataAccess dataAccess = new MockDataAccessEmptyResult();
            form = new Main_form_for_Client(dataAccess);

            // Act: ������ ������, ���� �������
            form.LoadMeetingsForClient();

            // Assert: ��������, �� ���������� �������� � ��������
            Assert.IsNotNull(form.meetingsTable);
            Assert.AreEqual(0, form.meetingsTable.Rows.Count);
        }
    }
}