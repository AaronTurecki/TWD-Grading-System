using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using twd_project.ViewModels;

namespace twd_project.Models {

    public class GradeExportRepository {

        public string FormatAdminHeading(List<TWD_User> students, string intake) {
            string reportLine = "";

            for (int i = 0; i < students.Count(); i++) {
                reportLine += "," + students[i].firstName + " " + students[i].lastName + " " + students[i].bcitId;
            }
            return intake + ",," + reportLine;
        }

        public string FormatInstructorHeading(List<TWD_User> students, Intake intake) {
            string reportLine = "";

            for (int i = 0; i < students.Count(); i++) {
                reportLine += "," + students[i].firstName + " " + students[i].lastName + " " + students[i].bcitId;
            }
            return intake.intake1 + "," + reportLine;
        }

        public string FormatStudentHeading(List<string> firstNames, List<string> lastNames
                                            , List<string> studentNos, string intake) {
            string reportLine = "";

            for (int i = 0; i < firstNames.Count(); i++)
            {
                reportLine += "," + firstNames[i] + " " + lastNames[i] + " " + studentNos[i];
            }
            return intake + "," + reportLine;
        }
        public List<string> FormatArrayLines(List<List<string>> moduleLines) {
            List<string> reportLines = new List<string>();
            string reportLine = "";

            foreach (List<string> items in moduleLines) {
                for (int i = 1; i < items.Count(); i++) {
                    reportLine += items[i] + ",";
                }
                reportLines.Add(reportLine);
                reportLine = "";
            }
            return reportLines;
        }

        public string FormatTotalLine(List<string> totalLine) {
            string reportLine = "";

            for (int i = 1; i < totalLine.Count(); i++) {
                reportLine += totalLine[i] + ",";
            }
            return reportLine;
        }
    }
}