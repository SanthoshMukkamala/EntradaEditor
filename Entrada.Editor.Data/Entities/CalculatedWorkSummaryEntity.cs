using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entrada.Entities;

namespace Entrada.Editor.Data
{
    public class CalculatedWorkSummaryEntity
    {
        public DateTime Date { get; set; }
        public float LineCount { get; set; }
        public int CharacterCount { get; set; }

        public static CalculatedWorkSummaryEntity FromWorkSummary (WorkSummaryEntity summary)
        {
            var new_sum = new CalculatedWorkSummaryEntity ();

            new_sum.Date = summary.Date;
            new_sum.LineCount = summary.NumVBC / 65.0f;
            new_sum.CharacterCount = summary.NumVBC;

            return new_sum;
        }
    }
}
