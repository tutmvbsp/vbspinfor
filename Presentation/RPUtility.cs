using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
namespace Presentation
{
    public class RPUtility
    {
        //Begin
        public static void ShowRp(ReportClass rc, object objDataSource, Window parentWindow, string database, string server, string use, string pass)
        {
            try
            {
                rc.SetDataSource(objDataSource);
                ReportViewUI Viewer = new ReportViewUI();
                //log on
                TableLogOnInfos logonInfos = new TableLogOnInfos();
                TableLogOnInfo logonInfo = new TableLogOnInfo();
                ConnectionInfo connectioninfo = new ConnectionInfo();
                Tables CrTables;
                // tham so server
                connectioninfo.DatabaseName = database;
                connectioninfo.ServerName = server;
                connectioninfo.Password = pass;
                connectioninfo.UserID = use;
                CrTables = rc.Database.Tables;
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    logonInfo = CrTable.LogOnInfo;
                    logonInfo.ConnectionInfo = connectioninfo;
                    CrTable.ApplyLogOnInfo(logonInfo);

                }
                //
                Viewer.setReportSource(rc);
                Viewer.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static void ShowRpOnePara(ReportClass rc, object objDataSource, string mau, Window parentWindow, string database, string server, string use, string pass)
        {
            try
            {
                rc.SetDataSource(objDataSource);
                ReportViewUI Viewer = new ReportViewUI();
                //----------------------------------------------------------------------------
                ParameterFieldDefinitions crParameterFieldDefinitions;
                ParameterFieldDefinition crParameterFieldDefinition;
                ParameterValues crParameterValues = new ParameterValues();
                ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

                crParameterDiscreteValue.Value = mau;
                crParameterFieldDefinitions = rc.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["in"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);
                //------------------------------------------------------------------------------
                //log on
                TableLogOnInfos logonInfos = new TableLogOnInfos();
                TableLogOnInfo logonInfo = new TableLogOnInfo();
                ConnectionInfo connectioninfo = new ConnectionInfo();
                Tables CrTables;
                // tham so server
                connectioninfo.DatabaseName = database;
                connectioninfo.ServerName = server;
                connectioninfo.Password = pass;
                connectioninfo.UserID = use;
                CrTables = rc.Database.Tables;
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    logonInfo = CrTable.LogOnInfo;
                    logonInfo.ConnectionInfo = connectioninfo;
                    CrTable.ApplyLogOnInfo(logonInfo);

                }
                //
                Viewer.setReportSource(rc);
                Viewer.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //
    }
}
