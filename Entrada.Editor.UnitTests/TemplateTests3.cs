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
    public class TemplateTests3 : BaseTemplateTest
    {
        [TestMethod]
        public async Task TemplateTest0201 ()
        {
            await TemplateTest ("0201 - NWO - HistoryandPhysical.doc");
        }

        [TestMethod]
        public async Task TemplateTest0202 ()
        {
            await TemplateTest ("0202 - NWO_Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0203 ()
        {
            await TemplateTest ("0203 - NWP - Consultation.doc");
        }

        [TestMethod]
        public async Task TemplateTest0204 ()
        {
            await TemplateTest ("0204 - NWP - LMN.doc");
        }

        [TestMethod]
        public async Task TemplateTest0205 ()
        {
            await TemplateTest ("0205 - NWP - New Patient New Problem.doc");
        }

        [TestMethod]
        public async Task TemplateTest0206 ()
        {
            await TemplateTest ("0206 - NWP - OfficeNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0207 ()
        {
            await TemplateTest ("0207 - NWP - Radiology.doc");
        }

        [TestMethod]
        public async Task TemplateTest0208 ()
        {
            await TemplateTest ("0208 - NWP - RX After Hours.doc");
        }

        [TestMethod]
        public async Task TemplateTest0209 ()
        {
            await TemplateTest ("0209 - NWP - Work_Comp.doc");
        }

        [TestMethod]
        public async Task TemplateTest0210 ()
        {
            await TemplateTest ("0210 - OAL_OfficeNoteTDD.doc");
        }

        [TestMethod]
        public async Task TemplateTest0211 ()
        {
            await TemplateTest ("0211 - OAM - ADDENDUM.doc");
        }

        [TestMethod]
        public async Task TemplateTest0212 ()
        {
            await TemplateTest ("0212 - OAM - FPA.doc");
        }

        [TestMethod]
        public async Task TemplateTest0213 ()
        {
            await TemplateTest ("0213 - OAM - IME.doc");
        }

        [TestMethod]
        public async Task TemplateTest0214 ()
        {
            await TemplateTest ("0214 - OAM - INJECTION.doc");
        }

        [TestMethod]
        public async Task TemplateTest0215 ()
        {
            await TemplateTest ("0215 - OAM - LETTER.doc");
        }

        [TestMethod]
        public async Task TemplateTest0216 ()
        {
            await TemplateTest ("0216 - OAM - OFFICE NOTE.doc");
        }

        [TestMethod]
        public async Task TemplateTest0217 ()
        {
            await TemplateTest ("0217 - OAM - SURGERY CONSULT.doc");
        }

        [TestMethod]
        public async Task TemplateTest0218 ()
        {
            await TemplateTest ("0218 - OAM - WCP.doc");
        }

        [TestMethod]
        public async Task TemplateTest0219 ()
        {
            await TemplateTest ("0219 - OAM - XRAY.doc");
        }

        [TestMethod]
        public async Task TemplateTest0220 ()
        {
            await TemplateTest ("0220 - OAP_Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0221 ()
        {
            await TemplateTest ("0221 - OAP_Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0222 ()
        {
            await TemplateTest ("0222 - OAT_Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0223 ()
        {
            await TemplateTest ("0223 - OBERD_HP.doc");
        }

        [TestMethod]
        public async Task TemplateTest0224 ()
        {
            await TemplateTest ("0224 - OBR - ENT_ProgressNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0225 ()
        {
            await TemplateTest ("0225 - OBR - Generic_ProgressNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0226 ()
        {
            await TemplateTest ("0226 - OGA_OfficeNoteTDD.doc");
        }

        [TestMethod]
        public async Task TemplateTest0227 ()
        {
            await TemplateTest ("0227 - OLY - Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0228 ()
        {
            await TemplateTest ("0228 - ONC - AME.doc");
        }

        [TestMethod]
        public async Task TemplateTest0229 ()
        {
            await TemplateTest ("0229 - ONC - Chart Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0230 ()
        {
            await TemplateTest ("0230 - ONC - Legal.doc");
        }

        [TestMethod]
        public async Task TemplateTest0231 ()
        {
            await TemplateTest ("0231 - ONC - Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0232 ()
        {
            await TemplateTest ("0232 - ONC - QME.doc");
        }

        [TestMethod]
        public async Task TemplateTest0233 ()
        {
            await TemplateTest ("0233 - OTN - MOC_Letterhead.doc");
        }

        [TestMethod]
        public async Task TemplateTest0234 ()
        {
            await TemplateTest ("0234 - OTN - OSOR_Letterhead.doc");
        }

        [TestMethod]
        public async Task TemplateTest0235 ()
        {
            await TemplateTest ("0235 - OTN - OTN_Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0236 ()
        {
            await TemplateTest ("0236 - OTN - OTN_MOC_ChartNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0237 ()
        {
            await TemplateTest ("0237 - OTN - OTN_OSOR_OfficeNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0238 ()
        {
            await TemplateTest ("0238 - ONT-KOC - Clinic Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0239 ()
        {
            await TemplateTest ("0239 - ONT-KOC - IME.doc");
        }

        [TestMethod]
        public async Task TemplateTest0240 ()
        {
            await TemplateTest ("0240 - ONT-KOC - Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0241 ()
        {
            await TemplateTest ("0241 - ONT-KOC - OpNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0242 ()
        {
            await TemplateTest ("0242 - OTN-MOC - MOC_Letterhead.doc");
        }

        [TestMethod]
        public async Task TemplateTest0243 ()
        {
            await TemplateTest ("0243 - OTN_MOC_ChartNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0244 ()
        {
            await TemplateTest ("0244 - OTN-OSOR - OTN Letterhead with Logo.doc");
        }

        [TestMethod]
        public async Task TemplateTest0245 ()
        {
            await TemplateTest ("0245 - OTN_OSOR_OfficeNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0246 ()
        {
            await TemplateTest ("0246 - OTN-PT - Clinic.doc");
        }

        [TestMethod]
        public async Task TemplateTest0247 ()
        {
            await TemplateTest ("0247 - OTN-UOS - UOS_Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0248 ()
        {
            await TemplateTest ("0248 - OTN-UOS - UOS_OfficeNote.doc");
        }
        [TestMethod]
        public async Task TemplateTest0249 ()
        {
            await TemplateTest ("0249 - SOC - Hospital.doc");
        }

        [TestMethod]
        public async Task TemplateTest0250 ()
        {
            await TemplateTest ("0250 - SOC - Letter - Angle.doc");
        }

        [TestMethod]
        public async Task TemplateTest0251 ()
        {
            await TemplateTest ("0251 - SOC - Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0252 ()
        {
            await TemplateTest ("0252 - SOC - Note.doc");
        }

/*
        [TestMethod]
        public async Task TemplateTest0253 ()
        {
            await TemplateTest ("0153 - MOR_Letterhead_Winfield.doc");
        }

        [TestMethod]
        public async Task TemplateTest0254 ()
        {
            await TemplateTest ("0154 - MOR_Ltr_IntMemo_Winfield.doc");
        }

        [TestMethod]
        public async Task TemplateTest0255 ()
        {
            await TemplateTest ("0155 - MOR_OB_Letterhead.doc");
        }

        [TestMethod]
        public async Task TemplateTest0256 ()
        {
            await TemplateTest ("0156 - MOR_OpReport.doc");
        }

        [TestMethod]
        public async Task TemplateTest0257 ()
        {
            await TemplateTest ("0157 - MOR_ProcedureNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0258 ()
        {
            await TemplateTest ("0158 - MOR_TeleCall.doc");
        }

        [TestMethod]
        public async Task TemplateTest0259 ()
        {
            await TemplateTest ("0159 - MSA_HP.doc");
        }

        [TestMethod]
        public async Task TemplateTest0260 ()
        {
            await TemplateTest ("0160 - MSA_Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0261 ()
        {
            await TemplateTest ("0161 - MSA_OperativeReport.doc");
        }

        [TestMethod]
        public async Task TemplateTest0262 ()
        {
            await TemplateTest ("0162 - MSA_ProgressNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0263 ()
        {
            await TemplateTest ("0163 - MSA_TreatmentRecord.doc");
        }

        [TestMethod]
        public async Task TemplateTest0264 ()
        {
            await TemplateTest ("0164 - MSA_Vascular.doc");
        }

        [TestMethod]
        public async Task TemplateTest0265 ()
        {
            await TemplateTest ("0165 - MTL_Consult.doc");
        }

        [TestMethod]
        public async Task TemplateTest0266 ()
        {
            await TemplateTest ("0166 - MTL_LetterheadPlain.doc");
        }

        [TestMethod]
        public async Task TemplateTest0267 ()
        {
            await TemplateTest ("0167 - MTL_XRray.doc");
        }

        [TestMethod]
        public async Task TemplateTest0268 ()
        {
            await TemplateTest ("0168 - MWR - 2 Day Nuclear Scan Bellah.doc");
        }

        [TestMethod]
        public async Task TemplateTest0269 ()
        {
            await TemplateTest ("0169 - MWR - DEXA Scan Report.doc");
        }

        [TestMethod]
        public async Task TemplateTest0270 ()
        {
            await TemplateTest ("0170 - MWR - DEXA.doc");
        }

        [TestMethod]
        public async Task TemplateTest0271 ()
        {
            await TemplateTest ("0171 - MWR - Dialysis.doc");
        }

        [TestMethod]
        public async Task TemplateTest0272 ()
        {
            await TemplateTest ("0172 - MWR - Echo Bellah.doc");
        }

        [TestMethod]
        public async Task TemplateTest0273 ()
        {
            await TemplateTest ("0173 - MWR - Echo Report.doc");
        }

        [TestMethod]
        public async Task TemplateTest0274 ()
        {
            await TemplateTest ("0174 - MWR - Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0275 ()
        {
            await TemplateTest ("0175 - MWR - Letterhead.doc");
        }

        [TestMethod]
        public async Task TemplateTest0276 ()
        {
            await TemplateTest ("0176 - MWR - Missed Appointment Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0277 ()
        {
            await TemplateTest ("0177 - MWR - Nuclear Cardiolite.doc");
        }

        [TestMethod]
        public async Task TemplateTest0278 ()
        {
            await TemplateTest ("0178 - MWR - Nuclear Report Bellah.doc");
        }

        [TestMethod]
        public async Task TemplateTest0279 ()
        {
            await TemplateTest ("0179 - MWR - Nuclear Report Kauer.doc");
        }

        [TestMethod]
        public async Task TemplateTest0280 ()
        {
            await TemplateTest ("0180 - MWR - Nuclear Report Mikinski.doc");
        }

        [TestMethod]
        public async Task TemplateTest0281 ()
        {
            await TemplateTest ("0181 - MWR - Nuclear Report Single Bellah.doc");
        }

        [TestMethod]
        public async Task TemplateTest0282 ()
        {
            await TemplateTest ("0182 - MWR - Nuclear Report.doc");
        }

        [TestMethod]
        public async Task TemplateTest0283 ()
        {
            await TemplateTest ("0183 - MWR - Nuclear Scan.doc");
        }

        [TestMethod]
        public async Task TemplateTest0284 ()
        {
            await TemplateTest ("0184 - MWR - Phone Call.doc");
        }

        [TestMethod]
        public async Task TemplateTest0285 ()
        {
            await TemplateTest ("0185 - MWR - Procedure.doc");
        }

        [TestMethod]
        public async Task TemplateTest0286 ()
        {
            await TemplateTest ("0186 - MWR - Radiology Report.doc");
        }

        [TestMethod]
        public async Task TemplateTest0287 ()
        {
            await TemplateTest ("0187 - MWR - Stress Test with Cardiolite.doc");
        }

        [TestMethod]
        public async Task TemplateTest0288 ()
        {
            await TemplateTest ("0188 - MWR - Stress Test with Echo.doc");
        }

        [TestMethod]
        public async Task TemplateTest0289 ()
        {
            await TemplateTest ("0189 - MWR - Vascular.doc");
        }

        [TestMethod]
        public async Task TemplateTest0290 ()
        {
            await TemplateTest ("0190 - MWR - Venous Operative Report.doc");
        }

        [TestMethod]
        public async Task TemplateTest0291 ()
        {
            await TemplateTest ("0191 - MWR - Venous Predetermination Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0292 ()
        {
            await TemplateTest ("0192 - NRS - Express Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0293 ()
        {
            await TemplateTest ("0193 - NRS - Note.doc");
        }

        [TestMethod]
        public async Task TemplateTest0294 ()
        {
            await TemplateTest ("0194 - NSA_EEG.doc");
        }

        [TestMethod]
        public async Task TemplateTest0295 ()
        {
            await TemplateTest ("0195 - NSA_EMG.doc");
        }

        [TestMethod]
        public async Task TemplateTest0296 ()
        {
            await TemplateTest ("0196 - NSA_Letter.doc");
        }

        [TestMethod]
        public async Task TemplateTest0297 ()
        {
            await TemplateTest ("0197 - NSA_NewConsultExam.doc");
        }

        [TestMethod]
        public async Task TemplateTest0298 ()
        {
            await TemplateTest ("0198 - NSA_NormalRecheckExam.doc");
        }

        [TestMethod]
        public async Task TemplateTest0299 ()
        {
            await TemplateTest ("0199 - NSA_ProcedureNote.doc");
        }

        [TestMethod]
        public async Task TemplateTest0200 ()
        {
            await TemplateTest ("0200 - NWO - FollowupVisit.doc");
        }*/
    }
}
