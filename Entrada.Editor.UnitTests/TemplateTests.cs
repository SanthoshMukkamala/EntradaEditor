using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using Entrada.Editor.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Entrada.Editor.UnitTests
{
    [TestClass]
    public class TemplateTests : BaseTemplateTest
    {
        [TestMethod]
        public async Task TemplateTest0001 ()
        {
            await TemplateTest ("0001 - AAL - Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0002 ()
        {
            await TemplateTest ("0002 - AAL - Progress Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0003 ()
        {
            await TemplateTest ("0003 - AGH_OfficeVisitTDD.doc");
        }

        [TestMethod]
        public async Task TemplateTest0004 ()
        {
            await TemplateTest ("0004 - AGH_Radiology.doc");
        }

        [TestMethod]
        public async Task TemplateTest0005 ()
        {
            await TemplateTest ("0005 - AGH_DEXA.doc");
        }

        [TestMethod]
        public async Task TemplateTest0006 ()
        {
            await TemplateTest ("0006 - AHC_OfficeNoteTDD.doc");
        }

        [TestMethod]
        public async Task TemplateTest0007 ()
        {
            await TemplateTest ("0007 - AKR - Action_Taken_Complainant.doc");
        }

        [TestMethod]
        public async Task TemplateTest0008 ()
        {
            await TemplateTest ("0008 - AKR - Action_Taken_Victim.doc");
        }

        [TestMethod]
        public async Task TemplateTest0009 ()
        {
            await TemplateTest ("0009 - AKR - Case_Status_Complainant.doc");
        }

        [TestMethod]
        public async Task TemplateTest0010 ()
        {
            await TemplateTest ("0010 - AKR - ROI_Complainant.doc");
        }

        [TestMethod]
        public async Task TemplateTest0011 ()
        {
            await TemplateTest ("0011 - AKR - ROI_Victim.doc");
        }

        [TestMethod]
        public async Task TemplateTest0012 ()
        {
            await TemplateTest ("0012 - AMA_OfficeNoteTDD.doc");
        }

        [TestMethod]
        public async Task TemplateTest0013 ()
        {
            await TemplateTest ("0013 - ANI - Clinic Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0014 ()
        {
            await TemplateTest ("0014 - AOA_Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0015 ()
        {
            await TemplateTest ("0015 - AOA_OfficeNoteTDD.doc");
        }

        [TestMethod]
        public async Task TemplateTest0016 ()
        {
            await TemplateTest ("0016 - APC - APC_OfficeNoteTDD.doc");
        }

        [TestMethod]
        public async Task TemplateTest0017 ()
        {
            await TemplateTest ("0017 - AWH - Letterhead.doc");
        }

        [TestMethod]
        public async Task TemplateTest0018 ()
        {
            await TemplateTest ("0018 - AWH - OfficeNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0019 ()
        {
            await TemplateTest ("0019 - AKR - Case_Status_Victim.doc");
        }

        [TestMethod]
        public async Task TemplateTest0020 ()
        {
            await TemplateTest ("0020 - BLO_OfficeNoteTDD.doc");
        }

        [TestMethod]
        public async Task TemplateTest0021 ()
        {
            await TemplateTest ("0021 - BMC - Addendum.doc");
        }

        [TestMethod]
        public async Task TemplateTest0022 ()
        {
            await TemplateTest ("0022 - BMC - Clinic Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0023 ()
        {
            await TemplateTest ("0023 - BMC - Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0024 ()
        {
            await TemplateTest ("0024 - BMC - Letter_IM.doc");
        }

        [TestMethod]
        public async Task TemplateTest0025 ()
        {
            await TemplateTest ("0025 - BMC - On Call Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0026 ()
        {
            await TemplateTest ("0026 - BMC - Pathology Report.doc");
        }

        [TestMethod]
        public async Task TemplateTest0027 ()
        {
            await TemplateTest ("0027 - BMC - Progress Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0028 ()
        {
            await TemplateTest ("0028 - BNJ_ChartNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0029 ()
        {
            await TemplateTest ("0029 - BNJ_ImpairmentRating.doc");
        }

        [TestMethod]
        public async Task TemplateTest0030 ()
        {
            await TemplateTest ("0030 - BNJ_Letterhead.doc");
        }

        [TestMethod]
        public async Task TemplateTest0031 ()
        {
            await TemplateTest ("0031 - BNJ_ProcedureNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0032 ()
        {
            await TemplateTest ("0032 - CAC_OfficeNoteTDD.doc");
        }

        [TestMethod]
        public async Task TemplateTest0033 ()
        {
            await TemplateTest ("0033 - CCV_CTA.doc");
        }

        [TestMethod]
        public async Task TemplateTest0034 ()
        {
            await TemplateTest ("0034 - CCV_Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0035 ()
        {
            await TemplateTest ("0035 - CCV_TDD.doc");
        }

        [TestMethod]
        public async Task TemplateTest0036 ()
        {
            await TemplateTest ("0036 - CCV_TEE.doc");
        }

        [TestMethod]
        public async Task TemplateTest0037 ()
        {
            await TemplateTest ("0037 - CHN_OfficeNoteTDD.doc");
        }

        [TestMethod]
        public async Task TemplateTest0038 ()
        {
            await TemplateTest ("0038 - CIO - Chart Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0039 ()
        {
            await TemplateTest ("0039 - CIO - Chart Note_PA.doc");
        }

        [TestMethod]
        public async Task TemplateTest0040 ()
        {
            await TemplateTest ("0040 - CIO - ClinicNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0041 ()
        {
            await TemplateTest ("0041 - CIO - Electrodiagnostic_Study.doc");
        }

        [TestMethod]
        public async Task TemplateTest0042 ()
        {
            await TemplateTest ("0042 - CIO - IME.doc");
        }

        [TestMethod]
        public async Task TemplateTest0043 ()
        {
            await TemplateTest ("0043 - CIO - Letter_MD.doc");
        }

        [TestMethod]
        public async Task TemplateTest0044 ()
        {
            await TemplateTest ("0044 - CIO - Letter_Other.doc");
        }

        [TestMethod]
        public async Task TemplateTest0045 ()
        {
            await TemplateTest ("0045 - CIO - Letter_PA.doc");
        }

        [TestMethod]
        public async Task TemplateTest0046 ()
        {
            await TemplateTest ("0046 - CIO - OpNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0047 ()
        {
            await TemplateTest ("0047 - CIO - ProcedureNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0048 ()
        {
            await TemplateTest ("0048 - CIO - Radiologic_Interpretation.doc");
        }

        [TestMethod]
        public async Task TemplateTest0049 ()
        {
            await TemplateTest ("0049 - CIO - SJOSC_OpNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0050 ()
        {
            await TemplateTest ("0050 - CIO - WorkComp.doc");
        }

        [TestMethod]
        public async Task TemplateTest0051 ()
        {
            await TemplateTest ("0051 - CMA - Diagnostics.doc");
        }

        [TestMethod]
        public async Task TemplateTest0052 ()
        {
            await TemplateTest ("0052 - CMA - Followup Office Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0053 ()
        {
            await TemplateTest ("0053 - CMA - Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0054 ()
        {
            await TemplateTest ("0054 - CMA - New Patient Office Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0055 ()
        {
            await TemplateTest ("0055 - CMC_Confidential__Nonpatient.doc");
        }

        [TestMethod]
        public async Task TemplateTest0056 ()
        {
            await TemplateTest ("0056 - CMC_Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0057 ()
        {
            await TemplateTest ("0057 - CMC_Office_Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0058 ()
        {
            await TemplateTest ("0058 - CMC_Op_Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0059 ()
        {
            await TemplateTest ("0059 - CMC_Phone_Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0060 ()
        {
            await TemplateTest ("0060 - CMC_Surgical_Booking.doc");
        }

        [TestMethod]
        public async Task TemplateTest0061 ()
        {
            await TemplateTest ("0061 - COO_ProgressNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0062 ()
        {
            await TemplateTest ("0062 - CPC - Clinic Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0063 ()
        {
            await TemplateTest ("0063 - CRO - Office Visit.doc");
        }

        [TestMethod]
        public async Task TemplateTest0064 ()
        {
            await TemplateTest ("0064 - CRS - Addendum.doc");
        }

        [TestMethod]
        public async Task TemplateTest0065 ()
        {
            await TemplateTest ("0065 - CRS - Hospital Visit.doc");
        }

        [TestMethod]
        public async Task TemplateTest0066 ()
        {
            await TemplateTest ("0066 - CRS - Inpatient Consult.doc");
        }

        [TestMethod]
        public async Task TemplateTest0067 ()
        {
            await TemplateTest ("0067 - CRS - Office Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0068 ()
        {
            await TemplateTest ("0068 - CRS - Operative Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0069 ()
        {
            await TemplateTest ("0069 - CRS - Outpatient Consult.doc");
        }

        [TestMethod]
        public async Task TemplateTest0070 ()
        {
            await TemplateTest ("0070 - CRS - Patient Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0071 ()
        {
            await TemplateTest ("0071 - CRS - Phone Note_Misc.doc");
        }

        [TestMethod]
        public async Task TemplateTest0072 ()
        {
            await TemplateTest ("0072 - CVA - Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0073 ()
        {
            await TemplateTest ("0073 - CVA - Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0074 ()
        {
            await TemplateTest ("0074 - CVA - PAMO-Dobutamine.NST.doc");
        }

        [TestMethod]
        public async Task TemplateTest0075 ()
        {
            await TemplateTest ("0075 - CVA - PAMO-Dobutamine.SEC.doc");
        }

        [TestMethod]
        public async Task TemplateTest0076 ()
        {
            await TemplateTest ("0076 - CVA - PAMO-Holter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0077 ()
        {
            await TemplateTest ("0077 - CVA - PAMO-LEXISCAN.doc");
        }

        [TestMethod]
        public async Task TemplateTest0078 ()
        {
            await TemplateTest ("0078 - CVA - PAMO-NST.CARDIOLITE.doc");
        }

        [TestMethod]
        public async Task TemplateTest0079 ()
        {
            await TemplateTest ("0079 - CVA - PAMO-PVL-PA.doc");
        }

        [TestMethod]
        public async Task TemplateTest0080 ()
        {
            await TemplateTest ("0080 - CVA - PAMO-STECHO.doc");
        }

        [TestMethod]
        public async Task TemplateTest0081 ()
        {
            await TemplateTest ("0081 - CVA - PAMO-TREADMILL.doc");
        }

        [TestMethod]
        public async Task TemplateTest0082 ()
        {
            await TemplateTest ("0082 - CVA - VBMO-Dobutamine.NST.doc");
        }

        [TestMethod]
        public async Task TemplateTest0083 ()
        {
            await TemplateTest ("0083 - CVA - VBMO-Dobutamine.SEC.doc");
        }

        [TestMethod]
        public async Task TemplateTest0084 ()
        {
            await TemplateTest ("0084 - CVA - VBMO-Holter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0085 ()
        {
            await TemplateTest ("0085 - CVA - VBMO-LEXISCAN.doc");
        }

        [TestMethod]
        public async Task TemplateTest0086 ()
        {
            await TemplateTest ("0086 - CVA - VBMO-NST.CARDIOLITE.doc");
        }

        [TestMethod]
        public async Task TemplateTest0087 ()
        {
            await TemplateTest ("0087 - CVA - VBMO-PVL-VBMO.doc");
        }

        [TestMethod]
        public async Task TemplateTest0088 ()
        {
            await TemplateTest ("0088 - CVA - VBMO-STECHO.doc");
        }

        [TestMethod]
        public async Task TemplateTest0089 ()
        {
            await TemplateTest ("0089 - CVA - VBMO-TREADMILL.doc");
        }

        [TestMethod]
        public async Task TemplateTest0090 ()
        {
            await TemplateTest ("0090 - CWH_OfficeNoteTDD.doc");
        }

        [TestMethod]
        public async Task TemplateTest0091 ()
        {
            await TemplateTest ("0091 - DJW - Riverpark_Radiology.doc");
        }

        [TestMethod]
        public async Task TemplateTest0092 ()
        {
            await TemplateTest ("0092 - DSGLaw - ROI_Complainant.doc");
        }

        [TestMethod]
        public async Task TemplateTest0093 ()
        {
            await TemplateTest ("0093 - EMS - Elder Acute Visit.doc");
        }

        [TestMethod]
        public async Task TemplateTest0094 ()
        {
            await TemplateTest ("0094 - EMS - Elder Annual History and Physical Exam.doc");
        }

        [TestMethod]
        public async Task TemplateTest0095 ()
        {
            await TemplateTest ("0095 - EMS - Elder History and Physical Examination.doc");
        }

        [TestMethod]
        public async Task TemplateTest0096 ()
        {
            await TemplateTest ("0096 - EMS - Elder Interval.doc");
        }

        [TestMethod]
        public async Task TemplateTest0097 ()
        {
            await TemplateTest ("0097 - EMS - Elder Medical - Discharge Summary.doc");
        }

        [TestMethod]
        public async Task TemplateTest0098 ()
        {
            await TemplateTest ("0098 - EMS - Elder OnCall.doc");
        }

        [TestMethod]
        public async Task TemplateTest0099 ()
        {
            await TemplateTest ("0099 - ETM_Addendum.doc");
        }

        [TestMethod]
        public async Task TemplateTest0100 ()
        {
            await TemplateTest ("0100 - ETM_ChartNote.doc");
        }
    }
}
