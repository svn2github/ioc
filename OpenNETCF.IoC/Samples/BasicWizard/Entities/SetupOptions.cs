using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace BasicWizard.Entities
{
    public class SetupOptions
    {
        public bool OptionA { get; set; }
        public bool OptionB { get; set; }
        public bool OptionC { get; set; }
        public bool OptionD { get; set; }

        public static SetupOptions Default
        {
            get
            {
                return new SetupOptions()
                {
                    OptionA = true,
                    OptionB = false,
                    OptionC = true,
                    OptionD = false
                };
            }
        }
    }
}
