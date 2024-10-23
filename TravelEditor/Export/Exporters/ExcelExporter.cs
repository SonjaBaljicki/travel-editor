using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using TravelEditor.Database;
using TravelEditor.Export.Iterfaces;
using TravelEditor.Export.Service;
using TravelEditor.Models;

namespace TravelEditor.Export.Exporters
{
    public class ExcelExporter : IDataExporter
    {
        private readonly IDataTableService _dataTableService;

        public ExcelExporter(IDataTableService dataTableService)
        {
            _dataTableService = dataTableService;
        }

        public void Export(string filePath)
        {
            Workbook workbook = new Workbook();

             workbook.Worksheets.Clear();

            workbook.Worksheets.Add("Trips");
            workbook.Worksheets.Add("Destinations");
            workbook.Worksheets.Add("Attractions");
            workbook.Worksheets.Add("Travellers");
            workbook.Worksheets.Add("Reviews");

            DataTable tripsDataTable = _dataTableService.GetAsDataTable<Trip>();
            Worksheet tripSheet = workbook.Worksheets[0];
            tripSheet.InsertDataTable(tripsDataTable, true, 1, 1);

            DataTable destinationDataTable = _dataTableService.GetAsDataTable<Destination>();
            Worksheet destinationSheet = workbook.Worksheets[1];
            destinationSheet.InsertDataTable(destinationDataTable, true, 1, 1);

            DataTable attractionsDataTable = _dataTableService.GetAsDataTable<Attraction>();
            Worksheet attractionSheet = workbook.Worksheets[2];
            attractionSheet.InsertDataTable(attractionsDataTable, true, 1, 1);

            DataTable travellersDataTable = _dataTableService.GetAsDataTable<Traveller>();
            Worksheet travellerSheet = workbook.Worksheets[3];
            travellerSheet.InsertDataTable(travellersDataTable, true, 1, 1);

            DataTable reviewsDataTable = _dataTableService.GetAsDataTable<Review>();
            Worksheet reviewSheet = workbook.Worksheets[4];
            reviewSheet.InsertDataTable(reviewsDataTable, true, 1, 1);

            foreach (Worksheet sheet in workbook.Worksheets)
            {
                sheet.AllocatedRange.AutoFitColumns();
            }
            workbook.SaveToFile(filePath, ExcelVersion.Version2016);

        }
    }
}
