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
    public class TemplateTests2 : BaseTemplateTest
    {
        [TestMethod]
        public async Task TemplateTest0101 ()
        {
            await TemplateTest ("0101 - ETM_Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0102 ()
        {
            await TemplateTest ("0102 - ETM_OfficeVisitTDD.doc");
        }

        [TestMethod]
        public async Task TemplateTest0103 ()
        {
            await TemplateTest ("0103 - ETM_SleepStudy.doc");
        }

        [TestMethod]
        public async Task TemplateTest0104 ()
        {
            await TemplateTest ("0104 - ETM_Study.doc");
        }

        [TestMethod]
        public async Task TemplateTest0105 ()
        {
            await TemplateTest ("0105 - ETM - ProgressNote_TDD.doc");
        }

        [TestMethod]
        public async Task TemplateTest0106 ()
        {
            await TemplateTest ("0106 - EXC - CTSA_Addendum.doc");
        }

        [TestMethod]
        public async Task TemplateTest0107 ()
        {
            await TemplateTest ("0107 - EXC - CTSA_LetterHead.doc");
        }

        [TestMethod]
        public async Task TemplateTest0108 ()
        {
            await TemplateTest ("0108 - EXC-EHO - EXC_Followup Visit.doc");
        }

        [TestMethod]
        public async Task TemplateTest0109 ()
        {
            await TemplateTest ("0109 - EXC-EHO - EXC_LetterHead.doc");
        }

        [TestMethod]
        public async Task TemplateTest0110 ()
        {
            await TemplateTest ("0110 - EXC-EHO - EXC_Office Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0111 ()
        {
            await TemplateTest ("0111 - EXC-EHO - EXC_Office Visit.doc");
        }

        [TestMethod]
        public async Task TemplateTest0112 ()
        {
            await TemplateTest ("0112 - EXC-GSX - GS_Addendum.doc");
        }

        [TestMethod]
        public async Task TemplateTest0113 ()
        {
            await TemplateTest ("0113 - EXC-GSX - GS_Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0114 ()
        {
            await TemplateTest ("0114 - EXC-GSX - GS_OfficeNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0115 ()
        {
            await TemplateTest ("0115 - EXC-LCA - EXC_LetterHead.doc");
        }

        [TestMethod]
        public async Task TemplateTest0116 ()
        {
            await TemplateTest ("0116 - EXC-LCA - EXC_MedicationList.doc");
        }

        [TestMethod]
        public async Task TemplateTest0117 ()
        {
            await TemplateTest ("0117 - EXC-LCA - EXC_OfficeNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0118 ()
        {
            await TemplateTest ("0118 - EXC-NEU - Neuro_Addendum.doc");
        }

        [TestMethod]
        public async Task TemplateTest0119 ()
        {
            await TemplateTest ("0119 - EXC-NEU - Neuro_Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0120 ()
        {
            await TemplateTest ("0120 - EXC-NEU - Neuro_OfficeNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0121 ()
        {
            await TemplateTest ("0121 - EXC-WGA - GI_Addendum.doc");
        }

        [TestMethod]
        public async Task TemplateTest0122 ()
        {
            await TemplateTest ("0122 - EXC-WGA - GI_Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0123 ()
        {
            await TemplateTest ("0123 - EXC-WGA - GI_ProgressNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0124 ()
        {
            await TemplateTest ("0124 - FAL_OfficeNoteTDD.doc");
        }

        [TestMethod]
        public async Task TemplateTest0125 ()
        {
            await TemplateTest ("0125 - HAC_AdmitDischargeNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0126 ()
        {
            await TemplateTest ("0126 - HAC_Letterhead-Plain.doc");
        }

        [TestMethod]
        public async Task TemplateTest0127 ()
        {
            await TemplateTest ("0127 - HAC_MISCNOTE.doc");
        }

        [TestMethod]
        public async Task TemplateTest0128 ()
        {
            await TemplateTest ("0128 - HAC_OUT-New.doc");
        }

        [TestMethod]
        public async Task TemplateTest0129 ()
        {
            await TemplateTest ("0129 - HAC_ST-THOMAS_OUT-New.doc");
        }

        [TestMethod]
        public async Task TemplateTest0130 ()
        {
            await TemplateTest ("0130 - HAV_OfficeNoteTDD.doc");
        }

        [TestMethod]
        public async Task TemplateTest0131 ()
        {
            await TemplateTest ("0131 - HCH - Generic Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0132 ()
        {
            await TemplateTest ("0132 - HCH_OfficeNoteTDD.doc");
        }

        [TestMethod]
        public async Task TemplateTest0133 ()
        {
            await TemplateTest ("0133 - HSA_OfficeNoteTDD.doc");
        }

        [TestMethod]
        public async Task TemplateTest0134 ()
        {
            await TemplateTest ("0134 - HUC - Office Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0135 ()
        {
            await TemplateTest ("0135 - HVR_Addendum.doc");
        }

        [TestMethod]
        public async Task TemplateTest0136 ()
        {
            await TemplateTest ("0136 - HVR_PHS.doc");
        }

        [TestMethod]
        public async Task TemplateTest0137 ()
        {
            await TemplateTest ("0137 - HVR_Radiology.doc");
        }

        [TestMethod]
        public async Task TemplateTest0138 ()
        {
            await TemplateTest ("0138 - MMI_OfficeNoteTDD.doc");
        }

        [TestMethod]
        public async Task TemplateTest0139 ()
        {
            await TemplateTest ("0139 - MNG - Administrative Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0140 ()
        {
            await TemplateTest ("0140 - MNG - Clinic Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0141 ()
        {
            await TemplateTest ("0141 - MOG - AS_Clinic Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0142 ()
        {
            await TemplateTest ("0142 - MOG - AS_Surgery Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0143 ()
        {
            await TemplateTest ("0143 - MOG - AS_WC Clinic Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0144 ()
        {
            await TemplateTest ("0144 - MOG - Chart Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0145 ()
        {
            await TemplateTest ("0145 - MOG - Express notes.doc");
        }

        [TestMethod]
        public async Task TemplateTest0146 ()
        {
            await TemplateTest ("0146 - MOG - WC Clinic Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0147 ()
        {
            await TemplateTest ("0147 - MOR_Addendum.doc");
        }

        [TestMethod]
        public async Task TemplateTest0148 ()
        {
            await TemplateTest ("0148 - MOR_Amendment.doc");
        }

        [TestMethod]
        public async Task TemplateTest0149 ()
        {
            await TemplateTest ("0149 - MOR_ClinicNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0150 ()
        {
            await TemplateTest ("0150 - MOR_IME.doc");
        }

        [TestMethod]
        public async Task TemplateTest0151 ()
        {
            await TemplateTest ("0151 - MOR_InternalMemo.doc");
        }

        [TestMethod]
        public async Task TemplateTest0152 ()
        {
            await TemplateTest ("0152 - MOR_LabNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0153 ()
        {
            await TemplateTest ("0153 - MOR_Letterhead_Winfield.doc");
        }

        [TestMethod]
        public async Task TemplateTest0154 ()
        {
            await TemplateTest ("0154 - MOR_Ltr_IntMemo_Winfield.doc");
        }

        [TestMethod]
        public async Task TemplateTest0155 ()
        {
            await TemplateTest ("0155 - MOR_OB_Letterhead.doc");
        }

        [TestMethod]
        public async Task TemplateTest0156 ()
        {
            await TemplateTest ("0156 - MOR_OpReport.doc");
        }

        [TestMethod]
        public async Task TemplateTest0157 ()
        {
            await TemplateTest ("0157 - MOR_ProcedureNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0158 ()
        {
            await TemplateTest ("0158 - MOR_TeleCall.doc");
        }

        [TestMethod]
        public async Task TemplateTest0159 ()
        {
            await TemplateTest ("0159 - MSA_HP.doc");
        }

        [TestMethod]
        public async Task TemplateTest0160 ()
        {
            await TemplateTest ("0160 - MSA_Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0161 ()
        {
            await TemplateTest ("0161 - MSA_OperativeReport.doc");
        }

        [TestMethod]
        public async Task TemplateTest0162 ()
        {
            await TemplateTest ("0162 - MSA_ProgressNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0163 ()
        {
            await TemplateTest ("0163 - MSA_TreatmentRecord.doc");
        }

        [TestMethod]
        public async Task TemplateTest0164 ()
        {
            await TemplateTest ("0164 - MSA_Vascular.doc");
        }

        [TestMethod]
        public async Task TemplateTest0165 ()
        {
            await TemplateTest ("0165 - MTL_Consult.doc");
        }

        [TestMethod]
        public async Task TemplateTest0166 ()
        {
            await TemplateTest ("0166 - MTL_LetterheadPlain.doc");
        }

        [TestMethod]
        public async Task TemplateTest0167 ()
        {
            await TemplateTest ("0167 - MTL_XRray.doc");
        }

        [TestMethod]
        public async Task TemplateTest0168 ()
        {
            await TemplateTest ("0168 - MWR - 2 Day Nuclear Scan Bellah.doc");
        }

        [TestMethod]
        public async Task TemplateTest0169 ()
        {
            await TemplateTest ("0169 - MWR - DEXA Scan Report.doc");
        }

        [TestMethod]
        public async Task TemplateTest0170 ()
        {
            await TemplateTest ("0170 - MWR - DEXA.doc");
        }

        [TestMethod]
        public async Task TemplateTest0171 ()
        {
            await TemplateTest ("0171 - MWR - Dialysis.doc");
        }

        [TestMethod]
        public async Task TemplateTest0172 ()
        {
            await TemplateTest ("0172 - MWR - Echo Bellah.doc");
        }

        [TestMethod]
        public async Task TemplateTest0173 ()
        {
            await TemplateTest ("0173 - MWR - Echo Report.doc");
        }

        [TestMethod]
        public async Task TemplateTest0174 ()
        {
            await TemplateTest ("0174 - MWR - Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0175 ()
        {
            await TemplateTest ("0175 - MWR - Letterhead.doc");
        }

        [TestMethod]
        public async Task TemplateTest0176 ()
        {
            await TemplateTest ("0176 - MWR - Missed Appointment Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0177 ()
        {
            await TemplateTest ("0177 - MWR - Nuclear Cardiolite.doc");
        }

        [TestMethod]
        public async Task TemplateTest0178 ()
        {
            await TemplateTest ("0178 - MWR - Nuclear Report Bellah.doc");
        }

        [TestMethod]
        public async Task TemplateTest0179 ()
        {
            await TemplateTest ("0179 - MWR - Nuclear Report Kauer.doc");
        }

        [TestMethod]
        public async Task TemplateTest0180 ()
        {
            await TemplateTest ("0180 - MWR - Nuclear Report Mikinski.doc");
        }

        [TestMethod]
        public async Task TemplateTest0181 ()
        {
            await TemplateTest ("0181 - MWR - Nuclear Report Single Bellah.doc");
        }

        [TestMethod]
        public async Task TemplateTest0182 ()
        {
            await TemplateTest ("0182 - MWR - Nuclear Report.doc");
        }

        [TestMethod]
        public async Task TemplateTest0183 ()
        {
            await TemplateTest ("0183 - MWR - Nuclear Scan.doc");
        }

        [TestMethod]
        public async Task TemplateTest0184 ()
        {
            await TemplateTest ("0184 - MWR - Phone Call.doc");
        }

        [TestMethod]
        public async Task TemplateTest0185 ()
        {
            await TemplateTest ("0185 - MWR - Procedure.doc");
        }

        [TestMethod]
        public async Task TemplateTest0186 ()
        {
            await TemplateTest ("0186 - MWR - Radiology Report.doc");
        }

/*      Can't figure out how to remove the transcription date field
        [TestMethod]
        public async Task TemplateTest0187 ()
        {
            await TemplateTest ("0187 - MWR - Stress Test with Cardiolite.doc");
        }

        [TestMethod]
        public async Task TemplateTest0188 ()
        {
            await TemplateTest ("0188 - MWR - Stress Test with Echo.doc");
        }
*/
        [TestMethod]
        public async Task TemplateTest0189 ()
        {
            await TemplateTest ("0189 - MWR - Vascular.doc");
        }

        [TestMethod]
        public async Task TemplateTest0190 ()
        {
            await TemplateTest ("0190 - MWR - Venous Operative Report.doc");
        }

        [TestMethod]
        public async Task TemplateTest0191 ()
        {
            await TemplateTest ("0191 - MWR - Venous Predetermination Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0192 ()
        {
            await TemplateTest ("0192 - NRS - Express Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0193 ()
        {
            await TemplateTest ("0193 - NRS - Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0194 ()
        {
            await TemplateTest ("0194 - NSA_EEG.doc");
        }

        [TestMethod]
        public async Task TemplateTest0195 ()
        {
            await TemplateTest ("0195 - NSA_EMG.doc");
        }

        [TestMethod]
        public async Task TemplateTest0196 ()
        {
            await TemplateTest ("0196 - NSA_Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0197 ()
        {
            await TemplateTest ("0197 - NSA_NewConsultExam.doc");
        }

        [TestMethod]
        public async Task TemplateTest0198 ()
        {
            await TemplateTest ("0198 - NSA_NormalRecheckExam.doc");
        }

        [TestMethod]
        public async Task TemplateTest0199 ()
        {
            await TemplateTest ("0199 - NSA_ProcedureNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0200 ()
        {
            await TemplateTest ("0200 - NWO - FollowupVisit.doc");
        }
    }
}
