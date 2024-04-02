using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectExample.Models.Entities
{
    public class ShowInterView
    {
        public Employee Employee { get; set; }
        public Candidate_Employe Candidate_Employe { get; set; }
        public Cadidate Cadidate { get; set; }  
    }
}